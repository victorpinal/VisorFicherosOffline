Imports MediaInfoNET
Imports MySql.Data.MySqlClient
Imports System.IO.Path
Imports System.ComponentModel

Public Class Principal

    Shared dataBase As BaseDatos

    Private myTableDevices As DataTable
    Private myTableMediaFormat As DataTable

    Private Sub Principal_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        dataBase = New BaseDatos()

        'load the imagelist
        uxImageList.Images.AddRange({My.Resources.folder, My.Resources.file})

        'load initial tables
        myTableMediaFormat = dataBase.Select("SELECT * FROM media_format")

    End Sub

    Private Sub Principal_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        loadDevices()

    End Sub

    Private Sub Principal_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing

        e.Cancel = uxBackground.IsBusy

    End Sub

    Private Sub Principal_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed

        dataBase.Dispose()

    End Sub

#Region "BackgroundWorker (Load folder)"

    Private Sub uxBackground_DoWork(sender As Object, e As DoWorkEventArgs) Handles uxBackground.DoWork

        loadFolderInDB(e.Argument.ToString)

    End Sub

    Private Sub uxBackground_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles uxBackground.ProgressChanged

        Dim elapsed As Double = Now.TimeOfDay.TotalMilliseconds - CDbl(uxProgressBar.Tag)
        Dim progress As ReportProgress = CType(e.UserState, ReportProgress)
        uxProgressBar.Value = progress.counter
        uxStatusLabel.Text = String.Format("FILE {0}/{1} TIME {2}/{3}  {4}",
                                           uxProgressBar.Value,
                                           uxProgressBar.Maximum,
                                           GetStringFormMillis(elapsed),
                                           GetStringFormMillis(elapsed * (uxProgressBar.Maximum - uxProgressBar.Value) / CDbl(uxProgressBar.Value)),
                                           progress.entry)

    End Sub

    Private Sub uxBackground_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles uxBackground.RunWorkerCompleted

        loadDevices()
        uxProgressBar.Visible = False
        uxProgressBar.Value = 0
        uxStatusLabel.Text = String.Empty
        uxbtnLoadFolder.Image = My.Resources.folder
        uxbtnLoadFolder.Tag = Nothing

    End Sub

#End Region

#Region "Load Data"

    Private Sub uxbtnLoadFolder_Click(sender As Object, e As EventArgs) Handles uxbtnLoadFolder.Click

        If (uxbtnLoadFolder.Tag Is Nothing) Then

            With New FolderBrowserDialog
                If (.ShowDialog() = DialogResult.OK) Then
                    uxStatusLabel.Text = "Scaning filesystem in " & .SelectedPath & "..."
                    uxProgressBar.Maximum = FolderInfo.getFilesCount(.SelectedPath)
                    uxProgressBar.Tag = Now.TimeOfDay.TotalMilliseconds        'Save actual time to make ETA
                    uxProgressBar.Value = 0
                    uxProgressBar.Visible = True
                    uxBackground.RunWorkerAsync(.SelectedPath)
                    uxbtnLoadFolder.Image = My.Resources.alert
                    uxbtnLoadFolder.Tag = "CANCEL"
                End If
            End With

        Else

            'Cancel de load
            uxBackground.CancelAsync()

        End If

    End Sub

    Private Sub loadFolderInDB(selectedPath As String)

        'init
        Dim device As Device = getDevice(selectedPath)
        Dim rootFolder As New FolderInfo With {.info = New IO.DirectoryInfo(selectedPath), .id = findFolderId(selectedPath, device)}
        Dim myTableLoad As DataTable = dataBase.Select("SELECT id,name,parent_id,is_folder FROM files WHERE device_id=" & device.id)

        'Erase some useless records from datatable (parents of rootfolder)        

        'Timer to show progress
        Dim showProgress As Boolean = False
        Dim timer As New Timers.Timer(1000)
        AddHandler timer.Elapsed, Sub() showProgress = True
        timer.Enabled = True

        Dim id As Integer = 0
        Dim counter As Integer = 0

        'Make a stack to follow the structure
        Dim stack As New Stack(Of FolderInfo)
        stack.Push(rootFolder)

        While (stack.Count > 0 And Not uxBackground.CancellationPending)

            Dim currentDir As FolderInfo = stack.Pop

            'TODO Check changes file<->dir

            'Delete old nonexisting entries in db
            For Each entry As DataRow In myTableLoad.Select("parent_id=" & currentDir.id.ToString).Where(Function(f) Not (From e In currentDir.info.GetFileSystemInfos
                                                                                                                          Select e.Name).Contains(f("name").ToString))
                dataBase.ExecuteNonQuery("DELETE FROM files WHERE id=@id", New MySqlParameter("id", entry("id")))
                entry.Delete()
            Next

            'insert folders
            For Each entry As IO.DirectoryInfo In currentDir.info.GetDirectories
                Try

                    entry.GetAccessControl()    'check permissions before insert
                    If (CBool(entry.Attributes And (IO.FileAttributes.Hidden Or IO.FileAttributes.System))) Then Continue For

                    Dim myRowEntry As DataRow = myTableLoad.Select(String.Format("is_folder=1 AND name='{0}' AND parent_id={1}", QuitaComilla(entry.Name), currentDir.id)).FirstOrDefault

                    'Insert new entries        
                    If (myRowEntry Is Nothing) Then
                        id = dataBase.ExecuteScalar("INSERT INTO files (name,parent_id,is_folder,size,creation_date,device_id) " &
                                                    "VALUES (@name,@parent_id,@is_folder,@size,@creation_date,@device_id); " &
                                                    "SELECT LAST_INSERT_ID()",
                                                    {New MySqlParameter("name", entry.Name),
                                                     New MySqlParameter("parent_id", currentDir.id),
                                                     New MySqlParameter("is_folder", 1),
                                                     New MySqlParameter("size", Nothing),
                                                     New MySqlParameter("creation_date", entry.CreationTime),
                                                     New MySqlParameter("device_id", device.id)})
                    Else
                        id = CInt(myRowEntry("id"))
                        myRowEntry.Delete()
                    End If

                    stack.Push(New FolderInfo With {.info = entry, .id = id})

                Catch ex As Exception
                    'GetAccessControl throws exception                       
                End Try
            Next

            'insert files
            For Each entry As IO.FileInfo In currentDir.info.GetFiles

                'Show progressbar                
                counter += 1
                If (showProgress) Then
                    If (uxBackground.CancellationPending) Then Exit For
                    uxBackground.ReportProgress(0, New ReportProgress With {.counter = counter, .entry = entry.FullName})
                    showProgress = False
                End If

                'Discard hidden/system files
                If (CBool(entry.Attributes And (IO.FileAttributes.Hidden Or IO.FileAttributes.System))) Then Continue For
                Dim myRowEntry As DataRow = myTableLoad.Select(String.Format("is_folder=0 AND name='{0}' AND parent_id={1}", QuitaComilla(entry.Name), currentDir.id)).FirstOrDefault

                'Insert new entries        
                If (myRowEntry Is Nothing) Then

                    'insert with mediainfo
                    If (videoExtension.Contains(entry.Extension.ToLower)) Then

                        id = dataBase.ExecuteScalar("INSERT INTO files (name,parent_id,is_folder,size,creation_date,device_id) " &
                                                   "VALUES (@name,@parent_id,@is_folder,@size,@creation_date,@device_id); " &
                                                   "SELECT LAST_INSERT_ID()",
                                                   {New MySqlParameter("name", entry.Name),
                                                    New MySqlParameter("parent_id", currentDir.id),
                                                    New MySqlParameter("is_folder", 0),
                                                    New MySqlParameter("size", entry.Length),
                                                    New MySqlParameter("creation_date", entry.CreationTime),
                                                    New MySqlParameter("device_id", device.id)})

                        Try
                            With New MediaFile(entry.FullName)
                                dataBase.ExecuteNonQuery("INSERT INTO media_info (file_id,format,duration,v_format,v_codec,v_width,v_height,v_framerate,a_count,a_format,a_bitrate) " &
                                                         "VALUES (@file_id,@format,@duration,@v_format,@v_codec,@v_width,@v_height,@v_framerate,@a_count,@a_format,@a_bitrate)",
                                                         {New MySqlParameter("file_id", id),
                                                          New MySqlParameter("format", getMediaFormatId(.General.FormatID)),
                                                          New MySqlParameter("duration", .General.DurationMillis),
                                                          New MySqlParameter("v_format", If(.Video.FirstOrDefault Is Nothing, Nothing, getMediaFormatId(.Video.First.FormatID))),
                                                          New MySqlParameter("v_codec", If(.Video.FirstOrDefault Is Nothing, Nothing, getMediaFormatId(.Video.First.CodecID))),
                                                          New MySqlParameter("v_width", If(.Video.FirstOrDefault Is Nothing, Nothing, .Video.First.Width)),
                                                          New MySqlParameter("v_height", If(.Video.FirstOrDefault Is Nothing, Nothing, .Video.First.Height)),
                                                          New MySqlParameter("v_framerate", If(.Video.FirstOrDefault Is Nothing, Nothing, .Video.First.FrameRate)),
                                                          New MySqlParameter("a_count", .Audio.Count),
                                                          New MySqlParameter("a_format", If(.Audio.FirstOrDefault Is Nothing, Nothing, getMediaFormatId(.Audio.First.FormatID))),
                                                          New MySqlParameter("a_bitrate", If(.Audio.FirstOrDefault Is Nothing, Nothing, .Audio.First.Bitrate))})
                            End With
                        Catch ex As Exception
                            Errores("[" & entry.FullName & "] " & ex.Message, False)
                        End Try


                    Else

                        'insert without mediainfo
                        dataBase.ExecuteNonQuery("INSERT INTO files (name,parent_id,is_folder,size,creation_date,device_id) " &
                                                "VALUES (@name,@parent_id,@is_folder,@size,@creation_date,@device_id)",
                                                {New MySqlParameter("name", entry.Name),
                                                New MySqlParameter("parent_id", currentDir.id),
                                                New MySqlParameter("is_folder", 0),
                                                New MySqlParameter("size", entry.Length),
                                                New MySqlParameter("creation_date", entry.CreationTime),
                                                New MySqlParameter("device_id", device.id)})

                    End If

                Else

                    myRowEntry.Delete()

                End If

            Next

        End While

    End Sub

    Private Function findFolderId(path As String, device As Device) As Object

        Dim formatedPath As String = Replace(path, GetPathRoot(path), device.name & DirectorySeparatorChar)
        Dim id As Object = Nothing

        For Each dir As String In formatedPath.Split(DirectorySeparatorChar)

            If (String.IsNullOrEmpty(dir)) Then Continue For

            Dim dirRow As DataRow = dataBase.Select("SELECT id FROM files WHERE device_id=@device_id AND is_folder=1 AND name=@name AND ifnull(parent_id,0)=ifnull(@parent_id,0)",
                                                    {New MySqlParameter("device_id", device.id),
                                                    New MySqlParameter("name", dir),
                                                    New MySqlParameter("parent_id", id)}).AsEnumerable.FirstOrDefault
            'insert if not exists
            If (dirRow Is Nothing) Then
                dirRow = dataBase.Select("INSERT INTO files (name,parent_id,is_folder,device_id) VALUES (@name,@parent_id,1,@device_id); " &
                                         "SELECT LAST_INSERT_ID() AS id",
                                         {New MySqlParameter("name", dir),
                                          New MySqlParameter("parent_id", id),
                                          New MySqlParameter("device_id", device.id)}).AsEnumerable.First
            End If

            id = dirRow("id")

        Next

        Return id

    End Function

    Private Function getDevice(fullpath As String) As Device

        Dim serial As String = GetHDSerialNo(fullpath.Split(":"c)(0))
        Dim name As String = IO.DriveInfo.GetDrives().First(Function(i) i.Name = GetPathRoot(fullpath)).VolumeLabel

        Dim myRowDevice As DataRow = myTableDevices.Select(String.Format("serial='{0}'", serial)).FirstOrDefault

        If (myRowDevice IsNot Nothing) Then

            'Check for label update
            If (myRowDevice("name").ToString <> name) Then
                dataBase.ExecuteNonQuery("UPDATE device SET name=@name WHERE id=@id;" &
                                         "UPDATE files SET name=@name WHERE device_id=@id AND parent_id IS NULL",
                                        {New MySqlParameter("name", name),
                                         New MySqlParameter("id", myRowDevice("id"))})
                myTableDevices = dataBase.Select("SELECT * FROM vw_device")
            End If

            Return New Device(CInt(myRowDevice("id")), name, serial)

        Else

            dataBase.ExecuteNonQuery("INSERT INTO device (name,serial) VALUES (@name,@serial)",
                                    {New MySqlParameter("name", name),
                                     New MySqlParameter("serial", serial)})
            myTableDevices = dataBase.Select("SELECT * FROM vw_device")
            Return getDevice(fullpath)

        End If

    End Function

    Private Function getMediaFormatId(format As String) As Object

        If (String.IsNullOrEmpty(format)) Then
            Return Nothing
        Else

            Dim formats As DataRow() = myTableMediaFormat.Select(String.Format("name='{0}'", format))
            If (formats.Count > 0) Then
                Return CInt(formats(0)("id"))
            Else
                dataBase.ExecuteNonQuery("INSERT INTO media_format (name) VALUES (@p1)", New MySqlParameter("p1", format))
                myTableMediaFormat = dataBase.Select("SELECT * FROM media_format")
                Return getMediaFormatId(format)
            End If

        End If

    End Function

#End Region

#Region "Explorer"

    Private Sub loadDevices()

        myTableDevices = dataBase.Select("SELECT * FROM vw_device")

        uxcmbDeviceNames.DisplayMember = "description"
        uxcmbDeviceNames.ValueMember = "id"
        uxcmbDeviceNames.DataSource = myTableDevices

    End Sub

    Private Sub uxcmbDeviceNames_SelectedIndexChanged(sender As Object, e As EventArgs) Handles uxcmbDeviceNames.SelectedIndexChanged

        'init table folders
        Dim myTableFolders As DataTable = dataBase.Select("SELECT id,name,parent_id FROM files WHERE is_folder=1 AND device_id=@device_id",
                                                          New MySqlParameter("device_id", uxcmbDeviceNames.SelectedValue))

        'load folder tree
        uxTreeFolder.BeginUpdate()
        uxTreeFolder.Nodes.Clear()

        Dim stack As New Stack(Of TreeNode)
        Dim node As TreeNode = (From f In myTableFolders.Select("parent_id IS NULL").AsEnumerable Select New TreeNode(f("name").ToString) With {.Name = f("id").ToString}).First
        stack.Push(node)

        While stack.Count > 0
            Dim currentNode As TreeNode = stack.Pop()
            For Each myRow As DataRow In myTableFolders.Select("parent_id=" & currentNode.Name)
                Dim childNode As New TreeNode(myRow("name").ToString) With {.Name = myRow("id").ToString}
                currentNode.Nodes.Add(childNode)
                stack.Push(childNode)
            Next
        End While

        uxTreeFolder.Nodes.Add(node)
        uxTreeFolder.EndUpdate()

        'clear files list while not selection
        uxlstFiles.Items.Clear()

    End Sub

    Private Sub uxTreeFolder_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles uxTreeFolder.AfterSelect

        Dim numFiles As Integer = 0

        uxlstFiles.BeginUpdate()
        uxlstFiles.Items.Clear()

        For Each myRow As DataRow In dataBase.Select("SELECT * FROM vw_files WHERE parent_id=@parent_id",
                                                      New MySqlParameter("parent_id", uxTreeFolder.SelectedNode.Name)).AsEnumerable.OrderByDescending(Function(r) r("is_folder"))
            Dim folder As Boolean = (CInt(myRow("is_folder")) = 1)
            'Filters
            If (uxchkFilesOnly.Checked And folder) Then Continue For
            If (uxchkVideo.Checked And Not folder And Not videoExtension.Contains(GetExtension(myRow("name").ToString))) Then Continue For
            'End filters
            Dim item As New ListViewItem({myRow("name").ToString,
                                        If(folder, "DIR", FormatFileSize(CLng(myRow("size")))),
                                        If(Not IsNumeric(myRow("duration")), String.Empty, GetStringFormMillis(CLng(myRow("duration")))),
                                        myRow("v_format").ToString,
                                        myRow("v_codec").ToString,
                                        myRow("a_format").ToString,
                                        myRow("creation_date").ToString
                                        })
            item.Name = myRow("id").ToString
            item.ImageIndex = If(folder, 0, 1)
            uxlstFiles.Items.Add(item)

            If (Not folder) Then numFiles += 1

        Next

        uxlstFiles.EndUpdate()

        uxStatusFiles.Text = String.Format("{0:N0} Files", numFiles)
        uxStatusLabel.Text = uxTreeFolder.SelectedNode.FullPath

    End Sub

    Private Sub uxlstDetail_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles uxlstFiles.MouseDoubleClick

        Dim item As ListViewItem = uxlstFiles.HitTest(e.X, e.Y).Item
        If (item IsNot Nothing) Then
            Dim dir As TreeNode = uxTreeFolder.Nodes.Find(item.Name, True).FirstOrDefault
            If (dir IsNot Nothing) Then
                dir.EnsureVisible()
                uxTreeFolder.SelectedNode = dir
            End If
        End If

    End Sub

    Private Sub uxlstDetail_SelectedIndexChanged(sender As Object, e As EventArgs) Handles uxlstFiles.SelectedIndexChanged

        If (uxlstFiles.SelectedItems.Count > 0) Then
            uxStatusLabel.Text = Combine(uxTreeFolder.SelectedNode.FullPath, uxlstFiles.SelectedItems(0).Text)
        ElseIf uxTreeFolder.SelectedNode IsNot Nothing Then
            uxStatusLabel.Text = uxTreeFolder.SelectedNode.FullPath
        End If

    End Sub

#End Region

#Region "Filters"

    Private Sub uxchkVideo_CheckedChanged(sender As Object, e As EventArgs) Handles uxchkVideo.CheckedChanged

        If (uxTreeFolder.SelectedNode IsNot Nothing) Then uxTreeFolder_AfterSelect(Nothing, Nothing)

    End Sub

    Private Sub uxchkFilesOnly_CheckedChanged(sender As Object, e As EventArgs) Handles uxchkFilesOnly.CheckedChanged

        If (uxTreeFolder.SelectedNode IsNot Nothing) Then uxTreeFolder_AfterSelect(Nothing, Nothing)

    End Sub

    Private Sub uxcmbExtensions_SelectedIndexChanged(sender As Object, e As EventArgs) Handles uxcmbExtensions.SelectedIndexChanged

        If (uxTreeFolder.SelectedNode IsNot Nothing) Then uxTreeFolder_AfterSelect(Nothing, Nothing)

    End Sub

    Private Sub uxbntSearch_Click(sender As Object, e As EventArgs) Handles uxbntSearch.Click

        Static text As String = String.Empty
        Static count As Integer
        If (text <> uxtxtSearch.Text) Then
            text = uxtxtSearch.Text
            count = 0
        End If

        'search in the treeview for the first match

        Dim myRow As DataRow = dataBase.Select("SELECT id,is_folder,parent_id FROM files WHERE device_id=@device_id AND name LIKE @name",
                                                   {New MySqlParameter("device_id", uxcmbDeviceNames.SelectedValue),
                                                    New MySqlParameter("name", "%" & QuitaComilla(uxtxtSearch.Text) & "%")}).AsEnumerable.Skip(count).FirstOrDefault
        If (myRow IsNot Nothing) Then
            If (CInt(myRow("is_folder")) = 1) Then
                uxTreeFolder.SelectedNode = uxTreeFolder.Nodes.Find(myRow("id").ToString, True).FirstOrDefault
                uxTreeFolder.Focus()
            Else
                uxTreeFolder.SelectedNode = uxTreeFolder.Nodes.Find(myRow("parent_id").ToString, True).FirstOrDefault
                'TODOD check filters
                With uxlstFiles.Items.Find(myRow("id").ToString, False).FirstOrDefault
                    .Selected = True
                    .EnsureVisible()
                End With
                uxlstFiles.Focus()
            End If
            count += 1
        Else
            count = 0
        End If

    End Sub

    Private Sub uxtxtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles uxtxtSearch.KeyDown

        If (e.KeyCode = Keys.Enter) Then uxbntSearch.PerformClick()

    End Sub

#End Region

End Class

Imports MediaInfoNET
Imports MySql.Data.MySqlClient
Imports System.IO.Path
Imports System.ComponentModel

Public Class Principal

    Shared dataBase As BaseDatos

    Private myTableDevices As DataTable
    Private myTableMediaFormat As DataTable

    Const SQL_devices As String = "SELECT *, CONCAT(name,' [',serial,']') as description FROM device ORDER BY name"
    Const SQL_files As String = "SELECT * FROM vw_files"
    Const SQL_media_format As String = "SELECT * FROM media_format"

    Private Sub Principal_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        dataBase = New BaseDatos()
        AddHandler FormClosed, Sub() dataBase.Dispose()

        'load the imagelist
        uxImageList.Images.AddRange({My.Resources.folder, My.Resources.file})

        'load initial tables
        myTableMediaFormat = dataBase.Select(SQL_media_format)

        'Background task is load selected folder
        AddHandler uxBackground.DoWork, Sub(se As Object, ev As DoWorkEventArgs)
                                            loadFolderInDB(ev.Argument.ToString)
                                        End Sub
        AddHandler uxBackground.RunWorkerCompleted, Sub()
                                                        loadDevices()
                                                        uxbtnReloadFolder.Enabled = True
                                                        uxProgress.Value = 0
                                                        uxProgressLabel.Text = String.Empty
                                                    End Sub

        AddHandler uxBackground.ProgressChanged, Sub(se As Object, ev As ProgressChangedEventArgs)
                                                     uxProgressLabel.Text = String.Format("[{0}/{1}] {2}", uxProgress.Value, uxProgress.Maximum, ev.UserState.ToString)
                                                     uxProgress.PerformStep()
                                                 End Sub

    End Sub

    Private Sub Principal_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        loadDevices()

    End Sub

#Region "Load Data"

    Private Sub uxbtnReloadFolder_Click(sender As Object, e As EventArgs) Handles uxbtnReloadFolder.Click

        If (uxbtnReloadFolder.Tag Is Nothing) Then

            With New FolderBrowserDialog
                If (.ShowDialog() = DialogResult.OK) Then
                    uxProgressLabel.Text = "Scaning filesystem in " & .SelectedPath & "..."
                    uxProgress.Maximum = DirectoryInfo.getFilesCount(.SelectedPath)
                    uxProgress.Value = 0
                    uxBackground.RunWorkerAsync(.SelectedPath)
                    uxbtnReloadFolder.Image = My.Resources.alert
                    uxbtnReloadFolder.Tag = "CANCEL"
                End If
            End With

        Else

            'Cancel de load
            uxBackground.CancelAsync()
            uxbtnReloadFolder.Image = My.Resources.folder
            uxbtnReloadFolder.Tag = Nothing

        End If

    End Sub

    Private Sub loadFolderInDB(selectedPath As String)

        'init table
        Dim device As Device = getDevice(selectedPath)
        Dim myTableLoad As DataTable = dataBase.Select("SELECT id,name,parent_id,is_folder FROM files WHERE device_id=" & device.id)

        'Make a stack to follow the structure
        Dim stack As New Stack(Of DirectoryInfo)
        stack.Push(New DirectoryInfo With {.info = New IO.DirectoryInfo(selectedPath), .id = createFolderId(selectedPath, myTableLoad)})

        While (stack.Count > 0 And Not uxBackground.CancellationPending)

            Dim currentDir As DirectoryInfo = stack.Pop

            'TODO Check changes file<->dir

            'Delete old nonexisting entries in db
            For Each entry As DataRow In myTableLoad.Select("parent_id=" & currentDir.id).Where(Function(f) Not (From e In currentDir.info.GetFileSystemInfos
                                                                                                                 Select e.Name).Contains(f("name").ToString))
                dataBase.ExecuteNonQuery("DELETE FROM files WHERE id=@id", New MySqlParameter("id", entry("id")))
                entry.Delete()
            Next
            myTableLoad.AcceptChanges()

            'insert folders
            For Each entry As IO.DirectoryInfo In currentDir.info.GetDirectories
                Try

                    entry.GetAccessControl()    'check permissions before insert
                    If (CBool(entry.Attributes And (IO.FileAttributes.Hidden Or IO.FileAttributes.System))) Then Continue For

                    Dim myRowEntry As DataRow = myTableLoad.Select(String.Format("name='{0}' AND parent_id={1}", QuitaComilla(entry.Name), currentDir.id)).FirstOrDefault

                    'Insert new entries        
                    If (myRowEntry Is Nothing) Then
                        myRowEntry = dataBase.Select("INSERT INTO files (name,parent_id,is_folder,size,creation_date,device_id) " &
                                                     "VALUES (@name,@parent_id,@is_folder,@size,@creation_date,@device_id); " &
                                                     "SELECT id,name,parent_id,is_folder FROM files WHERE id=LAST_INSERT_ID()",
                                                     {New MySqlParameter("name", entry.Name),
                                                      New MySqlParameter("parent_id", currentDir.id),
                                                      New MySqlParameter("is_folder", 1),
                                                      New MySqlParameter("size", Nothing),
                                                      New MySqlParameter("creation_date", entry.CreationTime),
                                                      New MySqlParameter("device_id", device.id)}).AsEnumerable.FirstOrDefault

                        myTableLoad.LoadDataRow(myRowEntry.ItemArray, True)
                    End If

                    stack.Push(New DirectoryInfo With {.info = entry, .id = CInt(myRowEntry("id"))})

                Catch ex As Exception
                    'GetAccessControl throws exception   
                    'Errores("[" & entry.FullName & "] " & ex.Message, False)
                End Try
            Next

            'insert files
            For Each entry As IO.FileInfo In currentDir.info.GetFiles

                'Show progressbar
                uxBackground.ReportProgress(0, entry.FullName)

                'Discard hidden/system files
                If (CBool(entry.Attributes And (IO.FileAttributes.Hidden Or IO.FileAttributes.System))) Then Continue For
                Dim myRowEntry As DataRow = myTableLoad.Select(String.Format("name='{0}' AND parent_id={1}", QuitaComilla(entry.Name), currentDir.id)).FirstOrDefault

                'Insert new entries        
                If (myRowEntry Is Nothing) Then
                    myRowEntry = dataBase.Select("INSERT INTO files (name,parent_id,is_folder,size,creation_date,device_id) " &
                                                 "VALUES (@name,@parent_id,@is_folder,@size,@creation_date,@device_id); " &
                                                 "SELECT id,name,parent_id,is_folder FROM files WHERE id=LAST_INSERT_ID()",
                                                 {New MySqlParameter("name", entry.Name),
                                                  New MySqlParameter("parent_id", currentDir.id),
                                                  New MySqlParameter("is_folder", 0),
                                                  New MySqlParameter("size", entry.Length),
                                                  New MySqlParameter("creation_date", entry.CreationTime),
                                                  New MySqlParameter("device_id", device.id)}).AsEnumerable.FirstOrDefault

                    myTableLoad.LoadDataRow(myRowEntry.ItemArray, True)

                    'insert mediainfo
                    If (New String() {".avi", ".mkv", ".mp4", ".wmv", ".mpg", ".mpeg", ".m4v", ".rmvb", ".divx", ".mov", ".flv", ".asf", ".3gp"}.Contains(entry.Extension.ToLower)) Then
                        Try
                            With New MediaFile(entry.FullName)
                                dataBase.ExecuteNonQuery("INSERT INTO media_info (file_id,format,duration,v_format,v_codec,v_width,v_height,v_framerate,a_count,a_format,a_bitrate) " &
                                                         "VALUES (@file_id,@format,@duration,@v_format,@v_codec,@v_width,@v_height,@v_framerate,@a_count,@a_format,@a_bitrate)",
                                                              {New MySqlParameter("file_id", myRowEntry("id")),
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
                    End If

                End If

            Next

        End While

    End Sub

    Private Function createFolderId(path As String, ByRef tableLoad As DataTable, Optional parentId As Object = Nothing, Optional device As Device = Nothing) As Integer

        Dim formatedPath As String = path

        'If fullpath, drive leter is replaced by drive name and get serial        
        If (parentId Is Nothing) Then
            device = getDevice(path)
            formatedPath = Replace(path, GetPathRoot(path), device.name & DirectorySeparatorChar)
        End If

        Dim myTableResult As DataRow = tableLoad.Select(String.Format("is_folder=1 AND name='{0}' AND ISNULL(parent_id,0)={1}",
                                                                      QuitaComilla(formatedPath.Split(DirectorySeparatorChar)(0)),
                                                                      If(parentId Is Nothing, 0, parentId))).FirstOrDefault

        'Folder not exists then create
        If (myTableResult Is Nothing) Then
            myTableResult = dataBase.Select("INSERT INTO files (name,parent_id,is_folder,device_id) VALUES (@name,@parent_id,1,@device_id); " &
                                            "SELECT id,name,parent_id,is_folder FROM files WHERE id = LAST_INSERT_ID()",
                                            {New MySqlParameter("name", formatedPath.Split(DirectorySeparatorChar)(0)),
                                             New MySqlParameter("parent_id", parentId),
                                             New MySqlParameter("device_id", device.id)}).AsEnumerable.First
            tableLoad.LoadDataRow(myTableResult.ItemArray, True)
        End If

        ''If device name changes update the new name
        'If (Not myTableResult("name").Equals(formatedPath.Split(DirectorySeparatorChar)(0))) Then
        '    dataBase.ExecuteNonQuery("UPDATE files SET name=@name WHERE id=@id",
        '                                  {New MySqlParameter("name", formatedPath.Split(DirectorySeparatorChar)(0)),
        '                                  New MySqlParameter("id", myTableResult("id"))})
        'End If

        'Recursive find id
        If (System.Text.RegularExpressions.Regex.IsMatch(path, ".+\\.+")) Then
            Return createFolderId(path.Substring(path.IndexOf(DirectorySeparatorChar) + 1), tableLoad, CInt(myTableResult("id")), device)
        Else
            Return CInt(myTableResult("id"))
        End If

    End Function

    Private Function getDevice(fullpath As String) As Device

        Dim serial As String = GetHDSerialNo(fullpath.Split(":"c)(0))
        Dim myRowDevice As DataRow = myTableDevices.Select(String.Format("serial='{0}'", serial)).FirstOrDefault
        If (myRowDevice IsNot Nothing) Then
            Return New Device(CInt(myRowDevice("id")), myRowDevice("name").ToString, myRowDevice("serial").ToString)
        Else
            dataBase.ExecuteNonQuery("INSERT INTO device (name,serial) VALUES (@name,@serial)",
                                      {New MySqlParameter("name", IO.DriveInfo.GetDrives().First(Function(i) i.Name = GetPathRoot(fullpath)).VolumeLabel),
                                       New MySqlParameter("serial", serial)})
            myTableDevices = dataBase.Select(SQL_devices)
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
                myTableMediaFormat = dataBase.Select(SQL_media_format)
                Return getMediaFormatId(format)
            End If

        End If

    End Function

#End Region

    Private Sub loadDevices()

        myTableDevices = dataBase.Select(SQL_devices)

        uxcmbDeviceNames.DisplayMember = "description"
        uxcmbDeviceNames.ValueMember = "id"
        uxcmbDeviceNames.DataSource = myTableDevices

    End Sub

    Private Sub uxcmbDeviceNames_SelectedIndexChanged(sender As Object, e As EventArgs) Handles uxcmbDeviceNames.SelectedIndexChanged

        Dim myTableFolders As DataTable = dataBase.Select("SELECT id,name,parent_id FROM files WHERE is_folder=1 AND device_id=@device_id", New MySqlParameter("device_id", uxcmbDeviceNames.SelectedValue))

        uxTreeFolder.BeginUpdate()
        loadTreeInternal(CInt(uxcmbDeviceNames.SelectedValue), myTableFolders)
        uxTreeFolder.EndUpdate()

        uxlstDetail.Items.Clear()

    End Sub

    Private Sub loadTreeInternal(device_id As Integer, ByRef tableFolders As DataTable)

        uxTreeFolder.Nodes.Clear()

        Dim stack As New Stack(Of TreeNode)
        Dim node As TreeNode = (From f In tableFolders.Select("parent_id IS NULL").AsEnumerable Select New TreeNode(f("name").ToString) With {.Name = f("id").ToString}).First
        stack.Push(node)

        While stack.Count > 0
            Dim currentNode As TreeNode = stack.Pop()
            For Each myRow As DataRow In tableFolders.Select("parent_id=" & currentNode.Name)
                Dim childNode As New TreeNode(myRow("name").ToString) With {.Name = myRow("id").ToString}
                currentNode.Nodes.Add(childNode)
                stack.Push(childNode)
            Next
        End While

        uxTreeFolder.Nodes.Add(node)

    End Sub

    Private Sub uxtxtSearch_TextChanged(sender As Object, e As KeyEventArgs) Handles uxtxtSearch.KeyDown
        Static count As Integer = 0

        'search in the treeview for the first match
        If (e.KeyCode = Keys.Enter) Then

            Dim myRow As DataRow = dataBase.Select("SELECT id,is_folder,parent_id FROM files WHERE device_id=@device_id AND name LIKE @name",
                                                   {New MySqlParameter("device_id", uxcmbDeviceNames.SelectedValue),
                                                    New MySqlParameter("name", "%" & QuitaComilla(uxtxtSearch.Text) & "%")}).AsEnumerable.Skip(count).FirstOrDefault
            If (myRow IsNot Nothing) Then
                If (CInt(myRow("is_folder")) = 1) Then
                    uxTreeFolder.SelectedNode = uxTreeFolder.Nodes.Find(myRow("id").ToString, True).FirstOrDefault
                Else
                    uxTreeFolder.SelectedNode = uxTreeFolder.Nodes.Find(myRow("parent_id").ToString, True).FirstOrDefault
                    With uxlstDetail.Items.Find(myRow("id").ToString, False).FirstOrDefault
                        .Selected = True
                        .EnsureVisible()
                    End With
                End If
                count += 1
            Else
                count = 0
            End If
        Else

            count = 0

        End If

    End Sub

    Private Sub uxTreeFolder_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles uxTreeFolder.AfterSelect

        uxlstDetail.BeginUpdate()

        uxlstDetail.Items.Clear()
        For Each myRow As DataRow In dataBase.Select(SQL_files & " WHERE parent_id=@parent_id",
                                                      New MySqlParameter("parent_id", e.Node.Name)).AsEnumerable.OrderByDescending(Function(r) r("is_folder"))
            Dim folder As Boolean = (CInt(myRow("is_folder")) = 1)
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
            uxlstDetail.Items.Add(item)

        Next

        uxlstDetail.EndUpdate()

        uxProgressLabel.Text = uxTreeFolder.SelectedNode.FullPath

    End Sub

    Private Sub uxlstDetail_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles uxlstDetail.MouseDoubleClick

        Dim item As ListViewItem = uxlstDetail.HitTest(e.X, e.Y).Item
        If (item IsNot Nothing) Then
            Dim dir As TreeNode = uxTreeFolder.Nodes.Find(item.Name, True).FirstOrDefault
            If (dir IsNot Nothing) Then
                dir.EnsureVisible()
                uxTreeFolder.SelectedNode = dir
            End If
        End If

    End Sub

    Private Sub uxlstDetail_SelectedIndexChanged(sender As Object, e As EventArgs) Handles uxlstDetail.SelectedIndexChanged
        If (uxlstDetail.SelectedItems.Count > 0) Then
            uxProgressLabel.Text = Combine(uxTreeFolder.SelectedNode.FullPath, uxlstDetail.SelectedItems(0).Text)
        ElseIf uxTreeFolder.SelectedNode IsNot Nothing Then
            uxProgressLabel.Text = uxTreeFolder.SelectedNode.FullPath
        End If
    End Sub

End Class

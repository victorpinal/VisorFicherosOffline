Imports MediaInfoNET
Imports MySql.Data.MySqlClient
Imports System.IO.Path
Imports System.ComponentModel

Public Class Principal

    Private myTableDevices As DataTable
    Private myTableFiles As DataTable
    Private myTableMediaFormat As DataTable

    Const SQL_devices = "SELECT *, CONCAT(name,' [',serial,']') as description FROM device ORDER BY name"
    Const SQL_files = "SELECT * FROM vw_files"
    Const SQL_media_format = "SELECT * FROM media_format"

    'Shared bg As New BackgroundWorker()


    Private Sub Principal_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        BaseDatos.Check()   'Init db

        'load the imagelist if doesn't exists
        Dim myImageList As New ImageList()
        myImageList.Images.AddRange({My.Resources.folder, My.Resources.file})
        uxTreeFolder.ImageList = myImageList
        uxlstDetail.SmallImageList = myImageList

        'load initial tables
        myTableFiles = BaseDatos.Select(SQL_files & " WHERE 1<>1")
        myTableMediaFormat = BaseDatos.Select(SQL_media_format)

        'AddHandler bg.DoWork, Sub(se As Object, ev As DoWorkEventArgs) reloadFolder(ev.Argument.ToString)

    End Sub

    Private Sub Principal_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        'Load device names in combo
        loadDeviceNames()

    End Sub

#Region "Load Data"

    Private Sub uxbtnReloadFolder_Click(sender As Object, e As EventArgs) Handles uxbtnReloadFolder.Click

        With New FolderBrowserDialog
            If (.ShowDialog() = DialogResult.OK) Then
                uxProgress.Maximum = 1
                uxProgress.Value = 0
                Cursor.Current = Cursors.WaitCursor
                'bg.RunWorkerAsync(.SelectedPath)
                reloadFolder(.SelectedPath)
                loadDeviceNames()
                Cursor.Current = Cursors.Default
                uxProgress.Value = 0
                uxProgressLabel.Text = String.Empty
            End If
        End With

    End Sub

    Private Sub reloadFolder(selectedPath As String)

        'init table
        Dim device As Device = getDevice(selectedPath)
        myTableFiles = BaseDatos.Select(SQL_files & " WHERE device_id=" & device.id)

        'Make a stack to follow the structure
        Dim stack = New Stack(Of KeyValuePair(Of IO.DirectoryInfo, Integer))
        stack.Push(New KeyValuePair(Of IO.DirectoryInfo, Integer)(New IO.DirectoryInfo(selectedPath), createFolderId(selectedPath)))

        While stack.Count > 0

            Dim currentDir = stack.Pop
            uxProgress.Maximum += currentDir.Key.GetFileSystemInfos.Count

            'Delete old nonexisting entries in db
            'TODO Check changes file<->dir
            For Each entry As DataRow In myTableFiles.Select("parent_id=" & currentDir.Value).Where(Function(f) Not (From e In currentDir.Key.GetFileSystemInfos
                                                                                                                     Select e.Name).Contains(f("name").ToString))
                BaseDatos.ExecuteNonQuery("DELETE FROM files WHERE id=@id", New MySqlParameter("id", entry("id")))
                entry.Delete()
            Next
            myTableFiles.AcceptChanges()

            'insert folders
            For Each entry As IO.DirectoryInfo In currentDir.Key.GetDirectories
                Try

                    entry.GetAccessControl()    'check permissions before insert
                    If (CBool(entry.Attributes And (IO.FileAttributes.Hidden Or IO.FileAttributes.System))) Then Continue For

                    Dim myRowEntry As DataRow = myTableFiles.Select(String.Format("name='{0}' AND parent_id={1}", BaseDatos.QuitaComilla(entry.Name), currentDir.Value)).FirstOrDefault

                    'Insert new entries        
                    If (myRowEntry Is Nothing) Then
                        myRowEntry = BaseDatos.Select("INSERT INTO files (name,parent_id,is_folder,size,creation_date,device_id) " &
                                                      "VALUES (@name,@parent_id,@is_folder,@size,@creation_date,@device_id); " &
                                                      SQL_files & " WHERE id = LAST_INSERT_ID()",
                                                      {New MySqlParameter("name", entry.Name),
                                                       New MySqlParameter("parent_id", currentDir.Value),
                                                       New MySqlParameter("is_folder", 1),
                                                       New MySqlParameter("size", Nothing),
                                                       New MySqlParameter("creation_date", entry.CreationTime),
                                                       New MySqlParameter("device_id", device.id)}).AsEnumerable.FirstOrDefault

                        myTableFiles.LoadDataRow(myRowEntry.ItemArray, True)
                    End If

                    stack.Push(New KeyValuePair(Of IO.DirectoryInfo, Integer)(entry, CInt(myRowEntry("id"))))

                Catch ex As Exception
                    'GetAccessControl throws exception   
                    BaseDatos.Errores("[" & entry.FullName & "] " & ex.Message, False)
                End Try
            Next

            'insert files
            For Each entry As IO.FileInfo In currentDir.Key.GetFiles

                'some ui info stuff
                uxProgressLabel.Text = String.Format("[{0}/{1}] {2}", uxProgress.Value, uxProgress.Maximum, entry.FullName)
                uxProgress.PerformStep()
                uxState.Refresh()

                If (CBool(entry.Attributes And (IO.FileAttributes.Hidden Or IO.FileAttributes.System))) Then Continue For
                Dim myRowEntry As DataRow = myTableFiles.Select(String.Format("name='{0}' AND parent_id={1}", BaseDatos.QuitaComilla(entry.Name), currentDir.Value)).FirstOrDefault

                'Insert new entries        
                If (myRowEntry Is Nothing) Then
                    myRowEntry = BaseDatos.Select("INSERT INTO files (name,parent_id,is_folder,size,creation_date,device_id) " &
                                                          "VALUES (@name,@parent_id,@is_folder,@size,@creation_date,@device_id); " &
                                                          "SELECT LAST_INSERT_ID() as id",
                                                          {New MySqlParameter("name", entry.Name),
                                                           New MySqlParameter("parent_id", currentDir.Value),
                                                           New MySqlParameter("is_folder", 0),
                                                           New MySqlParameter("size", entry.Length),
                                                           New MySqlParameter("creation_date", entry.CreationTime),
                                                           New MySqlParameter("device_id", device.id)}).AsEnumerable.FirstOrDefault

                    'insert mediainfo
                    If (New String() {".avi", ".mkv", ".mp4", ".wmv", ".mpg", ".mpeg", ".m4v", ".rmvb", ".divx", ".mov", ".flv", ".asf", ".3gp"}.Contains(entry.Extension.ToLower)) Then
                        Try
                            With New MediaFile(entry.FullName)
                                BaseDatos.ExecuteNonQuery("INSERT INTO media_info (file_id,format,duration,v_format,v_width,v_height,v_framerate,a_count,a_format,a_bitrate) " &
                                                              "VALUES (@file_id,@format,@duration,@v_format,@v_width,@v_height,@v_framerate,@a_count,@a_format,@a_bitrate)",
                                                              {New MySqlParameter("file_id", myRowEntry("id")),
                                                               New MySqlParameter("format", getMediaFormatId(.General.FormatID)),
                                                               New MySqlParameter("duration", .General.DurationMillis),
                                                               New MySqlParameter("v_format", getMediaFormatId(.Video.First.FormatID)),
                                                               New MySqlParameter("v_width", .Video.First.Width),
                                                               New MySqlParameter("v_height", .Video.First.Height),
                                                               New MySqlParameter("v_framerate", .Video.First.FrameRate),
                                                               New MySqlParameter("a_count", .Audio.Count),
                                                               New MySqlParameter("a_format", getMediaFormatId(.Audio.First.FormatID)),
                                                               New MySqlParameter("a_bitrate", .Audio.First.Bitrate)})
                            End With
                        Catch ex As Exception
                            BaseDatos.Errores("[" & entry.FullName & "] " & ex.Message, False)
                        End Try
                    End If

                    myTableFiles.LoadDataRow(BaseDatos.Select(SQL_files & " WHERE id=" & myRowEntry("id").ToString).AsEnumerable.FirstOrDefault.ItemArray, True)

                End If

            Next

        End While

    End Sub

    Private Function createFolderId(path As String, Optional parentId As Object = Nothing, Optional device As Device = Nothing) As Integer

        Dim formatedPath As String = path

        'If fullpath, drive leter is replaced by drive name and get serial        
        If (parentId Is Nothing) Then
            device = getDevice(path)
            formatedPath = Replace(path, GetPathRoot(path), device.name & DirectorySeparatorChar)
        End If

        Dim myTableResult As DataRow = myTableFiles.Select(String.Format("is_folder=1 AND name='{0}' AND ISNULL(parent_id,0)={1}", BaseDatos.QuitaComilla(formatedPath.Split(DirectorySeparatorChar)(0)), If(parentId Is Nothing, 0, parentId))).FirstOrDefault

        'Folder not exists then create
        If (myTableResult Is Nothing) Then
            myTableResult = BaseDatos.Select("INSERT INTO files (name,parent_id,is_folder,device_id) VALUES (@name,@parent_id,1,@device_id); " &
                                             SQL_files & " WHERE id = LAST_INSERT_ID()",
                                             {New MySqlParameter("name", formatedPath.Split(DirectorySeparatorChar)(0)),
                                              New MySqlParameter("parent_id", parentId),
                                              New MySqlParameter("device_id", device.id)}).AsEnumerable.First
            myTableFiles.LoadDataRow(myTableResult.ItemArray, True)
        End If

        ''If device name changes update the new name
        'If (Not myTableResult("name").Equals(formatedPath.Split(DirectorySeparatorChar)(0))) Then
        '    BaseDatos.ExecuteNonQuery("UPDATE files SET name=@name WHERE id=@id",
        '                                  {New MySqlParameter("name", formatedPath.Split(DirectorySeparatorChar)(0)),
        '                                  New MySqlParameter("id", myTableResult("id"))})
        'End If

        'Recursive find id
        If (System.Text.RegularExpressions.Regex.IsMatch(path, ".+\\.+")) Then
            Return createFolderId(path.Substring(path.IndexOf(DirectorySeparatorChar) + 1), CInt(myTableResult("id")), device)
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
            BaseDatos.ExecuteNonQuery("INSERT INTO device (name,serial) VALUES (@name,@serial)",
                                      {New MySqlParameter("name", IO.DriveInfo.GetDrives().First(Function(i) i.Name = GetPathRoot(fullpath)).VolumeLabel),
                                       New MySqlParameter("serial", serial)})
            myTableDevices = BaseDatos.Select(SQL_devices)
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
                BaseDatos.ExecuteNonQuery("INSERT INTO media_format (name) VALUES (@p1)", New MySqlParameter("p1", format))
                myTableMediaFormat = BaseDatos.Select(SQL_media_format)
                Return getMediaFormatId(format)
            End If

        End If

    End Function

#End Region

    Private Sub loadDeviceNames()

        myTableDevices = BaseDatos.Select(SQL_devices)

        uxcmbDeviceNames.DisplayMember = "description"
        uxcmbDeviceNames.ValueMember = "id"
        uxcmbDeviceNames.DataSource = myTableDevices

    End Sub

    Private Sub uxcmbDeviceNames_SelectedIndexChanged(sender As Object, e As EventArgs) Handles uxcmbDeviceNames.SelectedIndexChanged

        myTableFiles = BaseDatos.Select(SQL_files & " WHERE device_id=@device_id", New MySqlParameter("device_id", uxcmbDeviceNames.SelectedValue))

        uxProgress.Maximum = myTableFiles.Select("is_folder=1").Count
        uxProgressLabel.Text = "LOADING......."
        uxState.Refresh()
        uxTreeFolder.BeginUpdate()
        loadTreeInternal_v2(CInt(uxcmbDeviceNames.SelectedValue))
        'loadTreeInternal(uxcmbDeviceNames.SelectedValue)
        uxTreeFolder.EndUpdate()
        uxProgress.Value = 0
        uxProgressLabel.Text = String.Empty

    End Sub

    Private Sub loadTreeInternal(device_id As Object, Optional parent_node As TreeNode = Nothing)

        Dim node As TreeNode
        If (parent_node Is Nothing) Then uxTreeFolder.Nodes.Clear()

        For Each myRow As DataRow In myTableFiles.Select(String.Format("is_folder=1 AND ISNULL(parent_id,0)={0}", If(parent_node Is Nothing, 0, CInt(parent_node.Name))))

            node = New TreeNode(myRow("name").ToString)
            node.Name = myRow("id").ToString
            uxProgress.PerformStep()

            loadTreeInternal(device_id, node)
            If (parent_node Is Nothing) Then
                uxTreeFolder.Nodes.Add(node)
            Else
                parent_node.Nodes.Add(node)
            End If

        Next

    End Sub

    Private Sub loadTreeInternal_v2(device_id As Integer)

        uxTreeFolder.Nodes.Clear()

        Dim stack = New Stack(Of TreeNode)
        Dim node = (From f In myTableFiles.Select("is_folder=1 AND parent_id IS NULL").AsEnumerable Select New TreeNode(f("name").ToString) With {.Name = f("id").ToString}).First
        stack.Push(node)

        While stack.Count > 0
            Dim currentNode = stack.Pop()
            For Each myRow As DataRow In myTableFiles.Select("is_folder=1 AND parent_id=" & currentNode.Name)
                Dim childNode = New TreeNode(myRow("name").ToString) With {.Name = myRow("id").ToString}
                currentNode.Nodes.Add(childNode)
                stack.Push(childNode)
                uxProgress.PerformStep()
            Next
        End While

        uxTreeFolder.Nodes.Add(node)

    End Sub

    Private Sub uxtxtSearch_TextChanged(sender As Object, e As KeyEventArgs) Handles uxtxtSearch.KeyDown
        Static count As Integer = 0

        'search in the treeview for the first match
        If (e.KeyCode = Keys.Enter) Then

            Dim myRow As DataRow = myTableFiles.Select(String.Format("name LIKE '%{0}%'", BaseDatos.QuitaComilla(uxtxtSearch.Text))).Skip(count).FirstOrDefault
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
        For Each myRow As DataRow In myTableFiles.Select(String.Format("parent_id={0}", e.Node.Name)).OrderByDescending(Function(r) r("is_folder"))
            Dim folder As Boolean = (CInt(myRow("is_folder")) = 1)
            Dim item As New ListViewItem({myRow("name").ToString,
                                        If(folder, "DIR", FormatFileSize(CLng(myRow("size")))),
                                        If(Not IsNumeric(myRow("duration")), String.Empty, GetStringFormMillis(CLng(myRow("duration")))),
                                        myRow("v_format").ToString,
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

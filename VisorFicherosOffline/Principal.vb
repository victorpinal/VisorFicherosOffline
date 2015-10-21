Imports MediaInfoNET
Imports MySql.Data.MySqlClient
Imports System.IO.Path
Imports System.Management
Imports VisorFicherosOffline

Public Class Principal

    Private myTableMediaFormat As DataTable
    Private myTableDevices As DataTable
    Private myTableFiles As DataTable

    Private Sub Principal_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        BaseDatos.Check()   'Init db

        'load the imagelist if doesn't exists
        Dim myImageList As New ImageList
        With myImageList
            .Images.Add(My.Resources.carpeta)
        End With
        uxTreeFolder.ImageList = myImageList
        'uxlstDetail.SmallImageList = myImageList        

        myTableFiles = BaseDatos.Select("SELECT * FROM files WHERE 1<>1")
        myTableMediaFormat = BaseDatos.Select("SELECT * FROM media_format")

        loadDeviceNames()   'Load device names in combo

    End Sub

#Region "Load Data"

    Private Sub uxbtnReloadFolder_Click(sender As Object, e As EventArgs) Handles uxbtnReloadFolder.Click

        With New FolderBrowserDialog
            If (.ShowDialog() = DialogResult.OK) Then
                Cursor.Current = Cursors.WaitCursor
                reloadFolder(.SelectedPath)
                loadDeviceNames()
                Cursor.Current = Cursors.Default
            End If
        End With

    End Sub

    Private Sub reloadFolder(selectedPath As String, Optional device As Device = Nothing)

        If (device Is Nothing) Then device = getDevice(selectedPath)
        Dim dinfo As New IO.DirectoryInfo(selectedPath)
        Dim folderId As Integer = getFolderId(selectedPath)

        'load data from db for selectedPath
        'Dim myTableFiles As DataTable = BaseDatos.Select("SELECT * FROM files WHERE parent_id=@parent_id", New MySqlParameter("parent_id", folderId))

        'Delete old nonexisting entries in db
        For Each entry As DataRow In myTableFiles.Select("parent_id=" & folderId).Where(Function(f) (From e In dinfo.GetFileSystemInfos
                                                                                                     Select e.Name).Contains(f.Field(Of String)("name")))
            BaseDatos.ExecuteNonQuery("DELETE FROM files WHERE id=@id", New MySqlParameter("id", entry("id")))
            entry.Delete()
        Next
        myTableFiles.AcceptChanges()

        'Insert new entries        
        For Each entry As IO.FileSystemInfo In dinfo.GetFileSystemInfos().SkipWhile(Function(e) (From f In myTableFiles.AsEnumerable
                                                                                                 Select f.Field(Of String)("name")).Contains(e.Name))
            Try

                'discard system entries
                If (CBool(entry.Attributes And (IO.FileAttributes.Hidden Or IO.FileAttributes.System))) Then Continue For

                'TODO check if file is now a directory, then update

                'check is directory with permisions
                Dim isFolder As Boolean = CBool(entry.Attributes And IO.FileAttributes.Directory)
                If (isFolder) Then CType(entry, IO.DirectoryInfo).GetAccessControl()    'check permissions before insert                

                'insert in db
                Dim newFileRow As DataRow = BaseDatos.Select("INSERT INTO files (name,parent_id,is_folder,size,creation_date,device_id) " &
                                                             "VALUES (@name,@parent_id,@is_folder,@size,@creation_date,@device_id); " &
                                                             "SELECT * FROM files WHERE id = LAST_INSERT_ID()",
                                                             {New MySqlParameter("name", entry.Name),
                                                              New MySqlParameter("parent_id", folderId),
                                                              New MySqlParameter("is_folder", If(isFolder, 1, 0)),
                                                              New MySqlParameter("size", If(isFolder, Nothing, CType(entry, IO.FileInfo).Length)),
                                                              New MySqlParameter("creation_date", entry.CreationTime),
                                                              New MySqlParameter("device_id", device.id)}).AsEnumerable.First
                myTableFiles.LoadDataRow(newFileRow.ItemArray, True)

                'insert mediainfo
                If (Not isFolder AndAlso New String() {".avi", ".mkv", ".mp4", ".wmv", ".mpg", ".mpeg", ".m4v"}.Contains(entry.Extension.ToLower)) Then
                    With New MediaFile(entry.FullName)
                        BaseDatos.ExecuteNonQuery("INSERT INTO media_info (file_id,format,duration,v_format,v_width,v_height,v_framerate,a_count,a_format,a_bitrate) " &
                                                  "VALUES (@file_id,@format,@duration,@v_format,@v_width,@v_height,@v_framerate,@a_count,@a_format,@a_bitrate)",
                                                  {New MySqlParameter("file_id", newFileRow("id")),
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
                End If

                'make subdirs recursive
                If (isFolder) Then reloadFolder(Combine(selectedPath, entry.Name), device)

            Catch ex As Exception
            End Try
        Next

    End Sub

    Private Function getFolderId(path As String, Optional parentId As Object = Nothing, Optional device As Device = Nothing) As Integer

        Dim formatedPath As String = path

        'If fullpath, drive leter is replaced by drive name and get serial
        If (parentId Is Nothing) Then
            device = getDevice(path)
            formatedPath = Replace(path, GetPathRoot(path), device.name & DirectorySeparatorChar)
        End If

        Dim myTableResult As DataRow = myTableFiles.Select(String.Format("is_folder=1 AND name='{0}' AND ISNULL(parent_id,0)={1}", formatedPath.Split(DirectorySeparatorChar)(0), If(parentId Is Nothing, 0, parentId))).FirstOrDefault

        'Folder not exists then create
        If (myTableResult Is Nothing) Then
            myTableResult = BaseDatos.Select("INSERT INTO files (name,parent_id,is_folder,device_id) VALUES (@name,@parent_id,1,@device_id); " &
                                             "SELECT * FROM files WHERE id = LAST_INSERT_ID()",
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
            Return getFolderId(path.Substring(path.IndexOf(DirectorySeparatorChar) + 1), CInt(myTableResult("id")), device)
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
            myTableDevices = BaseDatos.Select("SELECT *, CONCAT(name,' [',serial,']') as description FROM device ORDER BY name")
            Return getDevice(fullpath)
        End If

    End Function

    Private Function GetHDSerialNo(ByVal strDrive As String) As String 'Get HD Serial Number

        'Ensure Valid Drive Letter Entered, Else, Default To C
        If strDrive = "" OrElse strDrive Is Nothing Then strDrive = "C"
        'Make Use Of Win32_LogicalDisk To Obtain Hard Disk Properties
        Dim moHD As New ManagementObject("Win32_LogicalDisk.DeviceID=""" + strDrive + ":""")
        'Get Info
        moHD.[Get]()
        'Get Serial Number
        Return moHD("VolumeSerialNumber").ToString()

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
                myTableMediaFormat = BaseDatos.Select("SELECT * FROM media_format")
                Return getMediaFormatId(format)
            End If

        End If

    End Function

#End Region

    Private Sub loadDeviceNames()

        myTableDevices = BaseDatos.Select("SELECT *, CONCAT(name,' [',serial,']') as description FROM device ORDER BY name")

        uxcmbDeviceNames.DisplayMember = "description"
        uxcmbDeviceNames.ValueMember = "id"
        uxcmbDeviceNames.DataSource = myTableDevices

    End Sub

    Private Sub uxcmbDeviceNames_SelectedIndexChanged(sender As Object, e As EventArgs) Handles uxcmbDeviceNames.SelectedIndexChanged

        myTableFiles = BaseDatos.Select("SELECT * FROM Files WHERE device_id=@device_id", New MySqlParameter("device_id", uxcmbDeviceNames.SelectedValue))

        uxTreeFolder.BeginUpdate()
        loadTreeInternal(uxcmbDeviceNames.SelectedValue, Nothing)
        uxTreeFolder.EndUpdate()

    End Sub

    Private Sub loadTreeInternal(device_id As Object, parent_node As TreeNode)

        Dim node As TreeNode
        If (parent_node Is Nothing) Then uxTreeFolder.Nodes.Clear()

        For Each myRow As DataRow In myTableFiles.Select(String.Format("device_id={0} AND ISNULL(parent_id,0)={1} AND is_folder=1", device_id, If(parent_node Is Nothing, 0, CInt(parent_node.Name))))

            node = New TreeNode(myRow("name").ToString)
            node.Name = myRow("id").ToString
            If (parent_node Is Nothing) Then
                uxTreeFolder.Nodes.Add(node)
            Else
                parent_node.Nodes.Add(node)
            End If
            loadTreeInternal(device_id, node)

        Next

    End Sub

    Private Sub uxtxtSearch_TextChanged(sender As Object, e As EventArgs) Handles uxtxtSearch.TextChanged

        'search in the treeview

    End Sub

    Private Sub uxTreeFolder_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles uxTreeFolder.AfterSelect

        uxlstDetail.BeginUpdate()

        uxlstDetail.Items.Clear()
        For Each myRow As DataRow In myTableFiles.Select(String.Format("parent_id={0} AND is_folder=0", e.Node.Name))
            uxlstDetail.Items.Add(New ListViewItem({myRow("name").ToString, myRow("size").ToString, myRow("creation_date").ToString}))
        Next
        uxColumnSize.Width = -1
        uxColumnDate.Width = -1

        uxlstDetail.EndUpdate()

    End Sub

End Class

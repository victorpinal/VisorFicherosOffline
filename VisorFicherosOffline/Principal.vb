Imports MySql.Data.MySqlClient
Imports System.IO.Path
Imports System.Management

Public Class Principal

    Private Sub Principal_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        BaseDatos.Check()   'Init db

        'load the imagelist if doesn't exists
        Dim myImageList As New ImageList
        With myImageList
            .Images.Add(My.Resources.carpeta)
        End With
        uxTreeFolder.ImageList = myImageList
        'uxlstDetail.SmallImageList = myImageList

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

    Private Sub reloadFolder(selectedPath As String)

        Dim dinfo As New IO.DirectoryInfo(selectedPath)
        Try
            If (CBool(dinfo.Attributes And (IO.FileAttributes.Hidden Or IO.FileAttributes.System))) Then Exit Sub
            dinfo.GetAccessControl()
        Catch ex As Exception
            Exit Sub
        End Try
        Dim folderId As Integer = getFolderId(selectedPath)
        'load data from db for selectedPath
        Dim myTableFiles As DataTable = BaseDatos.Select("SELECT * FROM files WHERE parent_id=@p1", New MySqlParameter("p1", folderId))
        'Delete old nonexisting entries in db
        For Each entry As DataRow In myTableFiles.AsEnumerable.Where(Function(f) (From e In dinfo.GetFileSystemInfos
                                                                                  Select e.Name).Contains(f.Field(Of String)("name")))
            BaseDatos.ExecuteNonQuery("DELETE FROM files WHERE id=@p1",
                                      New MySqlParameter("p1", entry("id")))
        Next
        'Insert new entries
        Dim isFolder As Boolean
        For Each entry As IO.FileSystemInfo In dinfo.GetFileSystemInfos().SkipWhile(Function(e) (From f In myTableFiles.AsEnumerable
                                                                                                 Select f.Field(Of String)("name")).Contains(e.Name))
            'TODO check if file is now a directory, then update
            isFolder = CBool(entry.Attributes And IO.FileAttributes.Directory)
            BaseDatos.ExecuteNonQuery("INSERT INTO files (name,parent_id,is_folder,size) VALUES (@p1,@p2,@p3,@p4)",
                              {New MySqlParameter("p1", entry.Name), New MySqlParameter("p2", folderId), New MySqlParameter("p3", If(isFolder, 1, 0)), New MySqlParameter("p4", If(isFolder, Nothing, CType(entry, IO.FileInfo).Length))})
            'Por recursión updateamos los subdirectorios
            If (isFolder) Then reloadFolder(Combine(selectedPath, entry.Name))
        Next

    End Sub

    Private Function getFolderId(path As String, Optional parentId As Object = Nothing) As Integer

        'If fullpath, drive leter is replaced by drive name and get serial
        Dim serial As Object = Nothing
        Dim formatedPath As String = path
        If (parentId Is Nothing) Then
            serial = GetHDSerialNo(path.Split(":"c)(0))
            formatedPath = Replace(path, GetPathRoot(path), IO.DriveInfo.GetDrives().First(Function(i) i.Name = GetPathRoot(path)).VolumeLabel & DirectorySeparatorChar)
        End If

        Dim myTableResult As DataTable = BaseDatos.Select("SELECT id,name FROM files WHERE is_folder=1 AND ((serial IS NULL AND name=@p1) OR serial=@p3) AND IFNULL(parent_id,0)=IFNULL(@p2,0)",
                                                          {New MySqlParameter("p1", formatedPath.Split(DirectorySeparatorChar)(0)), New MySqlParameter("p2", parentId), New MySqlParameter("p3", serial)})

        If (myTableResult.Rows.Count = 0) Then 'Folder not exists the create and recheck
            BaseDatos.ExecuteNonQuery("INSERT INTO files (name,parent_id,is_folder,serial) VALUES (@p1,@p2,1,@p3)",
                                      {New MySqlParameter("p1", formatedPath.Split(DirectorySeparatorChar)(0)), New MySqlParameter("p2", parentId), New MySqlParameter("p3", serial)})
            Return getFolderId(path, parentId)
        Else
            'If device name changes update the new name
            If (Not myTableResult.Rows(0)("name").Equals(formatedPath.Split(DirectorySeparatorChar)(0))) Then
                BaseDatos.ExecuteNonQuery("UPDATE files SET name=@p1 WHERE id=@p2",
                                          {New MySqlParameter("p1", formatedPath.Split(DirectorySeparatorChar)(0)), New MySqlParameter("p2", myTableResult.Rows(0)("id"))})
            End If
            'Recursive find id
            If (System.Text.RegularExpressions.Regex.IsMatch(path, ".+\\.+")) Then
                Return getFolderId(path.Substring(path.IndexOf(DirectorySeparatorChar) + 1), CInt(myTableResult.Rows(0)("id")))
            Else
                Return CInt(myTableResult.Rows(0)("id"))
            End If
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

#End Region

    Private Sub loadDeviceNames()

        uxcmbDeviceNames.DisplayMember = "name"
        uxcmbDeviceNames.ValueMember = "id"
        uxcmbDeviceNames.DataSource = BaseDatos.Select("SELECT * FROM files WHERE parent_id IS NULL ORDER BY name")

    End Sub

    Private Sub uxcmbDeviceNames_SelectedIndexChanged(sender As Object, e As EventArgs) Handles uxcmbDeviceNames.SelectedIndexChanged

        loadTree(uxcmbDeviceNames.SelectedValue)

    End Sub

    Private Sub loadTree(parent_id As Object)

        uxTreeFolder.BeginUpdate()
        loadTreeInternal(parent_id, Nothing)
        uxTreeFolder.EndUpdate()

    End Sub

    Private Sub loadTreeInternal(parent_id As Object, parent_node As TreeNode)

        Dim node As TreeNode
        If (parent_node Is Nothing) Then uxTreeFolder.Nodes.Clear()

        For Each myRow As DataRow In BaseDatos.Select("SELECT * FROM files WHERE parent_id=@p1 AND is_folder=1", New MySqlParameter("p1", parent_id)).Rows

            node = New TreeNode(myRow("name").ToString)
            'node.ImageIndex = If(CBool(myRow("is_folder")), 0, 1)
            node.Name = myRow("id").ToString
            If (parent_node Is Nothing) Then
                uxTreeFolder.Nodes.Add(node)
            Else
                parent_node.Nodes.Add(node)
            End If
            loadTreeInternal(myRow("id"), node)

        Next

    End Sub

    Private Sub uxtxtSearch_TextChanged(sender As Object, e As EventArgs) Handles uxtxtSearch.TextChanged

        'search in the treeview

    End Sub

    Private Sub uxTreeFolder_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles uxTreeFolder.AfterSelect

        uxlstDetail.BeginUpdate()

        uxlstDetail.Items.Clear()
        For Each myRow As DataRow In BaseDatos.Select("SELECT * FROM files WHERE parent_id=@p1 AND is_folder=0", New MySqlParameter("p1", e.Node.Name)).Rows
            uxlstDetail.Items.Add(New ListViewItem({myRow("name").ToString, myRow("size").ToString}))
        Next

        uxlstDetail.EndUpdate()

    End Sub

End Class

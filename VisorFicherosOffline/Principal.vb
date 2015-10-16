Option Strict On

Imports MySql.Data.MySqlClient
Imports System.IO.Path
Imports System.Management

Public Class Principal

    Private Sub Principal_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        BaseDatos.Check()
        loadDeviceNames()

    End Sub

    Private Sub uxbtnReloadFolder_Click(sender As Object, e As EventArgs) Handles uxbtnReloadFolder.Click

        With New FolderBrowserDialog
            If (.ShowDialog() = DialogResult.OK) Then
                reloadFolder(.SelectedPath)
                loadDeviceNames()
            End If
        End With

    End Sub

    Private Sub reloadFolder(selectedPath As String)

        Dim idFolder As Integer = getFolderId(selectedPath)
        'load data from db for selectedPath
        Dim myTableFiles As DataTable = BaseDatos.Select("SELECT * FROM files WHERE parent_id=@p1", New MySqlParameter("p1", idFolder))
        'Delete old nonexisting entries in db
        For Each entry As DataRow In myTableFiles.AsEnumerable.Except(From f In myTableFiles.AsEnumerable
                                                                      Where (From e In IO.Directory.EnumerateFileSystemEntries(selectedPath)
                                                                             Select GetFileName(e)).Contains(f.Field(Of String)("name")))
            BaseDatos.ExecuteNonQuery("DELETE FROM files WHERE id=@p1",
                                      New MySqlParameter("p1", entry("id")))
        Next
        'Insert new entries
        Dim isFolder As Boolean
        For Each entry As String In (From e In IO.Directory.EnumerateFileSystemEntries(selectedPath)
                                     Select GetFileName(e)).Except(From f In myTableFiles.AsEnumerable
                                                                   Select f.Field(Of String)("name"))
            'TODO check if file is now a directory, then update
            isFolder = IO.Directory.Exists(Combine(selectedPath, entry))
            BaseDatos.ExecuteNonQuery("INSERT INTO files (name,parent_id,is_folder) VALUES (@p1,@p2,@p3)",
                                      {New MySqlParameter("p1", entry), New MySqlParameter("p2", idFolder), New MySqlParameter("p3", If(isFolder, 1, 0))})
            'Por recursión updateamos los subdirectorios
            If (isFolder) Then reloadFolder(Combine(selectedPath, entry))
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

        If (myTableResult.Rows.Count = 0) Then 'Si no existe el directorio lo creamos y volvemos a comprobar
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
            If (path.Contains(DirectorySeparatorChar)) Then
                Return getFolderId(path.Substring(path.IndexOf(DirectorySeparatorChar) + 1), CInt(myTableResult.Rows(0)("id")))
            Else
                Return CInt(myTableResult.Rows(0)("id"))
            End If
        End If

    End Function

    Private Function GetHDSerialNo(ByVal strDrive As String) As String 'Get HD Serial Number

        'Ensure Valid Drive Letter Entered, Else, Default To C
        If strDrive = "" OrElse strDrive Is Nothing Then

            strDrive = "C"

        End If

        'Make Use Of Win32_LogicalDisk To Obtain Hard Disk Properties
        Dim moHD As New ManagementObject("Win32_LogicalDisk.DeviceID=""" + strDrive + ":""")

        'Get Info
        moHD.[Get]()

        'Get Serial Number
        Return moHD("VolumeSerialNumber").ToString()

    End Function

    Private Sub loadDeviceNames()

        uxcmbDeviceNames.DisplayMember = "name"
        uxcmbDeviceNames.ValueMember = "id"
        uxcmbDeviceNames.DataSource = BaseDatos.Select("SELECT * FROM files WHERE parent_id IS NULL ORDER BY name")

    End Sub

    Private Sub uxcmbDeviceNames_SelectedIndexChanged(sender As Object, e As EventArgs) Handles uxcmbDeviceNames.SelectedIndexChanged

        loadTree(uxcmbDeviceNames.SelectedValue)

    End Sub

    Private Sub loadTree(parent_id As Object, Optional parent_node As TreeNode = Nothing)

        Dim node As TreeNode
        If (parent_node Is Nothing) Then uxTree.Nodes.Clear()

        For Each myRow As DataRow In BaseDatos.Select("SELECT * FROM files WHERE parent_id=@p1", New MySqlParameter("p1", parent_id)).Rows

            node = New TreeNode(myRow("name").ToString)
            If (parent_node Is Nothing) Then
                uxTree.Nodes.Add(node)
            Else
                parent_node.Nodes.Add(node)
            End If
            If (CBool(myRow("is_folder"))) Then loadTree(myRow("id"), node)

        Next

    End Sub
End Class

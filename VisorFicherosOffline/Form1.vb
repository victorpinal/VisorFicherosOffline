Imports MySql.Data.MySqlClient
Imports System.IO.Path

Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BaseDatos.Check()       'Comprobamos la base de datos
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim folder As String
        With New FolderBrowserDialog
            If (.ShowDialog() = DialogResult.OK) Then
                folder = .SelectedPath
            End If
        End With
    End Sub

    Private Function getFolderId(path As String, parentId As Integer) As Integer
        Dim myTableResult As DataTable = Nothing
        If (path.Contains(DirectorySeparatorChar)) Then
            myTableResult = BaseDatos.Select("SELECT id FROM files WHERE isFolder=1 AND name=@p1 AND ifnull(parent_id,0)=@p2", {New MySqlParameter("p1", path.Substring(0, path.IndexOf(DirectorySeparatorChar))), New MySqlParameter("p2", parentId)})
            getFolderId(path.Substring(path.IndexOf(DirectorySeparatorChar)), 0)
        Else
            myTableResult = BaseDatos.Select("SELECT id FROM files WHERE isFolder=1 AND name=@p1 AND ifnull(parent_id,0)=@p2", {New MySqlParameter("p1", path), New MySqlParameter("p2", parentId)})
        End If
        Return If(myTableResult.Rows.Count > 0, myTableResult.Rows(0)("id"), 0)
    End Function

End Class

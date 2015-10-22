Public Class DirectoryInfo

    Property info As IO.DirectoryInfo
    Property id As Integer

    Shared Function getFilesCount(path As String) As Integer

        Dim count As Integer = 0

        Dim stack As New Stack(Of IO.DirectoryInfo)
        stack.Push(New IO.DirectoryInfo(path))

        While stack.Count > 0
            Dim currentDir As IO.DirectoryInfo = stack.Pop()
            Try
                currentDir.GetAccessControl() 'Check permisions
                count += currentDir.GetFiles.Length
                For Each dir As IO.DirectoryInfo In currentDir.GetDirectories
                    stack.Push(dir)
                Next
            Catch ex As Exception
            End Try
        End While

        Return count

    End Function

End Class

Class ListViewItemComparer
    Implements IComparer

    Private column As Integer
    Private order As SortOrder

    Public Sub New()
        column = 0
    End Sub

    Public Sub New(column As Integer, order As SortOrder)
        Me.column = column
        Me.order = order
    End Sub

    Public Function Compare(x As Object, y As Object) As Integer _
                            Implements System.Collections.IComparer.Compare

        Dim returnVal As Integer = -1

        Try
            ' Parse the two objects passed as a parameter as a DateTime.
            Dim firstDate As System.DateTime = DateTime.Parse(CType(x,
                                    ListViewItem).SubItems(column).Text)
            Dim secondDate As System.DateTime = DateTime.Parse(CType(y,
                                      ListViewItem).SubItems(column).Text)
            ' Compare the two dates.
            returnVal = DateTime.Compare(firstDate, secondDate)
            ' If neither compared object has a valid date format, 
            ' compare as a string.
        Catch
            ' Compare the two items as a string.
            returnVal = [String].Compare(CType(x,
                              ListViewItem).SubItems(column).Text, CType(y, ListViewItem).SubItems(column).Text)
        End Try

        If (order = SortOrder.Descending) Then
            ' Invert the value returned by String.Compare.
            returnVal *= -1
        End If

        Return returnVal

    End Function

End Class
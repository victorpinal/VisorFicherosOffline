Public Class Device

    Property id As Integer
    Property name As String
    Property serial As String

    Public Sub New(id As Integer, name As String, serial As String)
        Me.id = id
        Me.name = name
        Me.serial = serial
    End Sub

End Class

Imports System.IO
imports MySql.Data

Public Class BaseDatos

    Private Shared myConection As MySqlClient.MySqlConnection
    Private Shared myCommand As MySqlClient.MySqlCommand

    ''' <summary>
    ''' Comprueba la base de datos sqlite y la tabla "files"
    ''' </summary>
    ''' <remarks></remarks>
    Shared Sub Check()
        Try        
        	If (String.IsNullOrEmpty(My.MySettings.Default.Server)) Then
        		My.MySettings.Default.Server = InputBox("Servidor MySQL?",,"localhost")
                My.MySettings.Default.Port = CInt(InputBox("Puerto",, "3306"))
                My.MySettings.Default.User = InputBox("Usuario")
        		My.MySettings.Default.Password = InputBox("Password")
        	End If
        	
        	dim conb As mySqlClient.MySqlConnectionStringBuilder = New MySqlClient.MySqlConnectionStringBuilder()
            conb.Server = My.MySettings.Default.Server
            conb.Port = CUInt(My.MySettings.Default.Port)
        	conb.UserID = My.MySettings.Default.User
        	conb.Password = My.MySettings.Default.Password
            conb.Database = "peliculas"
            myConection = New MySqlClient.MySqlConnection(conb.ConnectionString)

            myConection.Open()
            myConection.Close()

            myCommand = myConection.CreateCommand()
        Catch ex As Exception
            Errores("BaseDatos:Check:" & ex.Message)
            If (myConection IsNot Nothing AndAlso myConection.State <> ConnectionState.Closed) Then myConection.Close()
        End Try
    End Sub

    ''' <summary>
    ''' Ejecuta una consulta y devuelve la tabla resultante
    ''' </summary>
    ''' <param name="Sql"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function [Select](Sql As String, Optional Param As MySqlClient.MySqlParameter = Nothing) As DataTable
        Dim myTable As New DataTable
        Try
            If (myConection.State = ConnectionState.Closed) Then myConection.Open()
            myCommand.CommandText = Sql
            myCommand.Parameters.Clear()
            If (Param IsNot Nothing) Then myCommand.Parameters.Add(Param)
            myTable.Load(myCommand.ExecuteReader)
        Catch ex As Exception
            Errores("BaseDatos:ExecuteScalar:" & Sql & ":" & ex.Message)
        Finally
            If (myConection.State <> ConnectionState.Closed) Then myConection.Close()
        End Try
        Return myTable
    End Function

    Shared Function [Select](Sql As String, Param() As MySqlClient.MySqlParameter) As DataTable
        Dim myTable As New DataTable
        Try
            If (myConection.State = ConnectionState.Closed) Then myConection.Open()
            myCommand.CommandText = Sql
            myCommand.Parameters.Clear()

            For Each myParam As MySqlClient.MySqlParameter In Param
                myCommand.Parameters.Add(myParam)
            Next
            myTable.Load(myCommand.ExecuteReader)
        Catch ex As Exception
            Errores("BaseDatos:ExecuteScalar:" & Sql & ":" & ex.Message)
        Finally
            If (myConection.State <> ConnectionState.Closed) Then myConection.Close()
        End Try
        Return myTable
    End Function

    ''' <summary>
    ''' Ejecuta una consulta DML y devuelve el numero de filas afectadas
    ''' </summary>
    ''' <param name="Sql"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function ExecuteNonQuery(Sql As String, Optional Param As MySqlClient.MySqlParameter = Nothing) As Integer
        ExecuteNonQuery = 0
        Try
            If (myConection.State = ConnectionState.Closed) Then myConection.Open()
            myCommand.CommandText = Sql
            myCommand.Parameters.Clear
            If (Param IsNot Nothing) Then myCommand.Parameters.Add(Param)
            ExecuteNonQuery = myCommand.ExecuteNonQuery
        Catch ex As Exception
            Errores("BaseDatos:ExecuteNonQuery:" & Sql & ":" & ex.Message)
        Finally
            If (myConection.State <> ConnectionState.Closed) Then myConection.Close()
        End Try
    End Function

    Shared Function ExecuteNonQuery(Sql As String, Param() As MySqlClient.MySqlParameter) As Integer
        ExecuteNonQuery = 0
        Try
            If (myConection.State = ConnectionState.Closed) Then myConection.Open()
            myCommand.CommandText = Sql
            myCommand.Parameters.Clear()
            For Each myParam As MySqlClient.MySqlParameter In Param
                myCommand.Parameters.Add(myParam)
            Next
            ExecuteNonQuery = myCommand.ExecuteNonQuery
        Catch ex As Exception
            Errores("BaseDatos:ExecuteNonQuery:" & Sql & ":" & ex.Message)
        Finally
            If (myConection.State <> ConnectionState.Closed) Then myConection.Close()
        End Try
    End Function

    ''' <summary>
    ''' Ejecuta una consulta DML y devuelve un valor (p.ej. el id insertado)
    ''' </summary>
    ''' <param name="Sql"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function ExecuteScalar(Sql As String, Param() As MySqlClient.MySqlParameter) As Integer
        ExecuteScalar = Nothing
        Try
            If (myConection.State = ConnectionState.Closed) Then myConection.Open()
            myCommand.CommandText = Sql
            myCommand.Parameters.Clear()
            For Each myParam As MySqlClient.MySqlParameter In Param
                myCommand.Parameters.Add(myParam)
            Next
            ExecuteScalar = CInt(myCommand.ExecuteScalar)
        Catch ex As Exception
            Errores("BaseDatos:ExecuteScalar:" & Sql & ":" & ex.Message)
        Finally
            If (myConection.State <> ConnectionState.Closed) Then myConection.Close()
        End Try
    End Function

    Shared Function QuitaComilla(str As String) As String
        Return str.Replace("'", "''")
    End Function

    Shared Sub Errores(str As String, Optional showError As Boolean = True)
        If (showError) Then MsgBox(str)
        Using outfile As New StreamWriter(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), My.Application.Info.Title & ".log"), True)
            outfile.WriteLine(Now.ToString & vbTab & str)
        End Using
    End Sub

End Class

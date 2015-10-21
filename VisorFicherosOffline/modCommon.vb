﻿Imports System.Management

Module modCommon

    Public Const KB As Long = 1024
    Public Const MB As Long = 1024 * 1024
    Public Const GB As Long = 1024 * 1024 * 1024
    Public Const MIN As Integer = 60
    Public Const HR As Integer = 60 * 60

    Function FormatFileSize(ByVal size As Long) As String
        If size > 0 Then
            Dim s As String = ""
            Select Case size
                Case 0 To 1024
                    s = size.ToString + " Bytes"

                Case KB To 100 * KB
                    s = (size / KB).ToString("#,###") + " KB"

                Case 100 * KB To MB
                    s = (size / KB).ToString("#,###") + " KB"

                Case MB To 100 * MB
                    s = (size / MB).ToString("#,###") + " MB"

                Case 100 * MB To GB
                    s = (size / MB).ToString("#,###") + " MB"

                Case GB To 100 * GB
                    s = (size / GB).ToString("#,###.#") + " GB"

                Case Else
                    s = (size / GB).ToString("#,###") + " GB"
            End Select
            Return s
        Else
            Return ""
        End If
    End Function

    'Function ParseTimeSpan(ByVal TimeString As String) As Long
    '    Dim millis As Long = 0

    '    If TimeString.Contains(":") Then
    '        Dim time As New TimeSpan
    '        If TimeSpan.TryParse(TimeString, time) Then
    '            millis = Math.Floor(time.TotalMilliseconds)
    '        End If
    '    Else
    '        Dim time As Double
    '        If Double.TryParse(TimeString, time) Then
    '            Dim k As Long = Math.Floor(time * 1000)
    '            millis = Math.Floor(time * 1000)
    '        End If
    '    End If

    '    Return millis
    'End Function

    'Function GetMillisFromString(ByVal TimeString As String) As Long
    '    Dim millis As Long = 0

    '    If TimeString.Contains(":") Then
    '        Dim time As New TimeSpan
    '        If TimeSpan.TryParse(TimeString, time) Then
    '            millis = Math.Floor(time.TotalMilliseconds)
    '        End If
    '    Else
    '        Dim time As Double
    '        If Double.TryParse(TimeString, time) Then
    '            Dim k As Long = Math.Floor(time * 1000)
    '            millis = Math.Floor(time * 1000)
    '        End If
    '    End If

    '    Return millis
    'End Function

    Function GetStringFormMillis(millis As Long) As String
        Return TimeSpan.FromMilliseconds(millis).ToString
    End Function

    Function GetHDSerialNo(ByVal strDrive As String) As String 'Get HD Serial Number

        'Ensure Valid Drive Letter Entered, Else, Default To C
        If strDrive = "" OrElse strDrive Is Nothing Then strDrive = "C"
        'Make Use Of Win32_LogicalDisk To Obtain Hard Disk Properties
        Dim moHD As New ManagementObject("Win32_LogicalDisk.DeviceID=""" + strDrive + ":""")
        'Get Info
        moHD.[Get]()
        'Get Serial Number
        Return moHD("VolumeSerialNumber").ToString()

    End Function

End Module

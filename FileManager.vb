Imports System.IO

Module FileManager
    Const NEW_LINE_ASCII_CODE As Integer = 10

    Sub FileWrite(Line As String, FilePath As String)
        'Attempts to append text (Line) to file (FilePath), and handles exceptions
        Try
            File.AppendAllText(FilePath, Line & Chr(NEW_LINE_ASCII_CODE))
        Catch ex As Exception
            If ex.GetType.ToString() = "System.IO.DirectoryNotFoundException" Then
                Debug.WriteLine("[FILEWRITE] Directory not found")
            Else
                Debug.WriteLine("[FILEWRITE] Error while writing to file")
            End If
        End Try
    End Sub

    Function FileRead(FileName As String) As List(Of String)
        'Attempts to read file contents and return a List
        'We must handle filenotfound errors when calling the function
        Dim FileContent As New List(Of String)

        For Each line As String In File.ReadAllLines(FileName)
            FileContent.Add(line)
        Next

        Return FileContent
    End Function
End Module

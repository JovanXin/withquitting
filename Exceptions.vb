Module Exceptions
    Public Function tryInt(prompt As String, Optional min As Integer? = Nothing) As Integer

        Dim var As Integer?

        While Not var.HasValue
            slowTypeLine(prompt, TYPING_SPEED)
            Try
                Dim userInput = Console.ReadLine()

                If Not min.HasValue Then
                    Debug.WriteLine("[TryInt] running with no min")
                    var = userInput
                    Exit While
                ElseIf CLng(userInput) > CLng(min) Then
                    var = userInput
                    Exit While
                Else
                    slowTypeNewLine($"Enter a number higher than the minimum of {min}", TYPING_SPEED, ConsoleLogType.GameError)
                End If

            Catch ex As Exception
                Space()
                If ex.GetType().ToString() = "System.InvalidCastException" Then
                    slowTypeNewLine("Enter only a number", TYPING_SPEED, logType:=ConsoleLogType.GameError)
                ElseIf ex.GetType().ToString() = "System.OverflowException" Then
                    slowTypeNewLine("Number too large", TYPING_SPEED, logType:=ConsoleLogType.GameError)
                Else
                    slowTypeNewLine("Error occured, please try again", TYPING_SPEED, ConsoleLogType.GameError)
                End If

            End Try
        End While

        Return var
    End Function

    Sub Space()
        Console.WriteLine("")
    End Sub
End Module

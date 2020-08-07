Module Util
    Enum ConsoleLogType
        Info
        GameError
        Validated
        RequestAction
        LightMessage
    End Enum

    ''' <summary>
    ''' Places a space between each character 
    ''' </summary>
    ''' <param name="message">The message which will be spaced out</param>
    ''' <returns></returns>
    Function spaceOutMessage(message As String) As String
        Dim newMessage = ""

        ' Space the message out
        For Each c In message
            newMessage += c + " "
        Next

        ' Trim the last space
        Return newMessage.Substring(0, newMessage.Length - 1)
    End Function
End Module
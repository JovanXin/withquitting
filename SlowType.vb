''' <summary>
''' A module that contains functions for slowly outputting messages
''' </summary>
''' <remarks>
''' <list type="table">
'''     <listheader>
'''         <term>Function</term>
'''         <description>Function descriptions</description>
'''     </listheader>
'''     <item>
'''         <term>slowType</term>
'''         <description>Slowly types out a sentence, yielding the thread</description>
'''     </item>
'''     <item>
'''         <term>slowTypeNewLine</term>
'''         <description>Slowly types out a sentence, then does a new line character</description>
'''     </item>
'''     <item>
'''         <term>slowTypeLine</term>
'''         <description>Slowly types out a sentence a entire line at a time</description>
'''     </item>
'''     <item>
'''         <term>slowTypeLineNewLine</term>
'''         <description>Slowly types out a sentence a entire line at a time, then does a new line character</description>
'''     </item>
''' </list>
''' </remarks>
Module SlowType
    ''' <summary>
    ''' Slowly types out a sentence, yielding the thread
    ''' </summary>
    ''' <param name="message">The message to spell out slowly</param>
    ''' <param name="interval">Interval between chars in milliseconds</param>
    ''' <param name="logType">Color of console text when logging</param>
    ''' <example><code>
    ''' ' Types "Hello World" over 3 seconds
    ''' Util.slowType("Hello World", 300)</code></example>
    ''' 

    Public Sub slowType(message As String, interval As Integer, Optional logType As ConsoleLogType = ConsoleLogType.Info)
        ' Save the current console colour to reset back to it later
        Dim currentColour = Console.ForegroundColor

        ' Change the console colour to the new color
        Select Case logType
            Case ConsoleLogType.GameError
                Console.ForegroundColor = ConsoleColor.Red
            Case ConsoleLogType.Info
                Console.ForegroundColor = ConsoleColor.Yellow
            Case ConsoleLogType.Validated
                Console.ForegroundColor = ConsoleColor.Green
            Case ConsoleLogType.RequestAction
                Console.ForegroundColor = ConsoleColor.Cyan
            Case ConsoleLogType.LightMessage
                Console.ForegroundColor = ConsoleColor.White
            Case Else
                Throw New ApplicationException("logType is invalid: " & logType.ToString())
        End Select

        For Each letter In message
            Console.Write(letter)
            Threading.Thread.Sleep(interval)
        Next

        ' Clear the console buffer
        While Console.KeyAvailable
            Console.ReadKey(True)
        End While

        Console.ForegroundColor = currentColour
    End Sub

    ''' <summary>
    ''' Slowly types out a sentence, yielding the thread, then does a new line character
    ''' </summary>
    ''' <param name="message">The message to type</param>
    ''' <param name="interval">The interval in milliseconds between each character</param>
    ''' <param name="logType">The colour that should be used when logging</param>
    Public Sub slowTypeNewLine(message As String, interval As Integer, Optional logType As ConsoleLogType = ConsoleLogType.Info)
        slowType(message, interval, logType)
        Console.WriteLine()
    End Sub

    ''' <summary>
    ''' Slowly types out a sentence one line at a time
    ''' </summary>
    ''' <param name="message">The message to type</param>
    ''' <param name="interval">The interval in milliseconds between each character</param>
    ''' <param name="logType">The colour that should be used when logging</param>

    Public Sub slowTypeLine(message As String, interval As Integer, Optional logType As ConsoleLogType = ConsoleLogType.Info)
        ' Save the current console colour to reset back to it later
        Dim currentColour = Console.ForegroundColor

        ' Change the console colour to the new color
        Select Case logType
            Case ConsoleLogType.GameError
                Console.ForegroundColor = ConsoleColor.Red
            Case ConsoleLogType.Info
                Console.ForegroundColor = ConsoleColor.Yellow
            Case ConsoleLogType.Validated
                Console.ForegroundColor = ConsoleColor.Green
            Case ConsoleLogType.RequestAction
                Console.ForegroundColor = ConsoleColor.Cyan
            Case Else
                Throw New ApplicationException("logType is invalid: " & logType.ToString())
        End Select

        For Each line In message.Split(CChar(Environment.NewLine))
            Console.Write(line)
            Threading.Thread.Sleep(interval)
        Next

        ' Clears console buffer
        While Console.KeyAvailable
            Console.ReadKey(True)
        End While

        Console.ForegroundColor = currentColour
    End Sub

    ''' <summary>
    ''' Slowly types out a sentence, but does lines at a time, yielding the main thread, and then does a new line character
    ''' </summary>
    ''' <param name="message">The message to spell out slowly</param>
    ''' <param name="interval">The interval in milliseconds between each character</param>
    ''' <param name="logType">The colour that should be used when logging</param>
    Public Sub slowTypeLineNewLine(message As String, interval As Integer, Optional logType As ConsoleLogType = ConsoleLogType.Info)
        slowTypeLine(message, interval, logType)
        Console.WriteLine()
    End Sub
End Module
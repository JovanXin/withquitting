Module MathQuiz
    Dim symbols() As String = {"*", "+", "-"}
    Const INVALID_SPACE As String = " "
    Dim generator As New Random

    Function PlayMathQuiz(numOfQuestions As Integer) As Integer
        Dim userScore As Integer = 0
        Dim minNum As Integer = tryInt("Enter the smallest number: ")
        Dim maxNum As Integer = tryInt("Enter the largest number: ", minNum)
        slowTypeNewLine("Press 'q' to quit the quiz at any time (result still saved)", TYPING_SPEED)
        Space()

        'Iterate through number of questions
        For currentNum As Integer = 1 To numOfQuestions
            slowTypeNewLine($"Question {currentNum}/{numOfQuestions}", TYPING_SPEED, logType:=ConsoleLogType.Validated)

            'Generate the question
            Dim firstNum As Integer = generateNum(minNum, maxNum)
            Dim secondNum As Integer = generateNum(minNum, maxNum)
            Dim symbol As String = symbols(generateNum(0, symbols.Length - 1))
            Dim question As String = $"{firstNum} {symbol} {secondNum} = "

            slowTypeLine(question, TYPING_SPEED)
            Dim userAnswer As String = Console.ReadLine() 'Get answer from user
            If LCase(userAnswer).Trim() = "q" Then
                Exit For
            End If
            'Only generate answer if user does not quit
            Dim answer As Integer = GetAnswer(firstNum, secondNum, symbol) 'Generate answer
            Space()
            'Validate user answer 
            If userAnswer.Contains(INVALID_SPACE) Then
                slowTypeNewLine($"Incorrect! Correct answer was {answer}, you answered nothing", TYPING_SPEED, logType:=ConsoleLogType.GameError)
            ElseIf answer = userAnswer Then
                userScore += 1
                slowTypeNewLine($"Correct!", TYPING_SPEED, logType:=ConsoleLogType.Validated)
            Else
                slowTypeNewLine($"Incorrect! Correct answer was {answer}, you answered nothing", TYPING_SPEED, logType:=ConsoleLogType.GameError)
            End If
            'Shows user their score
            slowTypeNewLine($"Your score is now {userScore}/{currentNum}", TYPING_SPEED, logType:=ConsoleLogType.Info)
            Space()
        Next

        slowTypeLineNewLine($"Thanks for playing this quiz :)", TYPING_SPEED)
        Return userScore 'Return the user score so we can write to file
    End Function

    Function GetAnswer(firstNum As Integer, secondNum As Integer, symbol As String)
        'This function returns the answer based on the numbers/the symbol
        Dim answer As integer
        If symbol = "*" Then
            answer = firstNum * secondNum
        ElseIf symbol = "+" Then
            answer = firstNum + secondNum
        ElseIf symbol = "-" Then
            answer = firstNum - secondNum
        End If

        Return answer
    End Function

    Function generateNum(lowerBound As Integer, upperBound As Integer) As Integer
        'This function generates a random Integer between two bounds and returns it
        Return generator.Next(lowerBound, upperBound)
    End Function

End Module

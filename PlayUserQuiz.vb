Module PlayUserQuiz
    Function PlayUserQuiz(QuizData As List(Of String)) As Integer
        'Allows user to play a quiz, taking in a list of strings with question/answer seperated
        'by a colon
        Dim UserScore As Integer = 0

        'Iterate over every Question/Answer string and split into question/answer
        For Each QuestionAnswer In QuizData
            Dim QuestionAnswerList = QuestionAnswer.Split(":"c)
            Dim Question As String = QuestionAnswerList(0)
            Dim Answer As String = QuestionAnswerList(1)

            slowTypeLine(Question & ":", TYPING_SPEED)
            'Get answer from user
            Dim UserAnswer As String = Console.ReadLine()

            'If UserAnswer is the same as the answer, then increase user score by 1
            If LCase(UserAnswer.Trim()) = LCase(Answer.Trim()) Then
                UserScore += 1
                slowTypeNewLine($"Correct!", TYPING_SPEED, logType:=ConsoleLogType.Validated)
            Else
                slowTypeNewLine($"Incorrect! Correct answer was {Answer}, you answered {UserAnswer}", TYPING_SPEED, logType:=ConsoleLogType.GameError)
            End If

            slowTypeNewLine($"Your score is now {UserScore}/{QuizData.Count}", TYPING_SPEED, logType:=ConsoleLogType.Info)
            Space()
        Next

        Return UserScore
    End Function
End Module

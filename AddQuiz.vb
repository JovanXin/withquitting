Imports System.IO

Module AddQuiz
    Dim currentDirectory As String = Environment.CurrentDirectory
    Dim quizName As String

    Const QUIZ_NAME_LENGTH As Integer = 16

    ReadOnly nonValidQuizNames() As String = {"CON", "PRN", "AUX", "NUL", "COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8", "COM9", "LPT1", "LPT2", "LPT3", "LPT4", "LPT5", "LPT6", "LPT7", "LPT8", "LPT9", "", " ", "<", ">", ":", """", "/", "\", "\", "|", "?", "*"}
    Public quizzesNamesAndDetails As String = currentDirectory & "\QuizNames.txt"

    '''<summary>
    '''Prompts user to enter a username.
    '''Returns: Name as String.
    '''Takes Parameters: Name As String
    '''</summary>
    Sub CreateQuiz()
        Console.WriteLine("Enter name of the quiz, max length 16: ")
        quizName = Console.ReadLine()
        If quizName.Length > QUIZ_NAME_LENGTH Then 'Checks length of quizname
            slowTypeNewLine($"Please enter a name that is {QUIZ_NAME_LENGTH} characters or less", TYPING_SPEED, ConsoleLogType.GameError)
            CreateQuiz()
        ElseIf nonValidQuizNames.Contains(quizName) Then
            slowTypeNewLine($"Please try again, file cannot be named {quizName}", TYPING_SPEED, ConsoleLogType.GameError)
            CreateQuiz()
        Else
            AddQuestionsToQuiz(quizName)
        End If
        Console.Clear()
    End Sub

    Sub AddQuestionsToQuiz(quizName As String)
        Dim numOfQuestions As Integer = tryInt("Enter the number of questions: ")
        Dim quizDirectory As String = $"{currentDirectory}/UserQuizzes/{quizName}"

        FileWrite($"{quizName}, {numOfQuestions}", $"{currentDirectory}/QuizNames.txt") 'Writing quiz name and questions in file

        Directory.CreateDirectory(quizDirectory)
        File.Create(quizDirectory & $"/{quizName}Scores.txt").Close()
        File.Create(quizDirectory & $"/{quizName}.txt").Close()

        AddQuestions(quizDirectory, numOfQuestions)
    End Sub

    '''<summary>
    '''Asks user for questions and answers they want in their quiz.
    '''Returns: Nothing.
    '''Takes Parameters: QuizDirectory As String, NumOfQuestions As Integer.
    '''</summary>
    Sub AddQuestions(quizDirectory As String, numOfQuestions As Integer)

        For questionNum As Integer = 1 To numOfQuestions
            slowTypeLine($"Question {questionNum}: ", TYPING_SPEED)
            Dim question As String = Console.ReadLine()
            slowTypeLine($"Answer to question {questionNum}: ", TYPING_SPEED)
            Dim answer As String = Console.ReadLine()

            'Since colons are used to seperate the kvp, we must ensure there are none in question/answer
            If question.Contains(":") Or answer.Contains(":") Then
                slowTypeNewLine("Sorry, question/answer can not include :", TYPING_SPEED, logType:=ConsoleLogType.GameError)
                Space()
                questionNum -= 1
            Else
                FileWrite($"{Question}: {Answer}", $"{quizDirectory}/{quizName}.txt") 'We represent a question/answer with a kvp
            End If
        Next

        Console.Clear()
        slowTypeNewLine($"Quiz with {numOfQuestions} has been added", TYPING_SPEED)

    End Sub

End Module
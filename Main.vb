Imports System.IO

Module Project
    'Declare constants
    Const MENU_PROMPT = "   
Enter
-'a' to add a quiz
-'q' to quit
-'s' to show playable quizzes
-'l' to show leaderboard
-'p' to play a quiz: "
    Public Const TYPING_SPEED = 10 'The speed that SlowType will type at
    'Public variables
    Public CurrentDirectory As String = Environment.CurrentDirectory
    Public Username As String

    Sub Menu()
        'A menu that closes when the user presses q
        While True
            slowTypeLine(MENU_PROMPT, TYPING_SPEED)
            Dim userOption As String = Console.ReadLine()

            Space() 'Add space between menu/user option
            If userOption = "a" Then
                AddQuiz.CreateQuiz()
            ElseIf userOption = "q" Then
                Exit While
            ElseIf userOption = "p" Then
                PlayQuiz() 'Let user play a quiz
            ElseIf userOption = "s" Then
                ShowQuizzes()
            ElseIf userOption = "l" Then
                ShowLeaderboardResults()
            Else
                slowTypeNewLine("Sorry, not a valid option", TYPING_SPEED, ConsoleLogType.GameError)
            End If

        End While
    End Sub

    Sub ShowLeaderboardResults()
        'Show leaderboard results and display in nice format for both math/user quizzes
        slowTypeLine("Enter name of quiz (math for math results): ", TYPING_SPEED)

        Dim QuizName As String = Console.ReadLine()

        If LCase(QuizName).Contains("math") Then
            If File.Exists($"{CurrentDirectory}/MathScores.txt") Then
                ShowLeaderboard($"{CurrentDirectory}/MathScores.txt")
            Else
                slowTypeNewLine("Sorry, no math results avaliable", TYPING_SPEED, ConsoleLogType.GameError)
            End If

        ElseIf SelectUserQuiz(QuizName).IsQuizData Then
            ShowLeaderboard($"{CurrentDirectory}/UserQuizzes/{QuizName}/{QuizName}Scores.txt")
        Else
            slowTypeNewLine("Not a valid quiz name/no results avaliable", TYPING_SPEED, logType:=ConsoleLogType.GameError)
        End If


    End Sub

    Sub ShowQuizzes()
        'Iterates through all quizzes made by user
        For Each QuizInfo In FileRead($"{CurrentDirectory}/QuizNames.txt")
            Dim QuizInfoList As String() = QuizInfo.Split(","c)
            slowTypeNewLine($"Quiz name: {QuizInfoList(0)}, Number of questions: {QuizInfoList(1)}", TYPING_SPEED)
        Next
    End Sub

    Sub PlayQuiz()
        'Lets user choose what quiz they will play
        slowTypeLine("Enter the type of quiz you would like to play (math/user): ", TYPING_SPEED)
        Dim QuizType As String = LCase(Console.ReadLine())

        If QuizType.Contains("math") Then
            Space()
            MathQuiz()
        ElseIf QuizType.Contains("user") Then
            UserQuiz()
            Debug.WriteLine("Playing user quiz")
        Else
            slowTypeNewLine("Sorry, not a valid option", TYPING_SPEED, ConsoleLogType.GameError)
        End If
    End Sub

    Function SelectUserQuiz(QuizName) As (IsQuizData As Boolean, QuizData As List(Of String))
        'Iterates through each item in quiznames, and returns quizdata if it exists
        For Each QuizInfo In FileRead($"{CurrentDirectory}/QuizNames.txt")
            Debug.WriteLine($"[SELECTUSERQUIZ] QuizInfo: {QuizInfo}")
            Dim QuizInfoList As String() = QuizInfo.Split(","c)

            If LCase(QuizInfoList(0).Trim()) = LCase(QuizName.Trim()) Then
                Dim QuizData As List(Of String) = FileRead($"{CurrentDirectory}/UserQuizzes/{QuizName}/{QuizName}.txt")
                Return (True, QuizData) 'True represents if the quiz exists or not
            End If
        Next

        Return (False, New List(Of String))

    End Function

    Sub UserQuiz()
        'Let user play a user quiz by selecting the quiz from name
        slowTypeLine("Enter the name of the quiz: ", TYPING_SPEED)
        Dim QuizName As String = Console.ReadLine()
        Dim QuizData = SelectUserQuiz(QuizName)

        'If quiz exists then play questions and save result to a txt file
        If QuizData.IsQuizData Then
            Dim QuizQuestionsData As List(Of String) = QuizData.QuizData
            Dim UserScore As Integer = PlayUserQuiz.PlayUserQuiz(QuizQuestionsData)
            Dim CurrentDate As String = System.DateTime.Today()

            FileWrite($"{CurrentDate}:{Username}:{UserScore}:{QuizQuestionsData.Count}", $"{CurrentDirectory}/UserQuizzes/{QuizName}/{QuizName}Scores.txt")

        Else
            slowTypeNewLine("Not a valid quiz name", TYPING_SPEED, logType:=ConsoleLogType.GameError)
        End If
    End Sub

    Sub MathQuiz()
        'Let user play the Math Quiz and save the result to a txt file
        Dim NumOfQuestions As Integer = tryInt("Enter the number of questions you'd like to play: ")
        Dim UserScore As Integer = PlayMathQuiz(NumOfQuestions)
        Dim CurrentDate As String = System.DateTime.Today()

        FileWrite($"{CurrentDate}:{Username}:{UserScore}:{NumOfQuestions}", $"{CurrentDirectory}/MathScores.txt")
    End Sub

    Sub GetUsername()
        'Let user enter their username to save in scores, but only without colons/spaces
        slowTypeLine("Please enter your username to get started: ", TYPING_SPEED)
        Username = Console.ReadLine()

        If Username.Contains(":") Then
            slowTypeNewLine("Invalid Character in username, please try again (no colons)", TYPING_SPEED, logType:=ConsoleLogType.GameError)
            GetUsername()
        End If
    End Sub

    Sub Main()
        'If the QuizNames file does not exist, create it
        If Not File.Exists($"{CurrentDirectory}/QuizNames.txt") Then
            File.Create($"{CurrentDirectory}/QuizNames.txt").Close()
        End If

        LoadScreens.Start_Up() 'Display loading screen 'animation'
        GetUsername() 'Get the username to use throughout program
        Menu() 'Display menu until user presses 'q' 
        LoadScreens.Shut_Down() 'Display shut down 'animation' 
    End Sub
End Module

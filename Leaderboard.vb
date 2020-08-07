Module Leaderboard
    'Displays leaderboard to the user
    Sub ShowLeaderboard(fileName As String)
        'Takes in a filename (the leaderboard file for the game), converts to a list
        'Nicely types (using slowType) a good formatted 
        Dim leaderboardResults As List(Of String) = fileRead(fileName)

        For Each LeaderboardItem In LeaderboardResults
            Dim LeaderboardItemSplit = LeaderboardItem.Split(":"c)

            slowTypeNewLine($"Date: {LeaderboardItemSplit(0)}", TYPING_SPEED, logType:=ConsoleLogType.Validated)
            slowTypeNewLine($"Username: {LeaderboardItemSplit(1)}", TYPING_SPEED, logType:=ConsoleLogType.Validated)
            slowTypeNewLine($"Score: {LeaderboardItemSplit(2)}/{LeaderboardItemSplit(3)}", TYPING_SPEED, logType:=ConsoleLogType.Validated)
            Space()
        Next
    End Sub
End Module

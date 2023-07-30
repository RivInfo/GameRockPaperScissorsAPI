namespace GameAPI.GameModels;

public static class GameLogic
{
    public static GameRoundResult ResultFromTwoSubjectSkills(GameSkills firsPlayerSkills, GameSkills secondPlayerSkills)
    {
        if (firsPlayerSkills == secondPlayerSkills)
            return GameRoundResult.Draw;

        if (firsPlayerSkills == GameSkills.Rock && secondPlayerSkills == GameSkills.Scissors)
            return GameRoundResult.WinFirst;

        if (firsPlayerSkills == GameSkills.Scissors && secondPlayerSkills == GameSkills.Paper)
            return GameRoundResult.WinFirst;

        if (firsPlayerSkills == GameSkills.Paper && secondPlayerSkills == GameSkills.Rock)
            return GameRoundResult.WinFirst;

        return GameRoundResult.WinSecond;
    }
}
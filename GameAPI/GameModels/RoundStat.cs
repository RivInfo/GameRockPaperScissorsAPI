using GameAPI.GameModels.Players;

namespace GameAPI.GameModels;

public class RoundStat
{
    public RoundStat(ISubject mainSubject, ISubject secondSubject, int round, GameRoundResult roundResult)
    {
        MainSubject = mainSubject;
        SecondSubject = secondSubject;
        Round = round;
        RoundResult = roundResult;
    }

    public ISubject MainSubject { get; }
    public ISubject SecondSubject { get; }

    public int Round { get; }

    public GameRoundResult RoundResult { get; }
}
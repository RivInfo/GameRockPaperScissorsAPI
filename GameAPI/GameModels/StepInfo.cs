using GameAPI.GameModels.Players;

namespace GameAPI.GameModels;

public class StepInfo
{
    public StepInfo(ISubject mainSubject, ISubject secondSubject, int round, bool isMainSubjectWin)
    {
        MainSubject = mainSubject;
        SecondSubject = secondSubject;
        Round = round;
        IsMainSubjectWin = isMainSubjectWin;
    }

    public ISubject MainSubject { get; init; }
    public ISubject SecondSubject { get; init; }

    public int Round { get; init; }
    
    public bool IsMainSubjectWin { get; init; }
}
using GameAPI.GameModels.Players;

namespace GameAPI.GameModels;

public class StepInfo
{
    public ISubject MainSubject { get; init; }
    public ISubject SecondSubject { get; init; }

    public int Round { get; init; }
    
    public bool IsMainSubjectWin { get; init; }
}
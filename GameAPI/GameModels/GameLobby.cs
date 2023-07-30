using GameAPI.GameModels.Players;
using GameAPI.Services;

namespace GameAPI.GameModels;

public class GameLobby
{
    private readonly ISettingsServices _settingsServices;

    public GameLobby(ISettingsServices settingsServices)
    {
        _settingsServices = settingsServices;
    }
    
    public Guid LobbyId { get; init; }
    
    public Guid GameId { get; set; }
    
    public DateTime GameStartTime { get; set; }
    
    public ISubject? MainSubject { get; set; }
    public ISubject? SecondSubject { get; set; }

    public int Round { get; set; }
    
    public bool MoveMainSubject  { get; set; }
    public bool MoveSecondSubject { get; set; }
    
    public GameSkills MainSubjectSkills { get; set;}
    public GameSkills SecondSubjectSkills { get; set;}
    
    public List<StepInfo> StepsInfo { get; set; }
    
    public bool LobbyIsFull() => MainSubject is not null && SecondSubject is not null;

    public bool TrySetMainSubject(ISubject subject)
    {
        if (MainSubject is not null) return false;
        
        MainSubject = subject;
        return true;
    }

    public bool TrySetSecondSubject(ISubject subject)
    {
        if (TrySetMainSubject(subject)) return true;
        
        if (SecondSubject is not null) return false;
            
        SecondSubject = subject;
        return true;
    }

    public bool TrySubjectTurn(ISubject subject, GameSkills skills)
    {
        if (LobbyIsFull() == false) return false;

        if (Round >= _settingsServices.RoundsCount) return false;
        
        if (MainSubject!.Id == subject.Id && MoveMainSubject == false)
        {
            MainSubjectSkills = skills;
            MoveMainSubject = true;
        }
        else if (SecondSubject!.Id == subject.Id && MoveSecondSubject == false)
        {
            SecondSubjectSkills = skills;
            MoveSecondSubject = true;
        }

        if (MoveSecondSubject && MoveMainSubject)
        {
            RoundResult();
        }

        return false;
    }

    private void RoundResult()
    {
        StepsInfo.Add(new StepInfo(MainSubject, SecondSubject,Round,));
    }
}
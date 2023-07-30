using GameAPI.GameModels.Players;
using GameAPI.Services;

namespace GameAPI.GameModels;

public class GameLobby
{
    private readonly int _maxRoundCounts;

    public GameLobby(int maxRoundCounts)
    {
        _maxRoundCounts = maxRoundCounts;
        GameStartTime = DateTime.UtcNow;
    }
    
    public Guid LobbyId { get; init; }

    public DateTime GameStartTime { get; set; }
    
    public ISubject? MainSubject { get; set; }
    public ISubject? SecondSubject { get; set; }

    public int Round { get; set; }
    
    public bool MoveMainSubject  { get; set; }
    public bool MoveSecondSubject { get; set; }
    
    public GameSkills MainSubjectSkills { get; set;}
    public GameSkills SecondSubjectSkills { get; set;}
    
    public List<RoundStat> RoundsStats { get; set; }
    
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

    public bool TryRemoveSubject(ISubject subject)
    {
        if (MainSubject is not null && MainSubject.Id == subject.Id)
        {
            MainSubject = SecondSubject;
            SecondSubject = null;
            return true;
        }
        
        if (SecondSubject is not null && SecondSubject.Id == subject.Id)
        {
            SecondSubject = null;
            return true;
        }

        return false;
    }

    public bool TrySubjectTurn(ISubject subject, GameSkills skills)
    {
        if (LobbyIsFull() == false) return false;

        if (Round >= _maxRoundCounts) return false;

        if (SecondSubject!.IsBot)
        {
            SecondSubjectSkills = (GameSkills)new Random().Next((int)GameSkills.Scissors);
            MoveSecondSubject = true;
        }
        
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
        else
        {
            return false;
        }

        if (MoveSecondSubject && MoveMainSubject)
        {
            RoundResult();
            return true;
        }

        return false;
    }

    public void ResetLobby()
    {
        GameStartTime = DateTime.UtcNow;
        RoundsStats.Clear();
        Round = 0;
        MoveMainSubject = false;
        MoveSecondSubject = false;
    }

    private void RoundResult()
    {
        RoundsStats.Add(new RoundStat(MainSubject!, SecondSubject!,Round, 
            GameLogic.ResultFromTwoSubjectSkills(MainSubjectSkills, SecondSubjectSkills)));

        Round++;
        
        MoveMainSubject = false;
        MoveSecondSubject = false;
    }
}
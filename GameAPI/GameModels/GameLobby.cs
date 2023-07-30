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

    public DateTime GameStartTime { get; private set; }

    public ISubject? MainSubject { get; private set; }
    public ISubject? SecondSubject { get; private set; }

    public int Round { get; private set; }

    public bool MoveMainSubject { get; private set; }
    public bool MoveSecondSubject { get; private set; }

    public GameSkills MainSubjectSkills { get; private set; }
    public GameSkills SecondSubjectSkills { get; private set; }

    public List<RoundStat> RoundsStats { get; } = new();

    public bool LobbyIsFull() => MainSubject is not null && SecondSubject is not null;

    private bool TryRoundMove()
    {
        if (MoveSecondSubject == false || MoveMainSubject == false) return false;

        RoundResult();
        return true;
    }

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

    public bool TryRemoveSubject(long subjectId)
    {
        if (MainSubject is not null && MainSubject.Id == subjectId)
        {
            MainSubject = SecondSubject;
            SecondSubject = null;
            return true;
        }

        if (SecondSubject is not null && SecondSubject.Id == subjectId)
        {
            SecondSubject = null;
            return true;
        }

        return false;
    }

    public bool TrySubjectTurn(long subjectId, GameSkills skills)
    {
        if (LobbyIsFull() == false) return false;

        if (Round >= _maxRoundCounts) return false;

        if (SecondSubject!.IsBot)
        {
            SecondSubjectSkills = (GameSkills)new Random().Next((int)GameSkills.Scissors);
            MoveSecondSubject = true;
        }

        if (MainSubject!.Id == subjectId && MoveMainSubject == false)
        {
            MainSubjectSkills = skills;
            MoveMainSubject = true;
            TryRoundMove();
            return true;
        }

        if (SecondSubject!.Id == subjectId && MoveSecondSubject == false)
        {
            SecondSubjectSkills = skills;
            MoveSecondSubject = true;
            TryRoundMove();
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
        RoundsStats.Add(new RoundStat(MainSubject!, SecondSubject!, Round,
            GameLogic.ResultFromTwoSubjectSkills(MainSubjectSkills, SecondSubjectSkills)));

        Round++;

        MoveMainSubject = false;
        MoveSecondSubject = false;
    }
}
using GameAPI.GameModels.Players;

namespace GameAPI.GameModels;

public class GameLobby
{
    public Guid LobbyId { get; init; }
    
    public Guid GameId { get; set; }
    
    public DateTime StartTime { get; set; }
    
    public ISubject MainSubject { get; set; }
    public ISubject SecondSubject { get; set; }
    
    public int Steps { get; set; }
    
    public bool MoveFirstMainPlayer { get; set; }
    
    public List<StepInfo> StepsInfo { get; set; }
}
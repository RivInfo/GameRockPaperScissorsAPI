using GameAPI.GameModels;
using GameAPI.GameModels.Players;

namespace GameAPI.DataStorage;

public interface IGameLobbysStorage
{
    public (Guid lobbyId, Guid gameId, long playerId) AddLobby(ISubject subject);
    public void DeleteLobby(Guid id);
    public GameLobby GetLobbyInfo(Guid id);
    public DateTime GetStartTime();
    public List<StepInfo> GetLobbyStat(Guid id);
    public (long playerId, Guid gameId) AddSubject(ISubject subject);
    public bool RemoveSubject(ISubject subject);
    public Guid ResetGame(Guid lobbyId);
}
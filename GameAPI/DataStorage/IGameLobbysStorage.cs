using GameAPI.GameModels;
using GameAPI.GameModels.Players;

namespace GameAPI.DataStorage;

public interface IGameLobbysStorage
{
    public (bool isSucces, Guid lobbyId, Guid gameId, long playerId) TryAddLobby(ISubject subject);
    public void DeleteLobby(Guid id);
    public GameLobby GetLobbyInfo(Guid id);
    public DateTime GetStartTime();
    public List<StepInfo> GetLobbyStat(Guid id);
    public (bool isSucces, Guid gameId, long playerId) TryAddSubjectToLobby(Guid lobbyId ,ISubject subject);
    public bool RemoveSubject(ISubject subject);
    public Guid ResetGame(Guid lobbyId);
}
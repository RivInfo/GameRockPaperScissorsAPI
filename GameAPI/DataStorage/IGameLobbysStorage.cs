using GameAPI.GameModels;
using GameAPI.GameModels.Players;

namespace GameAPI.DataStorage;

public interface IGameLobbysStorage
{
    public (bool isSucces, Guid lobbyId, long playerId) TryAddLobby(ISubject subject);
    public bool TryDeleteLobby(Guid lobbyId);
    public GameLobby GetLobbyInfo(Guid lobbyId);
    public IEnumerable<Guid> GetAllLobbysId();
    public DateTime GetStartTime(Guid lobbyId);
    public List<RoundStat> GetLobbyStat(Guid lobbyId);
    public (bool isSucces, long playerId) TryAddSubjectToLobby(Guid lobbyId ,ISubject subject);
    public bool TryRemoveSubjectFromLobby(long subjectId, Guid lobbyId);
    public bool TryResetGame(Guid lobbyId);
    public bool TryTurn(Guid lobbyId, long playerId, GameSkills turn);
}
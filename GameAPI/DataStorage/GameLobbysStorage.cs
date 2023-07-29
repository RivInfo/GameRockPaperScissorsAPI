using GameAPI.GameModels;
using GameAPI.GameModels.Players;

namespace GameAPI.DataStorage;

public class GameLobbysStorage : IGameLobbysStorage
{
    public (Guid lobbyId, Guid gameId, long playerId) AddLobby(ISubject subject)
    {
        throw new NotImplementedException();
    }

    public void DeleteLobby(Guid id)
    {
        throw new NotImplementedException();
    }

    public GameLobby GetLobbyInfo(Guid id)
    {
        throw new NotImplementedException();
    }

    public DateTime GetStartTime()
    {
        throw new NotImplementedException();
    }

    public List<StepInfo> GetLobbyStat(Guid id)
    {
        throw new NotImplementedException();
    }

    public (long playerId, Guid gameId) AddSubject(ISubject subject)
    {
        throw new NotImplementedException();
    }

    public bool RemoveSubject(ISubject subject)
    {
        throw new NotImplementedException();
    }

    public Guid ResetGame(Guid lobbyId)
    {
        throw new NotImplementedException();
    }
}
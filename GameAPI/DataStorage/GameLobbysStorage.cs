using System.Collections.Concurrent;
using GameAPI.GameModels;
using GameAPI.GameModels.Players;
using GameAPI.Services;

namespace GameAPI.DataStorage;

public class GameLobbysStorage : IGameLobbysStorage
{
    private readonly ISettingsServices _settings;
    
    private ConcurrentDictionary<Guid, GameLobby> _gameLobbys = new();
    
    public GameLobbysStorage(ISettingsServices settings)
    {
        _settings = settings;
    }

    public (bool isSucces, Guid lobbyId, Guid gameId, long playerId) TryAddLobby(ISubject subject)
    {
        GameLobby gameLobby = new(_settings)
        {
            LobbyId = Guid.NewGuid(),
            MainSubject = subject,
            GameId = Guid.NewGuid()
        };
        _gameLobbys.TryAdd(gameLobby.LobbyId, gameLobby);
        return (true, gameLobby.LobbyId, gameLobby.GameId , subject.Id);
    }
    
    public (bool isSucces, Guid gameId, long playerId) TryAddSubjectToLobby(Guid lobbyId ,ISubject subject)
    {
        if (_gameLobbys.ContainsKey(lobbyId))
        {
            _gameLobbys[lobbyId].SecondSubject = subject;
            return (true, _gameLobbys[lobbyId].GameId, subject.Id);
        }

        return (false, Guid.Empty, 0);
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

    public bool RemoveSubject(ISubject subject)
    {
        throw new NotImplementedException();
    }

    public Guid ResetGame(Guid lobbyId)
    {
        throw new NotImplementedException();
    }
}
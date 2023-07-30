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

    public (bool isSucces, Guid lobbyId, long playerId) TryAddLobby(ISubject subject)
    {
        GameLobby gameLobby = new(_settings.RoundsCount)
        {
            LobbyId = Guid.NewGuid()
        };
        gameLobby.TrySetMainSubject(subject);
        _gameLobbys.TryAdd(gameLobby.LobbyId, gameLobby);
        return (true, gameLobby.LobbyId, subject.Id);
    }

    public List<RoundStat>? GetLobbyStat(Guid lobbyId)
    {
        if (_gameLobbys.ContainsKey(lobbyId))
        {
            return _gameLobbys[lobbyId].RoundsStats;
        }

        return null;
    }

    public (bool isSucces, long playerId) TryAddSubjectToLobby(Guid lobbyId ,ISubject subject)
    {
        if (_gameLobbys.ContainsKey(lobbyId))
        {
            if(_gameLobbys[lobbyId].TrySetSecondSubject(subject))
                return (true, subject.Id);
        }

        return (false, 0);
    }

    public bool TryRemoveSubjectFromLobby(ISubject subject, Guid lobbyId)
    {
        if (_gameLobbys.ContainsKey(lobbyId))
        {
            return _gameLobbys[lobbyId].TryRemoveSubject(subject);
        }

        return false;
    }

    public bool TryResetGame(Guid lobbyId)
    {
        if (_gameLobbys.ContainsKey(lobbyId))
        {
            _gameLobbys[lobbyId].ResetLobby();
            return true;
        }

        return false;
    }

    public bool TryDeleteLobby(Guid lobbyId)
    {
        if (_gameLobbys.ContainsKey(lobbyId))
            return _gameLobbys.TryRemove(lobbyId, out GameLobby _);

        return false;
    }

    public GameLobby? GetLobbyInfo(Guid lobbyId)
    {
        if (_gameLobbys.ContainsKey(lobbyId))
        {
            return _gameLobbys[lobbyId];
        }

        return null;
    }

    public IEnumerable<Guid> GetAllLobbysId()
    {
        return _gameLobbys.Select(x => x.Key);
    }

    public DateTime GetStartTime(Guid lobbyId)
    {
        if (_gameLobbys.ContainsKey(lobbyId))
        {
            return _gameLobbys[lobbyId].GameStartTime;
        }

        return new DateTime();
    }
}
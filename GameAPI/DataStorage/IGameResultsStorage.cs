using GameAPI.GameModels;

namespace GameAPI.DataStorage;

public interface IGameResultsStorage
{
    public GameStepsContainer GetGameStat(Guid gameId);
}
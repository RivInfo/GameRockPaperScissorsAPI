namespace GameAPI.Services;

public interface ISettingsServices
{
    public int TimeDelayCheckingGameLobbys { get; }
    public int TimeToRemoveLobbys { get; }
    public int RoundsCount { get; }
}
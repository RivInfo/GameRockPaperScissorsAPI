using GameAPI.Options;
using Microsoft.Extensions.Options;

namespace GameAPI.Services;

public class SettingsServices : ISettingsServices
{
    private readonly ServiceSettings _settings;

    public SettingsServices(IOptions<ServiceSettings> settings)
    {
        _settings = settings.Value;
        TimeDelayCheckingGameLobbys = _settings.TimeDelayCheckingGameLobbys;
        TimeToRemoveLobbys = _settings.TimeToRemoveLobbys;
        RoundsCount = _settings.RoundsCount;
    }

    public int TimeDelayCheckingGameLobbys { get; }
    public int TimeToRemoveLobbys { get; }
    public int RoundsCount { get; }
}
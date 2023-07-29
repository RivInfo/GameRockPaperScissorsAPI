using GameAPI.Services;

namespace GameAPI.Background;

public class GameLobbysBackground : BackgroundService
{
    private readonly ISettingsServices _settings;

    public GameLobbysBackground(ISettingsServices settings)
    {
        _settings = settings;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {

            
            await Task.Delay(_settings.TimeDelayCheckingGameLobbys, stoppingToken);
        }
    }
}
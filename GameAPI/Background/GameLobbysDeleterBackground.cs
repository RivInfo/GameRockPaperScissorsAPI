using GameAPI.DataStorage;
using GameAPI.Services;

namespace GameAPI.Background;

public class GameLobbysDeleterBackground : BackgroundService
{
    private readonly ILogger<GameLobbysDeleterBackground> _logger;
    private readonly ISettingsServices _settings;
    private readonly IGameLobbysStorage _lobbysStorage;

    public GameLobbysDeleterBackground(ILogger<GameLobbysDeleterBackground> logger,
        ISettingsServices settings, IGameLobbysStorage lobbysStorage)
    {
        _logger = logger;
        _settings = settings;
        _lobbysStorage = lobbysStorage;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            DateTime nowTime = DateTime.UtcNow;

            TimeSpan minNonDeleteTime = nowTime - nowTime.AddMilliseconds(-_settings.TimeToRemoveLobbys);

            foreach (var lobbyId in _lobbysStorage.GetAllLobbysId())
            {
                 _lobbysStorage.GetStartTime(lobbyId);

                 TimeSpan resTime = nowTime - _lobbysStorage.GetStartTime(lobbyId);

                 if (resTime >= minNonDeleteTime)
                 {
                     if(_lobbysStorage.TryDeleteLobby(lobbyId))
                        _logger.LogInformation("Server {0} is remove", lobbyId);
                     else
                         _logger.LogInformation("Server {0} is NOT removing", lobbyId);
                 }
            }

            await Task.Delay(_settings.TimeDelayCheckingGameLobbys, stoppingToken);
        }
    }
}
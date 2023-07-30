using GameAPI.DataStorage;
using GameAPI.GameModels;
using GameAPI.GameModels.Players;
using GameAPI.ResponseModels;
using Microsoft.AspNetCore.Mvc;

namespace GameAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class GameController : Controller
{
    private readonly IGameLobbysStorage _lobbysStorage;

    public GameController(IGameLobbysStorage lobbysStorage)
    {
        _lobbysStorage = lobbysStorage;
    }
    
    [HttpGet("create")]
    public async Task<ActionResult<AddPlayerResponse>> AddLobby(string name)
    {
        ISubject subject = new Player(name);
        var result = _lobbysStorage.TryAddLobby(subject);

        if (result.isSucces == false)
            return NotFound();

        AddPlayerResponse response = new AddPlayerResponse
        {
            LobbyId = result.lobbyId,
            PlayerId = result.playerId
        };
        
        return Ok(response);
    }
    
    [HttpGet("{lobbyId:guid}/join/{name}&{isBot:bool}")]
    public async Task<ActionResult<AddPlayerResponse>> JoinToRoom(Guid lobbyId, string name, bool isBot = false)
    {
        ISubject subject;
        
        if (isBot)
            subject = new Bot(name);
        else
            subject = new Player(name);

        var result = _lobbysStorage.TryAddSubjectToLobby(lobbyId, subject);

        if (result.isSucces == false)
            return NotFound();
        
        AddPlayerResponse response = new AddPlayerResponse
        {
            LobbyId = lobbyId,
            PlayerId = result.playerId
        };

        return Ok(response);
    }
    
    [HttpGet("{lobbyId:guid}/user/{userId:long}/{turn}")]
    public async Task<ActionResult> Turn(Guid lobbyId, long userId, GameSkills turn)
    {
        var result = _lobbysStorage.TryTurn(lobbyId, userId, turn);

        if (result == false)
            return NotFound();

        return Ok();
    }
    
    [HttpGet("{lobbyId:guid}/stat")]
    public async Task<ActionResult> GetStat(Guid lobbyId)
    {
        var result = _lobbysStorage.GetLobbyStat(lobbyId);

        if (result is null)
            return NotFound();

        var response = result
            .Select(x => new RoundStatResponse(x.MainSubject.Name, 
                x.SecondSubject.Name, x.Round, x.RoundResult));

        return Ok(response);
    }
    
    [HttpGet("{lobbyId:guid}/resetGame")]
    public async Task<ActionResult> ResetGame(Guid lobbyId)
    {
        var result = _lobbysStorage.TryResetGame(lobbyId);

        if (result == false)
            return NotFound();

        return Ok();
    }
    
    [HttpGet("{lobbyId:guid}/removeGame")]
    public async Task<ActionResult> RemoveGame(Guid lobbyId)
    {
        var result = _lobbysStorage.TryDeleteLobby(lobbyId);

        if (result == false)
            return NotFound();

        return Ok();
    }
    
    [HttpGet("{lobbyId:guid}/removePlayer/{playerId:long}")]
    public async Task<ActionResult> RemoveGame(Guid lobbyId, long playerId)
    {
        var result = _lobbysStorage.TryRemoveSubjectFromLobby(playerId, lobbyId);

        if (result == false)
            return NotFound();

        return Ok();
    }
}
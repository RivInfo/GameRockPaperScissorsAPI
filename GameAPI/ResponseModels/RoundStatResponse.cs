using System.Text.Json.Serialization;
using GameAPI.GameModels;

namespace GameAPI.ResponseModels;

public class RoundStatResponse
{
    public RoundStatResponse(string player1Name, string player2Name, int round, GameRoundResult roundResult)
    {
        Player1Name = player1Name;
        Player2Name = player2Name;
        Round = round;
        RoundResult = roundResult;
    }

    public string Player1Name { get; }
    public string Player2Name { get; }
    public int Round { get; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public GameRoundResult RoundResult { get; }
}
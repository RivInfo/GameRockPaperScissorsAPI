namespace GameAPI.ResponseModels;

public class AddPlayerResponse
{
    public long PlayerId { get; set; }
    public Guid LobbyId { get; set; }
}
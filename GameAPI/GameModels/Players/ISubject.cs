namespace GameAPI.GameModels.Players;

public interface ISubject
{
    public long Id { get; }
    public string Name { get; }
    public bool IsBot { get; }
}
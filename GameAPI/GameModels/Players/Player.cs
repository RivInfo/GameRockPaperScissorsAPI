namespace GameAPI.GameModels.Players;

public class Player : ISubject
{
    public Player(string name, long guid)
    {
        Name = name;
        Id = guid;
    }

    public long Id { get; }
    public string Name { get; }
    
    public bool IsBot => false;
}
namespace GameAPI.GameModels.Players;

public class Player : ISubject
{
    public Player(string name)
    {
        Name = name;
        Id = new Random().NextInt64(1000000);
    }

    public long Id { get; }
    public string Name { get; }
    
    public bool IsBot => false;
}
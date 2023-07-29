namespace GameAPI.GameModels.Players;

public class Bot : ISubject
{
    public Bot(string name)
    {
        Name = name;
        Id = -1;
    }

    public long Id { get; }
    public string Name { get;}
    
    public bool IsBot => true;
}
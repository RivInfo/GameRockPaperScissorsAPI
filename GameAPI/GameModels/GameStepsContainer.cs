namespace GameAPI.GameModels;

public class GameStepsContainer
{
    public Guid GameId { get; set; }
    public StepInfo[] StepsInfo { get; set; }
}
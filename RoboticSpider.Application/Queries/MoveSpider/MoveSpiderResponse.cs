namespace RoboticSpider.Application.Queries.MoveSpider;

public class MoveSpiderResponse
{
    public string CurrentPositionOfSpider { get; set; }
    public bool IsSuccess { get; set; }

    public string ErrorMessage { get; set; }
}
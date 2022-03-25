using MediatR;

namespace RoboticSpider.Application.Queries.MoveSpider
{
    public class MoveSpiderQuery : IRequest<MoveSpiderResponse>
    {
        public string WallGridSize { get; set; }
        public string CurrentPosition { get; set; }
        public string Moves { get; set; }
    }
}

using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using MediatR;
using RoboticSpider.Application.Helpers;
using RoboticSpider.Domain.Entities;

namespace RoboticSpider.Application.Queries.MoveSpider;

public class MoveSpiderQueryHandler : IRequestHandler<MoveSpiderQuery, MoveSpiderResponse>
{
    // TODO:Exception handling, query validation and testing.
    public Task<MoveSpiderResponse> Handle(MoveSpiderQuery request, CancellationToken cancellationToken)
    {
        // Create empty response model.
        var moveSpiderResponse = new MoveSpiderResponse();
        // Parse position inputs.
        var inputPosition = InputParser.ParsePositionInput(request.CurrentPosition);
        var wallSizeParsed = InputParser.ParseWallSize(request.WallGridSize);

        var positionResult = Position.Create(inputPosition.Item1, inputPosition.Item2, inputPosition.Item3);

        var wallResult = Wall.Create(wallSizeParsed);

        var movesResult = Moves.Create(request.Moves);

        // Combine result
        var combinedResult = Result.Combine(wallResult, movesResult, positionResult);
        if (combinedResult.IsSuccess)
        {
            var newPositionResult = positionResult.Value.Move(wallResult.Value, movesResult.Value);

            if (newPositionResult.IsSuccess)
            {
                moveSpiderResponse.CurrentPositionOfSpider = newPositionResult.Value;
                moveSpiderResponse.ErrorMessage = "";
                moveSpiderResponse.IsSuccess = true;
            }
            else
            {
                moveSpiderResponse.CurrentPositionOfSpider = request.CurrentPosition;
                moveSpiderResponse.IsSuccess = false;
                moveSpiderResponse.ErrorMessage = newPositionResult.Error;
            }
        }
        else
        {
            moveSpiderResponse.CurrentPositionOfSpider = request.CurrentPosition;
            moveSpiderResponse.IsSuccess = false;
            moveSpiderResponse.ErrorMessage = combinedResult.Error;
        }

        return Task.FromResult(moveSpiderResponse);
    }
}
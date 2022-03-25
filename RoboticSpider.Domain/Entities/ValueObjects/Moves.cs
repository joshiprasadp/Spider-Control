using System.Collections.Generic;
using System.Linq;
using CSharpFunctionalExtensions;

namespace RoboticSpider.Domain.Entities
{
    public class Moves : ValueObject
    {
        private readonly string _moves;

        private Moves(string moves)
        {
            _moves = moves;
        }

        public static Result<Moves> Create(string inputMoves)
        {
            return ValidateMoves(inputMoves);
        }

        private static Result<Moves> ValidateMoves(string inputMoves)
        {
            foreach (var move in inputMoves)
            {
                if (!(move == 'F' || move == 'L' || move == 'R'))
                {
                    return Result.Failure<Moves>($"Invalid move {move}");
                }
            }

            return Result.Success(new Moves(inputMoves));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _moves;
        }

        public string GetMoves() => _moves;
    }
}
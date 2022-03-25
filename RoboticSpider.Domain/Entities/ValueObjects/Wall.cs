using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace RoboticSpider.Domain.Entities
{
    public class Wall : ValueObject
    {
        private readonly int[] _points;

        private Wall(int[] points)
        {
            _points = points;
        }

        public static Result<Wall> Create(int[] points)
        {
            if (ValidateWall(points, out var failure)) return failure;

            return Result.Success(new Wall(points));
        }

        public static bool ValidateWall(int[] points, out Result<Wall> failure)
        {
            if (points is null)
            {
                failure = Result.Failure<Wall>("Points are null");
                return true;
            }
            else if (points.Length != 2)
            {
                failure = Result.Failure<Wall>("Wall length must at least 2 coordinates.");
                return true;
            }
            else if (points[0] != 0 && points[1] != 0 && points[0] == points[1])
            {
                failure = Result.Failure<Wall>("Wall must be rectangle!");
                return true;
            }
            else
            {
                failure = null;
                return false;
            }

        }


        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _points;
        }

        public int GetWidth() => _points[0];
        public int GetHeight() => _points[1];
    }
}

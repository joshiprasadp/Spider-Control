using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace RoboticSpider.Domain.Entities
{
    public class Y : ValueObject
    {
        private int _y;

        private Y(int y)
        {
            _y = y;
        }
        public static Result<Y> Create(int x)
        {
            if (x >= 0)
            {
                return Result.Success<Y>(new Y(x));
            }

            return Result.Failure<Y>("Invalid Y coordinate! should be greater than or equals to 0.");
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _y;
        }

        public void Add(int a) => _y += a;
        public void Subtract(int a) => _y -= a;

        public override string ToString()
        {
            return _y.ToString();
        }

        public static bool operator <(Y a, Y b) => a._y < b._y;
        public static bool operator >(Y a, Y b) => a._y > b._y;

        public static bool operator <(Y a, int b) => a._y < b;
        public static bool operator >(Y a, int b) => a._y > b;
    }
}
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace RoboticSpider.Domain.Entities
{
    public class X : ValueObject
    {
        private int _x;

        private X(int x)
        {
            _x = x;
        }
        public static Result<X> Create(int x)
        {
            if (x >= 0)
            {
                return Result.Success<X>(new X(x));
            }

            return Result.Failure<X>("Invalid X coordinate! should be greater than or equals to 0.");
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _x;
        }

        public void Add(int a) => _x += a;
        public void Subtract(int a) => _x -= a;

        public override string ToString()
        {
            return _x.ToString();
        }

        public static bool operator<(X a, X b) => a._x < b._x;
        public static bool operator>(X a, X b) => a._x > b._x;

        public static bool operator <(X a, int b) => a._x < b;
        public static bool operator >(X a, int b) => a._x > b;
    }
}
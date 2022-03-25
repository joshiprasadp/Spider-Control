using CSharpFunctionalExtensions;
using RoboticSpider.Domain.Enums;

namespace RoboticSpider.Domain.Entities
{
    public class Position
    {
        private readonly X _x;
        private readonly Y _y;
        private Directions _direction;
        private readonly int _moveByPosition;

        private Position(X x, Y y, Directions direction, int moveByPosition = 1)
        {
            _x = x;
            _y = y;
            _direction = direction;
            _moveByPosition = moveByPosition;
        }
        public static Result<Position> Create(int x, int y, Directions direction, int moveByPosition = 1)
        {
            var xResult = X.Create(x);
            var yResult = Y.Create(y);
            var combine = Result.Combine(xResult,yResult);  
            if (combine.IsSuccess)
            {
                return Result.Success(new Position(xResult.Value, yResult.Value, direction, moveByPosition));
            }

            return Result.Failure<Position>(combine.Error);
        }

        public void Rotate90Left()
        {
            switch (_direction)
            {
                case Directions.Up:
                    _direction = Directions.Left;
                    break;
                case Directions.Down:
                    _direction = Directions.Right;
                    break;
                case Directions.Right:
                    _direction = Directions.Up;
                    break;
                case Directions.Left:
                    _direction = Directions.Down;
                    break;
            }
        }

        public void Rotate90Right()
        {
            switch (_direction)
            {
                case Directions.Up:
                    _direction = Directions.Right;
                    break;
                case Directions.Down:
                    _direction = Directions.Left;
                    break;
                case Directions.Right:
                    _direction = Directions.Down;
                    break;
                case Directions.Left:
                    _direction = Directions.Up;
                    break;
            }
        }

        public void MoveForward()
        {
            switch (_direction)
            {
                case Directions.Up:
                    _y.Add(_moveByPosition);
                    break;
                case Directions.Down:
                    _y.Subtract(_moveByPosition);
                    break;
                case Directions.Right:
                    _x.Add(_moveByPosition);;
                    break;
                case Directions.Left:
                    _x.Subtract(_moveByPosition);
                    break;
            }
        }

        public bool IsInvalidMove(Wall wall) => (_x < 0 || _x > wall.GetWidth() || _y < 0 || _y > wall.GetHeight());

        public override string ToString()
        {
            return $"{_x} {_y} {_direction}";
        }

        public Result<string> Move(Wall wall, Moves moves)
        {
            foreach (var move in moves.GetMoves())
            {
                switch (move)
                {
                    case 'F':
                        MoveForward();
                        break;
                    case 'L':
                        Rotate90Left();
                        break;
                    case 'R':
                        Rotate90Right();
                        break;
                    default:
                        return Result.Failure<string>($"Invalid Character {move}");
                }

                if (IsInvalidMove(wall))
                {
                    return Result.Failure<string>(
                        $"Oops! Position ({_x}, {_y}) can not be beyond bounderies (0 , 0) and ({wall.GetWidth()} , {wall.GetHeight()})");
                }
            }

            return Result.Success(ToString());
        }
    }
}

using System;
using RoboticSpider.Domain.Enums;

namespace RoboticSpider.Application.Helpers;

public static class InputParser
{
    public static int[] ParseWallSize(string inputString)
    {
        var size = inputString.Split(' ');
        if (int.TryParse(size[0], out int x) && int.TryParse(size[1], out int y))
        {
            return new[] { x, y };
        }

        return new int[] { };
    }

    public static (int, int, Directions, string) ParsePositionInput(string inputString)
    {
        var splitString = inputString.Split(' ');

        if (splitString.Length == 3 && int.TryParse(splitString[0], out var x) && int.TryParse(splitString[1], out var y) && Enum.TryParse<Directions>(splitString[2], out var direction))
        {
            return (x, y, direction, string.Empty);
        }
        return (0, 0, Directions.Up, "Invalid input position!");
    }
}
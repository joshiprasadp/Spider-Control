using System;
using System.Linq;
using FluentAssertions;
using RoboticSpider.Domain.Entities;
using RoboticSpider.Domain.Enums;
using Xunit;

namespace RoboticSpider.Domain.Tests
{
    public class PositionTests
    {
        [Theory]
        [InlineData(7,15,4,10, Directions.Left, "FLFLFRFFLF", "5 7 Right")]
        public void GivenValiddata_ItShouldReturnExpectedPosition(int gridX, int gridY, int startX, int startY, Directions startDirection, string inputMoves, string expectedOutput)
        {
            var position = Position.Create(startX, startY, startDirection).Value;
            var moves = Moves.Create(inputMoves).Value;
            var maxPoints = new [] { gridX, gridY };
            var wall = Wall.Create(maxPoints).Value;

            position.Move(wall, moves);

            position.ToString().Should().Be(expectedOutput);
        }

        [Theory]
        [InlineData(-1,-1, "Invalid X coordinate! should be greater than or equals to 0., Invalid Y coordinate! should be greater than or equals to 0.")]
        [InlineData(0, -1, "Invalid Y coordinate! should be greater than or equals to 0.")]
        [InlineData(-1, 0, "Invalid X coordinate! should be greater than or equals to 0.")]
        public void GivenInvalidStartingPoints_ItShouldReturnErrorMessage(int gridX, int gridY, string errorMessage)
        {
            var position = Position.Create(gridX, gridY, Directions.Down);
            position.Error.Should().Be(errorMessage);
        }

        [Theory]
        [InlineData("FLRS",false)]
        [InlineData("FLRFFR", true)]
        [InlineData("XXS", false)]
        [InlineData("FLRFFSS", false)]
        public void GivenValidAndInvalidMoves_ItShouldReturnExpectedResult(string inputMoves, bool expectedResult)
        {
           var moves = Moves.Create(inputMoves);
           moves.IsSuccess.Should().Be(expectedResult);
        }
    }
}
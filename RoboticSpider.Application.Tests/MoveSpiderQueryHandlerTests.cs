using System;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RoboticSpider.Application.Queries.MoveSpider;
using Xunit;

namespace RoboticSpider.Application.Tests
{
    public class MoveSpiderQueryHandlerTests
    {
        private readonly ServiceProvider _provider;
        public MoveSpiderQueryHandlerTests()
        {
            var serviceCollection = new ServiceCollection();
            var services = new ServiceCollection();
            services.AddMediatR(typeof(MoveSpiderQueryHandler));
            _provider = services.BuildServiceProvider();
        }
        [Fact]
        public void DependencyInjectionTest()
        {
            Func<IMediator> resolver = () => _provider.GetService<IMediator>();
            resolver.Should().NotThrow<Exception>();

            resolver().Should().NotBeNull();
        }

        [Theory]
        [InlineData("7 15", "4 10 Left", "FLFLFRFFLF")]
        public async Task HappyTests(string wallSize, string currentPosition, string moves)
        {
            var mediator = _provider.GetService<IMediator>();
            var response = await mediator.Send(new MoveSpiderQuery
            {
                WallGridSize = wallSize,
                CurrentPosition = currentPosition,
                Moves = moves
            });

            response.Should().NotBeNull();
            response.IsSuccess.Should().BeTrue();
            response.CurrentPositionOfSpider.Should().Be("5 7 Right");
        }
    }
}

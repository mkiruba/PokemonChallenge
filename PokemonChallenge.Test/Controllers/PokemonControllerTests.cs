using AutoFixture;
using AutoFixture.AutoMoq;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using PokemonChallenge.Api.Controllers;
using PokemonChallenge.Application.Queries;
using PokemoneChallenge.Domain.Entities;
using PokemoneChallenge.Domain.Exceptions;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace PokemonChallenge.Test.Controllers
{
    public class PokemonControllerTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IMediator> _mockMediator;
        private readonly Mock<ILogger<PokemonController>> _mockLogger;

        public PokemonControllerTests()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoMoqCustomization());

            _mockMediator = new Mock<IMediator>();
            _mockLogger = new Mock<ILogger<PokemonController>>();
            _fixture.Inject<IMediator>(_mockMediator.Object);
            _fixture.Inject<ILogger<PokemonController>>(_mockLogger.Object);
        }

        [Fact]
        public async Task Get_Should_Return_Status200()
        {
            //Arrange
            var mockPokemon = _fixture.Create<Pokemon>();

            _mockMediator.Setup(x => x.Send(It.IsAny<GetPokemonByName>(), It.IsAny<CancellationToken>())).ReturnsAsync(mockPokemon);

            var pokemonController = _fixture.Build<PokemonController>()
                .OmitAutoProperties()
                .Create();

            //Act
            var result = await pokemonController.Get(It.IsAny<string>());

            //Assert
            Assert.Equal(200, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task Get_Should_Return_Data()
        {
            //Arrange
            var mockPokemon = _fixture.Create<Pokemon>();

            _mockMediator.Setup(x => x.Send(It.IsAny<GetPokemonByName>(), It.IsAny<CancellationToken>())).ReturnsAsync(mockPokemon);

            var pokemonController = _fixture.Build<PokemonController>()
                .OmitAutoProperties()
                .Create();

            //Act
            var result = await pokemonController.Get(It.IsAny<string>());

            //Assert
            Assert.Equal(mockPokemon, ((ObjectResult)result).Value);
        }

        [Fact]
        public async Task Get_Should_Return_Status404()
        {
            //Arrange
            _mockMediator.Setup(x => x.Send(It.IsAny<GetPokemonByName>(), It.IsAny<CancellationToken>())).ReturnsAsync((Pokemon)null);

            var pokemonController = _fixture.Build<PokemonController>()
                .OmitAutoProperties()
                .Create();

            //Act
            var result = await pokemonController.Get(It.IsAny<string>());

            //Assert
            Assert.Equal(404, ((NotFoundResult)result).StatusCode);
        }

        [Fact]
        public async Task Get_Should_Return_Status400()
        {
            //Arrange
            var mockPokemon = _fixture.Create<Pokemon>();

            _mockMediator.Setup(x => x.Send(It.IsAny<GetPokemonByName>(), It.IsAny<CancellationToken>())).ThrowsAsync(new PokemonBaseException("Bad Request"));

            var pokemonController = _fixture.Build<PokemonController>()
                .OmitAutoProperties()
                .Create();

            //Act
            var result = await pokemonController.Get(It.IsAny<string>());

            //Assert
            Assert.Equal(400, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task Get_Should_Return_Status500()
        {
            //Arrange
            var mockPokemon = _fixture.Create<Pokemon>();

            _mockMediator.Setup(x => x.Send(It.IsAny<GetPokemonByName>(), It.IsAny<CancellationToken>())).ThrowsAsync(new Exception("Internal Server Error"));

            var pokemonController = _fixture.Build<PokemonController>()
                .OmitAutoProperties()
                .Create();

            //Act
            var result = await pokemonController.Get(It.IsAny<string>());

            //Assert
            Assert.Equal(500, ((ObjectResult)result).StatusCode);
        }
    }
}

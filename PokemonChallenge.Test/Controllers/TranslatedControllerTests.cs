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
    public class TranslatedControllerTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IMediator> _mockMediator;
        private readonly Mock<ILogger<TranslatedController>> _mockLogger;

        public TranslatedControllerTests()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoMoqCustomization());

            _mockMediator = new Mock<IMediator>();
            _mockLogger = new Mock<ILogger<TranslatedController>>();
            _fixture.Inject<ILogger<TranslatedController>>(_mockLogger.Object);
        }

        [Fact]
        public async Task Get_Should_Return_Status200()
        {
            //Arrange
            var mockPokemon = _fixture.Create<Pokemon>();

            _mockMediator.Setup(x => x.Send(It.IsAny<GetPokemonByName>(), It.IsAny<CancellationToken>())).ReturnsAsync(mockPokemon);

            var translatedController = _fixture.Build<TranslatedController>()
                .OmitAutoProperties()
                .Create();

            //Act
            var result = await translatedController.Get(It.IsAny<string>());

            //Assert
            Assert.Equal(200, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task Get_Should_Return_Data()
        {
            //Arrange
            var mockPokemon = _fixture.Create<Pokemon>();

            _mockMediator.Setup(x => x.Send(It.IsAny<GetPokemonByName>(), It.IsAny<CancellationToken>())).ReturnsAsync(mockPokemon);

            var translatedController = _fixture.Build<TranslatedController>()
                .OmitAutoProperties()
                .Create();

            //Act
            var result = await translatedController.Get(It.IsAny<string>());

            //Assert
            Assert.Equal(mockPokemon, ((ObjectResult)result).Value);
        }

        [Fact]
        public async Task Get_Should_Return_Status404()
        {
            //Arrange
            _mockMediator.Setup(x => x.Send(It.IsAny<GetPokemonByName>(), It.IsAny<CancellationToken>())).ReturnsAsync((Pokemon)null);

            var translatedController = _fixture.Build<TranslatedController>()
                .OmitAutoProperties()
                .Create();

            //Act
            var result = await translatedController.Get(It.IsAny<string>());

            //Assert
            Assert.Equal(404, ((NotFoundResult)result).StatusCode);
        }

        [Fact]
        public async Task Get_Should_Return_Status400()
        {
            //Arrange
            var mockPokemon = _fixture.Create<Pokemon>();

            _mockMediator.Setup(x => x.Send(It.IsAny<GetPokemonByName>(), It.IsAny<CancellationToken>())).ThrowsAsync(new PokemonBaseException("Bad Request"));

            var translatedController = _fixture.Build<TranslatedController>()
                .OmitAutoProperties()
                .Create();

            //Act
            var result = await translatedController.Get(It.IsAny<string>());

            //Assert
            Assert.Equal(400, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task Get_Should_Return_Status500()
        {
            //Arrange
            var mockPokemon = _fixture.Create<Pokemon>();

            _mockMediator.Setup(x => x.Send(It.IsAny<GetPokemonByName>(), It.IsAny<CancellationToken>())).ThrowsAsync(new Exception("Internal Server Error"));

            var translatedController = _fixture.Build<TranslatedController>()
                .OmitAutoProperties()
                .Create();

            //Act
            var result = await translatedController.Get(It.IsAny<string>());

            //Assert
            Assert.Equal(500, ((ObjectResult)result).StatusCode);
        }
    }
}

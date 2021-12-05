using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using PokemonChallenge.Application.Queries;
using PokemonChallenge.Application.Services;
using PokemonChallenge.Infrastructure.Queries.Handlers;
using PokemoneChallenge.Domain.Entities;
using PokemoneChallenge.Domain.Exceptions;
using PokemoneChallenge.Domain.Factories;
using PokemoneChallenge.Domain.ValueObjects;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace PokemonChallenge.Test.Queries.Handler;

public class PokemonQueryHandlerTests
{
    private readonly IFixture _fixture;
    private readonly Mock<IPokemonService> _mockPokemonService;
    private readonly Mock<IPokemonFactory> _mockPokemonFactory;

    public PokemonQueryHandlerTests()
    {
        _fixture = new Fixture();
        _fixture.Customize(new AutoMoqCustomization());

        _mockPokemonService = new Mock<IPokemonService>();
        _mockPokemonFactory = new Mock<IPokemonFactory>();
        _fixture.Inject<IPokemonService>(_mockPokemonService.Object);
        _fixture.Inject<IPokemonFactory>(_mockPokemonFactory.Object);
    }

    [Fact]
    public async Task Handle_Should_ReturnData()
    {
        //Arrange
        var mockPokemonResponse = _fixture.Create<PokemonResponse>();
        var mockPokemon = _fixture.Create<Pokemon>();
        var mockGetPokemonByName = _fixture.Create<GetPokemonByName>();

        _mockPokemonService.Setup(x => x.GetPokemon(It.IsAny<string>())).ReturnsAsync(mockPokemonResponse);
        _mockPokemonFactory.Setup(x => x.CreatePokemon(mockPokemonResponse)).Returns(mockPokemon);

        var pokemonQueryHandler = _fixture.Create<PokemonQueryHandler>();

        //Act
        var result = await pokemonQueryHandler.Handle(mockGetPokemonByName, It.IsAny<CancellationToken>());

        //Assert
        Assert.Equal(result, mockPokemon);
    }

    [Fact]
    public async Task Handle_Should_FailToParse()
    {
        //Arrange
        var mockPokemonResponse = _fixture.Create<PokemonResponse>();
        var mockGetPokemonByName = _fixture.Create<GetPokemonByName>();

        _mockPokemonService.Setup(x => x.GetPokemon(It.IsAny<string>())).ReturnsAsync((PokemonResponse)null);

        var pokemonQueryHandler = _fixture.Create<PokemonQueryHandler>();

        //Act
        var exception = await Record.ExceptionAsync(
                () => pokemonQueryHandler.Handle(mockGetPokemonByName, It.IsAny<CancellationToken>()));

        //Assert
        Assert.IsType<PokemonParseException>(exception);
    }

}


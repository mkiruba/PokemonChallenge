using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using PokemonChallenge.Application.Queries;
using PokemonChallenge.Application.Services;
using PokemonChallenge.Infrastructure.Queries.Handlers;
using PokemoneChallenge.Domain.Entities;
using PokemoneChallenge.Domain.Factories;
using PokemoneChallenge.Domain.ValueObjects;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace PokemonChallenge.Test.Queries.Handler;

public class TranslatedQueryHandlerTests
{
    private readonly IFixture _fixture;
    private readonly Mock<IPokemonService> _mockPokemonService;
    private readonly Mock<IPokemonFactory> _mockPokemonFactory;
    private readonly Mock<IYodaService> _mockYodaService;
    private readonly Mock<IShakespeareService> _mockShakespeareService;

    public TranslatedQueryHandlerTests()
    {
        _fixture = new Fixture();
        _fixture.Customize(new AutoMoqCustomization());

        _mockYodaService = new Mock<IYodaService>();
        _mockShakespeareService = new Mock<IShakespeareService>();
        _mockPokemonService = new Mock<IPokemonService>();
        _mockPokemonFactory = new Mock<IPokemonFactory>();

        _fixture.Inject<IYodaService>(_mockYodaService.Object);
        _fixture.Inject<IShakespeareService>(_mockShakespeareService.Object);        
        _fixture.Inject<IPokemonService>(_mockPokemonService.Object);
        _fixture.Inject<IPokemonFactory>(_mockPokemonFactory.Object);
    }

    [Fact]
    public async Task Handle_Should_Return_YodaTranslation_When_Legendry_True()
    {
        //Arrange
        var mockPokemonResponse = _fixture.Create<PokemonResponse>();
        var mockTranslationResponse = _fixture.Create<TranslationResponse>();
        var mockPokemon = _fixture.Build<Pokemon>()
            .With(x => x.IsLegendary, true)
            .Create();
        var mockGetPokemonByName = _fixture.Create<GetPokemonByName>();

        _mockPokemonService.Setup(x => x.GetPokemon(It.IsAny<string>())).ReturnsAsync(mockPokemonResponse);
        _mockPokemonFactory.Setup(x => x.CreatePokemon(mockPokemonResponse)).Returns(mockPokemon);
        _mockYodaService.Setup(x => x.GetTranslation(It.IsAny<string>())).ReturnsAsync(mockTranslationResponse);

        var translatedQueryHandler = _fixture.Create<TranslatedQueryHandler>();

        //Act
        var result = await translatedQueryHandler.Handle(mockGetPokemonByName, It.IsAny<CancellationToken>());

        //Assert
        Assert.Equal(mockTranslationResponse.Contents.Translated, result.Description);
    }

    [Fact]
    public async Task Handle_Should_Return_ShakespeareTranslation_When_Legendry_False()
    {
        //Arrange
        var mockPokemonResponse = _fixture.Create<PokemonResponse>();
        var mockTranslationResponse = _fixture.Create<TranslationResponse>();
        var mockPokemon = _fixture.Build<Pokemon>()
            .With(x => x.IsLegendary, false)
            .Create();
        var mockGetPokemonByName = _fixture.Create<GetPokemonByName>();

        _mockPokemonService.Setup(x => x.GetPokemon(It.IsAny<string>())).ReturnsAsync(mockPokemonResponse);
        _mockPokemonFactory.Setup(x => x.CreatePokemon(mockPokemonResponse)).Returns(mockPokemon);
        _mockShakespeareService.Setup(x => x.GetTranslation(It.IsAny<string>())).ReturnsAsync(mockTranslationResponse);

        var translatedQueryHandler = _fixture.Create<TranslatedQueryHandler>();

        //Act
        var result = await translatedQueryHandler.Handle(mockGetPokemonByName, It.IsAny<CancellationToken>());

        //Assert
        Assert.Equal(mockTranslationResponse.Contents.Translated, result.Description);
    }
}


using MediatR;
using PokemonChallenge.Application.Queries;
using PokemonChallenge.Application.Services;
using PokemoneChallenge.Domain.Entities;
using PokemoneChallenge.Domain.Exceptions;
using PokemoneChallenge.Domain.Factories;

namespace PokemonChallenge.Infrastructure.Queries.Handlers;

public class TranslatedQueryHandler : IRequestHandler<GetTranslatedPokemonByName, Pokemon>
{
    private readonly IPokemonService _pokemonService;
    private readonly IPokemonFactory _pokemonFactory;
    private readonly IYodaService _yodaService;
    private readonly IShakespeareService _shakespeareService;

    public TranslatedQueryHandler(IYodaService yodaService,
        IShakespeareService shakespeareService, 
        IPokemonService pokemonService, 
        IPokemonFactory pokemonFactory)
    {
        _yodaService = yodaService;
        _shakespeareService = shakespeareService;
        _pokemonService = pokemonService;
        _pokemonFactory = pokemonFactory;
    }

    public async Task<Pokemon> Handle(GetTranslatedPokemonByName request, CancellationToken cancellationToken)
    {
        var pokemonResponse = await _pokemonService.GetPokemon(request.Name);
        if (pokemonResponse == null)
        {
            throw new PokemonParseException();
        }
        var pokemon = _pokemonFactory.CreatePokemon(pokemonResponse);
        if (pokemon.Habitat == "cave" || pokemon.IsLegendary)
        {
            var translatedResponse = await _yodaService.GetTranslation(pokemon.Description);
            pokemon.Description = translatedResponse.Contents.Translated;
        }
        else
        {
            var translatedResponse = await _shakespeareService.GetTranslation(pokemon.Description);
            pokemon.Description = translatedResponse.Contents.Translated;
        }
        return pokemon;
    }
}

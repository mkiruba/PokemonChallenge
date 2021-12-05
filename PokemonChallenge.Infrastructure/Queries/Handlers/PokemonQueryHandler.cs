using MediatR;
using PokemonChallenge.Application.Queries;
using PokemonChallenge.Application.Services;
using PokemoneChallenge.Domain.Entities;
using PokemoneChallenge.Domain.Exceptions;
using PokemoneChallenge.Domain.Factories;

namespace PokemonChallenge.Infrastructure.Queries.Handlers;

public class PokemonQueryHandler : IRequestHandler<GetPokemonByName, Pokemon>
{
    private readonly IPokemonService _pokemonService;
    private readonly IPokemonFactory _pokemonFactory;

    public PokemonQueryHandler(IPokemonService pokemonService, IPokemonFactory pokemonFactory)
    {
        _pokemonService = pokemonService;
        _pokemonFactory = pokemonFactory;
    }

    public async Task<Pokemon> Handle(GetPokemonByName request, CancellationToken cancellationToken)
    {
        var pokemonResponse = await _pokemonService.GetPokemon(request.Name);
        if (pokemonResponse == null)
        {
            throw new PokemonParseException();
        }
        return _pokemonFactory.CreatePokemon(pokemonResponse);
    }
}

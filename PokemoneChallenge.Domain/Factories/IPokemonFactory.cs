using PokemoneChallenge.Domain.Entities;
using PokemoneChallenge.Domain.ValueObjects;

namespace PokemoneChallenge.Domain.Factories;

public interface IPokemonFactory
{
    Pokemon CreatePokemon(PokemonResponse pokemonResponse);
}

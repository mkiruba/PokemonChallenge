using PokemoneChallenge.Domain.ValueObjects;

namespace PokemonChallenge.Application.Services
{
    public interface IPokemonService
    {
        Task<PokemonResponse> GetPokemon(string name);
    }
}

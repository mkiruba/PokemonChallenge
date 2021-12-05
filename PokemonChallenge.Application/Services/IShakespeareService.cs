using PokemoneChallenge.Domain.ValueObjects;

namespace PokemonChallenge.Application.Services
{
    public interface IShakespeareService
    {
        Task<TranslationResponse> GetTranslation(string message);
    }
}

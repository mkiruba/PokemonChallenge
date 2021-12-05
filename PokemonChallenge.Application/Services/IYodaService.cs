using PokemoneChallenge.Domain.ValueObjects;

namespace PokemonChallenge.Application.Services
{
    public interface IYodaService
    {
        Task<TranslationResponse> GetTranslation(string message);
    }
}

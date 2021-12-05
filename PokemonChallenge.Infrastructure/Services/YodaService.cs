using PokemonChallenge.Application.Services;
using PokemoneChallenge.Domain.Exceptions;
using PokemoneChallenge.Domain.ValueObjects;
using System.Net;
using System.Net.Http.Json;

namespace PokemonChallenge.Infrastructure.Services;

public class YodaService : IYodaService
{
    private readonly HttpClient _httpClient;

    public YodaService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<TranslationResponse> GetTranslation(string message)
    {
        HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync($"translate/yoda.json?text={message}");
        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            throw httpResponseMessage.StatusCode switch
            {
                HttpStatusCode.NotFound => new PokemonNotFoundException(message),
                _ => new PokemonFailedException(),
            };
        }
        var translationResponse = await httpResponseMessage.Content.ReadFromJsonAsync<TranslationResponse>();

        return translationResponse;
    }
}

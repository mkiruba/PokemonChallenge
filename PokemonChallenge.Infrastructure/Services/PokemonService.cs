using PokemonChallenge.Application.Services;
using PokemoneChallenge.Domain.Exceptions;
using PokemoneChallenge.Domain.ValueObjects;
using System.Net;
using System.Net.Http.Json;

namespace PokemonChallenge.Infrastructure.Services;

public class PokemonService : IPokemonService
{
    private readonly HttpClient _httpClient;

    public PokemonService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<PokemonResponse> GetPokemon(string name)
    {
        HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync($"pokemon-species/{name}");
        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            throw httpResponseMessage.StatusCode switch
            {
                HttpStatusCode.NotFound => new PokemonNotFoundException(name),
                _ => new PokemonFailedException(),
            };
        }
        var pokemonResponse = await httpResponseMessage.Content.ReadFromJsonAsync<PokemonResponse>();

        return pokemonResponse;
    }
}

using PokemoneChallenge.Domain.Entities;
using PokemoneChallenge.Domain.ValueObjects;

namespace PokemoneChallenge.Domain.Factories;

public class PokemonFactory : IPokemonFactory
{
    public Pokemon CreatePokemon(PokemonResponse pokemonResponse)
    {
        var flavorText = string.Empty;
        var flavorEntries = pokemonResponse.FlavorTextEntries.FirstOrDefault(x => x.Language.Name == "en");        
        if (flavorEntries != null)
        {
            flavorText = flavorEntries.FlavorText.Replace("\n", " ")
           .Replace("\f", " ")
           .Replace("\r\n", " ");
        }
        
        return new Pokemon
        {
            Name = pokemonResponse.Name,
            Description = flavorText,
            Habitat = pokemonResponse.Habitat.Name,
            IsLegendary = pokemonResponse.IsLegendary
        };
    }
}

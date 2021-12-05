using System.Text.Json.Serialization;

namespace PokemoneChallenge.Domain.ValueObjects;

public partial class PokemonResponse
{
    [JsonPropertyName("flavor_text_entries")]
    public FlavorTextEntry[] FlavorTextEntries { get; set; }

    [JsonPropertyName("habitat")]
    public Color Habitat { get; set; }

    [JsonPropertyName("is_legendary")]
    public bool IsLegendary { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }
}

public partial class Color
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("url")]
    public Uri Url { get; set; }
}

public partial class FlavorTextEntry
{
    [JsonPropertyName("flavor_text")]
    public string FlavorText { get; set; }

    [JsonPropertyName("language")]
    public Color Language { get; set; }

    [JsonPropertyName("version")]
    public Color Version { get; set; }
}
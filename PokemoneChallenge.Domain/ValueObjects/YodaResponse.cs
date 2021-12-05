using System.Text.Json.Serialization;

namespace PokemoneChallenge.Domain.ValueObjects
{
    public partial class TranslationResponse
    {
        [JsonPropertyName("success")]
        public Success Success { get; set; }

        [JsonPropertyName("contents")]
        public Contents Contents { get; set; }
    }

    public partial class Contents
    {
        [JsonPropertyName("translated")]
        public string Translated { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("translation")]
        public string Translation { get; set; }
    }

    public partial class Success
    {
        [JsonPropertyName("total")]
        public long Total { get; set; }
    }
}
using System.Text.Json.Serialization;

namespace PokeFun.PokeApi.Model
{
    public class FlavorText
    {
        [JsonPropertyName("flavor_text")]
        public string Text { get; set; }

        [JsonPropertyName("language")]
        public NamedAPIResource Language { get; set; }

        [JsonPropertyName("version")]
        public NamedAPIResource Version { get; set; }
    }
}

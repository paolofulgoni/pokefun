using System.Text.Json.Serialization;

namespace PokeFun.PokeApi.Model
{
    public class Pokemon
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("species")]
        public NamedAPIResource Species { get; set; }
    }
}

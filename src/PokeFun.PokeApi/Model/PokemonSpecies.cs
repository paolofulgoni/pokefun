using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PokeFun.PokeApi.Model
{
    public class PokemonSpecies
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("flavor_text_entries")]
        public IEnumerable<FlavorText> FlavorTextEntries { get; set; }

        [JsonPropertyName("habitat")]
        public NamedAPIResource Habitat { get; set; }

        [JsonPropertyName("is_legendary")]
        public bool IsLegendary { get; set; }
    }
}

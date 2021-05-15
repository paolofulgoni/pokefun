using System.Text.Json.Serialization;

namespace PokeFun.PokeApi.Model
{
    public class NamedAPIResource
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PokeFun.PokeApi.Model
{
    public record FlavorText
    {
        [JsonPropertyName("flavor_text")]
        public string Text { get; init; }

        [JsonPropertyName("language")]
        public NamedAPIResource Language { get; init; }

        [JsonPropertyName("version")]
        public NamedAPIResource Version { get; init; }
    }
}

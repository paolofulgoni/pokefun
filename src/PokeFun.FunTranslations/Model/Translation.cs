using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PokeFun.FunTranslations.Model
{
    public record Translation
    {
        [JsonPropertyName("success")]
        public Success Success { get; init; }

        [JsonPropertyName("contents")]
        public Contents Contents { get; init; }
    }
}

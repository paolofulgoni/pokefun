using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PokeFun.PokeApi.Model
{
    public class NamedAPIResource
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
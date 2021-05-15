using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PokeFun.Model
{
    /// <summary>
    /// Pokemon information
    /// </summary>
    public record Pokemon
    {
        /// <summary>
        /// Name of the Pokemon
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; init; }

        /// <summary>
        /// Description of the Pokemon.
        /// </summary>
        /// <remarks>
        /// It may be in plain English or translated depending on the request
        /// </remarks>
        [JsonPropertyName("description")]
        public string Description { get; init; }

        /// <summary>
        /// The habitat this Pokemon can be encountered in
        /// </summary>
        [JsonPropertyName("habitat")]
        public string Habitat { get; init; }

        /// <summary>
        /// Whether or not this is a legendary Pokemon
        /// </summary>
        [JsonPropertyName("isLegendary")]
        public bool IsLegendary { get; init; }
    }
}

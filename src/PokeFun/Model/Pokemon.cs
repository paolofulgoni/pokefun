using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PokeFun.Model
{
    /// <summary>
    /// Pokemon information
    /// </summary>
    public class Pokemon
    {
        /// <summary>
        /// Name of the Pokemon
        /// </summary>
        [Required]
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Description of the Pokemon.
        /// </summary>
        /// <remarks>
        /// It may be in plain English or translated depending on the request
        /// </remarks>
        [Required]
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// The habitat this Pokemon can be encountered in
        /// </summary>
        [JsonPropertyName("habitat")]
        public string Habitat { get; set; }

        /// <summary>
        /// Whether or not this is a legendary Pokemon
        /// </summary>
        [JsonPropertyName("isLegendary")]
        public bool IsLegendary { get; set; }
    }
}

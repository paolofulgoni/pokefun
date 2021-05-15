using System.Text.Json.Serialization;

namespace PokeFun.FunTranslations.Model
{
    public class Translation
    {
        [JsonPropertyName("success")]
        public Success Success { get; set; }

        [JsonPropertyName("contents")]
        public Contents Contents { get; set; }
    }
}

using System.Text.Json.Serialization;

namespace PokeFun.FunTranslations.Model
{
    public class Contents
    {
        [JsonPropertyName("translated")]
        public string Translated { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("translation")]
        public string Translation { get; set; }
    }
}

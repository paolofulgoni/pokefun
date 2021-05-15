using System.Text.Json.Serialization;

namespace PokeFun.FunTranslations.Model
{
    public class Success
    {
        [JsonPropertyName("total")]
        public int Total { get; set; }
    }
}

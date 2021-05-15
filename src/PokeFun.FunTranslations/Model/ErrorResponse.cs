using System.Text.Json.Serialization;

namespace PokeFun.FunTranslations.Model
{
    public class ErrorResponse
    {
        [JsonPropertyName("error")]
        public Error Error { get; set; }
    }
}

using System.Text.Json.Serialization;

namespace Pokedex.API.Responses
{
    public class FunTranslationsResponse
    {
        [JsonPropertyName("contents")]
        public FunTranslationsContents Contents { get; set; }
    }
}

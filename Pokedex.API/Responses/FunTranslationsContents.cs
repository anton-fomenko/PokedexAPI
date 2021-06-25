using System.Text.Json.Serialization;

namespace Pokedex.API.Responses
{
    public class FunTranslationsContents
    {
        [JsonPropertyName("translated")]
        public string Translated { get; set; }
    }
}

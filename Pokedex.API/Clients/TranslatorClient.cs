using Microsoft.Extensions.Configuration;
using Pokedex.API.Responses;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Pokedex.API.Clients
{
    public class TranslatorClient : ITranslatorClient
    {
        private const string TranslatorUrlConfigKey = "TranslatorUrl";

        private readonly HttpClient _client;
        private readonly string _translatorUrl;

        public TranslatorClient(
            HttpClient httpClient, 
            IConfiguration config)
        {
            _client = httpClient;
            _translatorUrl = config.GetValue<string>(TranslatorUrlConfigKey);
        }

        public async Task<string> TranslateAsync(string text, string lang)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters["text"] = text;

            FormUrlEncodedContent encodedContent = new FormUrlEncodedContent(parameters);

            HttpResponseMessage httpResponse = await _client.PostAsync(_translatorUrl + $"{lang}.json", encodedContent);

            FunTranslationsResponse funTranslationsResponse = await httpResponse.Content.ReadFromJsonAsync<FunTranslationsResponse>();

            return funTranslationsResponse.Contents.Translated;
        }
    }
}

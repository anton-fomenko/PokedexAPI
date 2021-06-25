using Microsoft.Extensions.Logging;
using PokeApiNet;
using Pokedex.API.Clients;
using Pokedex.API.Constants;
using Pokedex.API.Helpers;
using Pokedex.API.Resources;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Pokedex.API.Services
{
    public class PokemonService : IPokemonService
    {
        private readonly IPokeClient _pokeClient;
        private readonly ITranslatorClient _translatorClient;
        private readonly ITextHelper _textHelper;
        private readonly ILogger<PokemonService> _logger;

        public PokemonService(
            IPokeClient pokeClient,
            ITranslatorClient translatorClient,
            ITextHelper textHelper,
            ILogger<PokemonService> logger)
        {
            _pokeClient = pokeClient;
            _translatorClient = translatorClient;
            _textHelper = textHelper;
            _logger = logger;
        }

        public async Task<PokemonResource> Get(string name)
        {
            Pokemon pokemon = null;

            try
            {
                pokemon = await _pokeClient.GetResourceAsync<Pokemon>(name);
            }
            catch (HttpRequestException)
            {
                return null;
            }

            PokemonSpecies species = await _pokeClient.GetResourceAsync<PokemonSpecies>(pokemon.Species.Name);
            string description = species.FlavorTextEntries.FirstOrDefault(text => text.Language.Name == Languages.English)?.FlavorText;
            description = _textHelper.Fix(description);

            PokemonResource res = new PokemonResource
            {
                Name = pokemon.Name,
                Description = description,
                Habitat = species.Habitat.Name,
                IsLegendary = species.IsLegendary
            };

            return res;
        }

        public async Task<PokemonResource> GetTranslatedAsync(string name)
        {
            Pokemon pokemon = null;

            try
            {
                pokemon = await _pokeClient.GetResourceAsync<Pokemon>(name);
            }
            catch (HttpRequestException)
            {
                return null;
            }
            PokemonSpecies species = await _pokeClient.GetResourceAsync<PokemonSpecies>(pokemon.Species.Name);
            string description = species.FlavorTextEntries.FirstOrDefault(text => text.Language.Name == Languages.English)?.FlavorText;
            description = _textHelper.Fix(description);

            description = await TranslateDescriptionAsync(
                name, 
                species.Habitat.Name, 
                species.IsLegendary,
                description);

            PokemonResource res = new PokemonResource
            {
                Name = pokemon.Name,
                Description = description,
                Habitat = species.Habitat.Name,
                IsLegendary = species.IsLegendary
            };

            return res;
        }

        private async Task<string> TranslateDescriptionAsync(
            string pokemon,
            string habitatName,
            bool isLegendary,
            string description)
        {
            try
            {
                string lang = Languages.Fun.Shakespeare;

                if (habitatName == Habitats.Cave || isLegendary)
                {
                    lang = Languages.Fun.Yoda;
                }

                description = await _translatorClient.TranslateAsync(description, lang);

                return description;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Translation failed for {pokemon}");
            }

            return description;
        }
    }
}

using PokeApiNet;
using Pokedex.API.Clients;
using Pokedex.API.Constants;
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

            PokemonResource res = new PokemonResource
            {
                Name = pokemon.Name,
                Description = description,
                Habitat = species.Habitat.Name,
                IsLegendary = species.IsLegendary
            };

            return res;
        }

        public Task<PokemonResource> GetTranslatedAsync(string name)
        {
            throw new NotImplementedException();
        }
    }
}

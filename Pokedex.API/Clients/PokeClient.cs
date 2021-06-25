using PokeApiNet;
using System.Threading.Tasks;

namespace Pokedex.API.Clients
{
    public class PokeClient : IPokeClient
    {
        private readonly PokeApiClient _pokeClient;

        public PokeClient(PokeApiClient pokeClient)
        {
            _pokeClient = pokeClient;
        }

        public async Task<T> GetResourceAsync<T>(string name) where T : NamedApiResource
        {
            return await _pokeClient.GetResourceAsync<T>(name);
        }
    }
}

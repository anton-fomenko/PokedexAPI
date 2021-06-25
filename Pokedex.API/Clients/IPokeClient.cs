using PokeApiNet;
using System.Threading.Tasks;

namespace Pokedex.API.Clients
{
    public interface IPokeClient
    {
        Task<T> GetResourceAsync<T>(string name) where T : NamedApiResource;
    }
}

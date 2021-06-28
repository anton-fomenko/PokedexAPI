using Pokedex.API.Resources;
using System.Threading.Tasks;

namespace Pokedex.API.Services
{
    public interface IPokemonService
    {
        Task<PokemonResource> GetAsync(string name);

        Task<PokemonResource> GetTranslatedAsync(string name);
    }
}

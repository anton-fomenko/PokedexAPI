using Pokedex.API.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pokedex.API.Services
{
    public class PokemonService : IPokemonService
    {
        public Task<PokemonResource> Get(string name)
        {
            throw new NotImplementedException();
        }

        public Task<PokemonResource> GetTranslatedAsync(string name)
        {
            throw new NotImplementedException();
        }
    }
}

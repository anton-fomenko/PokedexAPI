using Microsoft.AspNetCore.Mvc;
using Pokedex.API.Resources;
using Pokedex.API.Services;
using System.Net;
using System.Threading.Tasks;

namespace Pokedex.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ResponseCache(Duration = 60)]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonService _pokemonService;

        public PokemonController(IPokemonService pokemonService)
        {
            _pokemonService = pokemonService;
        }

        [HttpGet]
        [Route("{name}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(PokemonResource), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<PokemonResource>> Get(string name)
        {
            PokemonResource res = await _pokemonService.Get(name);

            if (res != null)
                return res;

            return NotFound();
        }

        [HttpGet]
        [Route("translated/{name}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(PokemonResource), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<PokemonResource>> GetTranslated(string name)
        {
            PokemonResource res = await _pokemonService.GetTranslatedAsync(name);

            if (res != null)
                return res;

            return NotFound();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using PokeApiNet.Services;
using PokeApiNet.Models;

namespace PokeApiNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly PokeApiClient _pokeApiClient;

        public PokemonController(PokeApiClient pokeApiClient)
        {
            _pokeApiClient = pokeApiClient;
        }

        [HttpGet("GetTop10RandomPokemons")]
        public async Task<ActionResult<List<PokemonInfo>>> GetTop10RandomPokemons()
        {
            var result = await _pokeApiClient.GetTop10RandomPokemons();
            if (result is not null)
            {
                return Ok(result);
            }
            return BadRequest("Nenhum registro encontrado");
        }

        [HttpGet("GetPokemonByName")]
        public async Task<ActionResult<PokemonInfo>> GetPokemonByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("Nome inválido");
            }

            var result = await _pokeApiClient.GetPokemonByName(name);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound("Pokémon não encontrado");
        }

    }
}

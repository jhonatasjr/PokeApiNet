using Microsoft.AspNetCore.Mvc;
using PokeApiNet.Data;
using PokeApiNet.Models;
using PokeApiNet.Services;
using System.Collections.Concurrent;

namespace PokeApiNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MestrePokemonController : ControllerBase
    {
        private readonly PokemonRepository _repository;
        private readonly PokeApiClient _pokeApiClient;

        public MestrePokemonController(PokemonRepository repository, PokeApiClient pokeApiClient)
        {
            _repository = repository;
            _pokeApiClient = pokeApiClient;
        }

        [HttpPost("CadastrarMestrePokemon")]
        public ActionResult<MestrePokemon> CadastrarMestrePokemon(MestrePokemon mestrePokemon)
        {
            _repository.AddMestrePokemon(mestrePokemon);
            return Ok(mestrePokemon);
        }

        [HttpPost("CapturarPokemon")]
        public async Task<ActionResult> CapturarPokemon(PokemonCapturado pokemonCapturado)
        {
            // Valida se o mestre pokemon existe
            var mestrePokemon = _repository.GetMestrePokemonByCod(pokemonCapturado.CodMestrePokemon);
            if (mestrePokemon == null)
            {
                return BadRequest("Mestre Pokémon não encontrado.");
            }

            // Trasnforma em minúsculo
            pokemonCapturado.NomePokemonASerCapturado = pokemonCapturado.NomePokemonASerCapturado.ToLower();

            // Verificar se o Pokémon já foi capturado
            if (_repository.PokemonCapturadoExists(pokemonCapturado.CodMestrePokemon, pokemonCapturado.NomePokemonASerCapturado))
            {
                return BadRequest("Pokémon já foi capturado antes.");
            }

            // Validar se o Pokémon a ser capturado existe
            var pokemonASerCapturado = await _pokeApiClient.GetPokemonByName(pokemonCapturado.NomePokemonASerCapturado);
            if (pokemonASerCapturado == null)
            {
                return BadRequest("Pokémon a ser capturado não encontrado.");
            }

            _repository.AddPokemonCapturado(pokemonCapturado);

            var response = new
            {
                PokemonCapturado = pokemonCapturado,
                MestrePokemon = mestrePokemon
            };

            return Ok(response);
        }


        [HttpGet("PokemonsCapturados")]
        public ActionResult<IEnumerable<object>> ListarPokemonsCapturados()
        {
            var pokemonsCapturados = _repository.GetAllPokemonsCapturados();
            if (pokemonsCapturados.Count() == 0)
            {
                return Ok("Nenhum Pokémon foi capturado.");
            }

            var listaComDetalhes = new List<object>();

            foreach (var pokemonCapturado in pokemonsCapturados)
            {
                var mestrePokemon = _repository.GetMestrePokemonByCod(pokemonCapturado.CodMestrePokemon);

                if (mestrePokemon != null)
                {
                    var detalhes = new
                    {
                        PokemonCapturado = pokemonCapturado,
                        MestrePokemon = mestrePokemon
                    };
                    listaComDetalhes.Add(detalhes);
                }
            };

            return Ok(listaComDetalhes);
        }

    }
}

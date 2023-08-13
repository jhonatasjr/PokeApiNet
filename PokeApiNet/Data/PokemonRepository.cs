using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PokeApiNet.Models;

namespace PokeApiNet.Data
{
    public class PokemonRepository
    {
        private readonly PokeApiContext _context;

        public PokemonRepository(PokeApiContext context)
        {
            _context = context;
        }

        // Métodos para operações relacionadas a MestrePokemons

        public MestrePokemon GetMestrePokemonByCod(int cod)
        {
            return _context.MestrePokemons.FirstOrDefault(mp => mp.CodMestrePokemon == cod);
        }

        public bool PokemonCapturadoExists(int CodMestrePokemon, string nmPokemon)
        {
            return _context.PokemonsCapturados.Any(pc => pc.CodMestrePokemon == CodMestrePokemon && pc.NomePokemonASerCapturado == nmPokemon);
        }

        public void AddMestrePokemon(MestrePokemon mestrePokemon)
        {
            _context.MestrePokemons.Add(mestrePokemon);
            _context.SaveChanges();
        }

        public IEnumerable<PokemonCapturado> GetAllPokemonsCapturados()
        {
            return _context.PokemonsCapturados.ToList();
        }

        public void AddPokemonCapturado(PokemonCapturado pokemonCapturado)
        {
            _context.PokemonsCapturados.Add(pokemonCapturado);
            _context.SaveChanges();
        }

    }
}

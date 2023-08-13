using Microsoft.EntityFrameworkCore;
using PokeApiNet.Models;

namespace PokeApiNet.Data
{
    public class PokeApiContext : DbContext
    {
        public DbSet<MestrePokemon> MestrePokemons { get; set; }
        public DbSet<PokemonCapturado> PokemonsCapturados { get; set; }

        public PokeApiContext(DbContextOptions<PokeApiContext> options) : base(options)
        {
        }
    }
}

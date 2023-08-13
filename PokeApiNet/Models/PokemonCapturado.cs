using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PokeApiNet.Models
{
    public class PokemonCapturado
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }

        // Relacionamento com o mestre Pokémon que capturou
        public int CodMestrePokemon { get; set; }

        public string NomePokemonASerCapturado { get; set; }
    }
}

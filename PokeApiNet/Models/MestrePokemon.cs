using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PokeApiNet.Models
{
    public class MestrePokemon
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }
        public int CodMestrePokemon { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }
        public string CPF { get; set; }
    }
}
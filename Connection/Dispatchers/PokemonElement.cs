using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace Connection.Dispatchers
{
    public class PokemonElement
    {
        public int Id { get; set; }

        [ForeignKey("PokemonPokemon")]
        public int PokemonId { get; set; }
        [JsonProperty("pokemon")]
        public PokemonPokemon Pokemon { get; set; }

        [ForeignKey("Types")]
        public int TypesId { get; set; }
    }
}
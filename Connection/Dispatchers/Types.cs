using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Connection.Dispatchers
{
    public class Types
    {
        public int Id { get; set; }

        [JsonProperty("name")]
        public string name { get; set; }

        [ForeignKey("PokemonElement")]
        public int PokemonId { get; set; }
        [JsonProperty("pokemon")]
        public List<PokemonElement> Pokemon { get; set; }
    }
}

using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace Connection.Dispatchers
{
    public class StatElement
    {
        public int Id { get; set; }

        [JsonProperty("base_stat")]
        public int Base_Stat { get; set; }

        [ForeignKey("Pokemon")]
        public int PokemonId { get; set; }
        public Pokemon Pokemon { get; set; }

        [ForeignKey("StatStat")]
        public int StatId { get; set; }
        [JsonProperty("stat")]
        public StatStat Stat { get; set; }
    }
}
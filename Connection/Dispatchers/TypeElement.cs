using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace Connection.Dispatchers
{
    public class TypeElement
    {
        public int Id { get; set; }

        [ForeignKey("Pokemon")]
        public int PokemonId { get; set; }
        public Pokemon Pokemon { get; set; }

        [ForeignKey("TypeClass")]
        public int TypeId { get; set; }
        [JsonProperty("type")]
        public TypeClass Type { get; set; }
    }
}
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace Connection.Dispatchers
{
    public class AbilityElement
    {
        public int Id { get; set; }

        [ForeignKey("Pokemon")]
        public int PokemonId { get; set; }
        public Pokemon Pokemon { get; set; }

        [ForeignKey("TypeClass")]
        public int AbilityId { get; set; }
        [JsonProperty("ability")]
        public TypeClass Ability { get; set; }
    }
}
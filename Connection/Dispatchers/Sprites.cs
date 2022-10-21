using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace Connection.Dispatchers
{
    public class Sprites
    {
        public int Id { get; set; }

        [JsonProperty("back_default")]
        public string Back_Default { get; set; }

        [JsonProperty("back_female")]
        public string Back_Female { get; set; }

        [JsonProperty("back_shiny")]
        public string Back_Shiny { get; set; }

        [JsonProperty("back_shiny_female")]
        public string Back_Shiny_Female { get; set; }

        [JsonProperty("front_default")]
        public string Front_Default { get; set; }

        [JsonProperty("front_female")]
        public string Front_Female { get; set; }

        [JsonProperty("front_shiny")]
        public string Front_Shiny { get; set; }

        [JsonProperty("front_shiny_female")]
        public string Front_Shiny_Female { get; set; }

        [ForeignKey("Other")]
        public int OtherId { get; set; }
        [JsonProperty("other")]
        public Other Other { get; set; }
    }
}
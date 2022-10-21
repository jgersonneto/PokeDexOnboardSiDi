using Newtonsoft.Json;

namespace Connection.Dispatchers
{
    public class Home
    {
        public int Id { get; set; }

        [JsonProperty("front_default")]
        public string Front_Default { get; set; }

        [JsonProperty("front_female")]
        public string Front_Female { get; set; }

        [JsonProperty("front_shiny")]
        public string Front_Shiny { get; set; }

        [JsonProperty("front_shiny_female")]
        public string Front_Shiny_Female { get; set; }
    }
}
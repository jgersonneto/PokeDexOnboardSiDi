using Newtonsoft.Json;

namespace Connection.Dispatchers
{
    public class DreamWorld
    {
        public int Id { get; set; }

        [JsonProperty("front_default")]
        public string Front_Default { get; set; }

        [JsonProperty("front_female")]
        public string Front_Female { get; set; }
    }
}
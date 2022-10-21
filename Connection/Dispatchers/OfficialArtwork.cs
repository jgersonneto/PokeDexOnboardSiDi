using Newtonsoft.Json;

namespace Connection.Dispatchers
{
    public class OfficialArtwork
    {
        public int Id { get; set; }

        [JsonProperty("front_default")]
        public string Front_Default { get; set; }
    }
}
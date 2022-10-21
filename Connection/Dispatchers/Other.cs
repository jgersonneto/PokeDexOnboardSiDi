using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace Connection.Dispatchers
{
    public class Other
    {
        public int Id { get; set; }

        [JsonProperty("dream_world")]
        public DreamWorld Dream_World { get; set; }

        [ForeignKey("Home")]
        public int HomeId { get; set; }
        [JsonProperty("home")]
        public Home Home { get; set; }

        [JsonProperty("official-artwork")]
        public OfficialArtwork OfficialArtwork { get; set; }
    }
}
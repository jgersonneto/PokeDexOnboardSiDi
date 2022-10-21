using Newtonsoft.Json;

namespace Connection.Dispatchers
{
    public class StatStat
    {
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
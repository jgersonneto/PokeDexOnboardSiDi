using Newtonsoft.Json;

namespace Connection.Dispatchers
{
    public class TypeClass
    {
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        public string IconName { get; set; }
    }
}
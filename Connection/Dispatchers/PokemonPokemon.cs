using Newtonsoft.Json;

namespace Connection.Dispatchers
{
    public class PokemonPokemon
    {
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
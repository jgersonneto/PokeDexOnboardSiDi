using Connection.Dispatchers;
using System.Collections.Generic;

namespace PokedexUwp.Interface{
    public interface IDBConnection
    {
        void AddPokemonToDB(Pokemon pokemon);
        List<Pokemon> GetPokemons(string pokemonAttribute);
        IDBConnection CreateConnection();
    }
}

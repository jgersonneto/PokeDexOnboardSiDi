using Connection.Dispatchers;
using System.Collections.Generic;

namespace PokedexUwp.Interface
{
    public interface ISearchPokemon
    {
        List<Pokemon> SearchAndGetPokemon(string pokemonAttribute);
    }
}

using Connection.Dispatchers;
using System.Collections.Generic;

namespace Connection.Interface
{
    public interface ISearchPokemon
    {
        List<Pokemon> SearchAndGetPokemon(string pokemonAttribute);
    }
}

using Connection.Api;
using Connection.Dispatchers;
using Connection.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Connection.Factory.Api
{
    public class SearchPokemonByIdFromApi : ISearchPokemon
    {
        public List<Pokemon> SearchAndGetPokemon(string pokemonAttribute)
        {
            int id = short.Parse(pokemonAttribute);
            List<Pokemon> pokemons = new List<Pokemon>();
            pokemons.Add(Task.Run(async () => await ApiService.ApiPokeById(id)).Result);
            return pokemons;
        }
    }
}

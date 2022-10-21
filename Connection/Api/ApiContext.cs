using Connection.Dispatchers;
using Connection.Factory.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connection.Api
{
    public class ApiContext
    {
        #region Private Variables

        private Dictionary<int, string> types = new Dictionary<int, string>();
        
        #endregion

        public ApiContext()
        {
            types.Add(0, "normal");
            types.Add(1, "fighting");
            types.Add(2, "flying");
            types.Add(3, "poison");
            types.Add(4, "ground");
            types.Add(5, "rock");
            types.Add(6, "bug");
            types.Add(7, "ghost");
            types.Add(8, "steel");
            types.Add(9, "fire");
            types.Add(10, "water");
            types.Add(11, "grass");
            types.Add(12, "electric");
            types.Add(13, "psychic");
            types.Add(14, "ice");
            types.Add(15, "dragon");
            types.Add(16, "dark");
            types.Add(17, "fairy");
            types.Add(18, "unknown");
            types.Add(19, "shadow");
        }

        #region Public Method

        public List<Pokemon> GetPokemons(string pokemonAttribute)
        {
            if (string.IsNullOrEmpty(pokemonAttribute))
                return null;
            if (types.ContainsValue(pokemonAttribute))
            {
                List<Pokemon> pokemons = new List<Pokemon>();
                foreach (var pk in new SearchPokemonByTypeFromApi().SearchAndGetPokemon(pokemonAttribute))
                {
                    if (pk?.Id < 251)
                        pokemons.Add(new SearchPokemonByNameFromApi().SearchAndGetPokemon(pk.Name)[0]);
                }
                return pokemons;
            }
            if (pokemonAttribute.All(char.IsDigit))
                return new SearchPokemonByIdFromApi().SearchAndGetPokemon(pokemonAttribute);
            return new SearchPokemonByNameFromApi().SearchAndGetPokemon(pokemonAttribute);
        }
        
        #endregion
    }
}

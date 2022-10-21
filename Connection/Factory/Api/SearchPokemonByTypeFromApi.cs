using Connection.Api;
using Connection.DataBase;
using Connection.Dispatchers;
using Connection.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Connection.Factory.Api
{
    public class SearchPokemonByTypeFromApi : ISearchPokemon
    {
        #region Public Method

        public List<Pokemon> SearchAndGetPokemon(string pokemonAttribute)
        {
            List<Pokemon> pokemons = new List<Pokemon>();
            Types typePoke = Task.Run(async () => await ApiService.ApiPokeByTypes(pokemonAttribute)).Result;
            AddTypeToDB(typePoke);
            foreach (var type in typePoke.Pokemon)
            {
                Pokemon pokemon = new Pokemon()
                {
                    Name = type.Pokemon.Name
                };
                pokemons.Add(pokemon);
            }
            return pokemons;
        }

        #endregion

        #region Private Method

        private async Task AddTypeToDB(Types typePoke)
        {
            using (var db = new ClientDataBase())
            {
                db.Types.Add(typePoke);

                await db.SaveChangesAsync();
            }
        }
        
        #endregion
    }
}

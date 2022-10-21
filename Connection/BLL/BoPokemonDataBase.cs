using Connection.Api;
using Connection.DataBase;
using Connection.Dispatchers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Connection.BLL
{
    public class BoPokemonDataBase
    {

        #region Private Variables

        private DataBaseContext _dbContext;
        private ApiContext _apiContext;
        
        #endregion

        public BoPokemonDataBase(string dbPath)
        {
            _dbContext = new DataBaseContext(dbPath);
            _apiContext = new ApiContext();
        }

        #region Public Methods

        public async Task CreateDataBase()
        {
            await _dbContext.CreateDataBase();

            if (!_dbContext.HasPokemons())
            {
                foreach (Pokemon pkm in GetInitialList())
                {
                    await AddPokemon(pkm);
                }
            }
        }

        public async Task AddPokemon(Pokemon pokemon)
        {
            await _dbContext.AddPokemonToDB(pokemon);
        }

        public List<Pokemon> GetAllPokemons()
        {
            return _dbContext.GetAllPokemons();
        }

        public async Task<List<Pokemon>> GetPokemons(string pokemonAttribute)
        {
            return await _dbContext.GetPokemons(pokemonAttribute);
        }

        public int GetMaxPokemonIndex()
        {
            List<Pokemon> pokemonList = _dbContext.GetAllPokemons();
            return pokemonList.Count == 0 ? 0 : pokemonList[pokemonList.Count - 1].Id;
        }

        public bool HasPokemonById(string id)
        {
            return _dbContext.HasPokemonById(id);
        }

        public async Task SearchPokemonsInApi(string pokemonAttribute)
        {
            List<Pokemon> pokemonsAPI;
            await Task.Run(() =>
            {
                if (!HasPokemonById(pokemonAttribute) || !_dbContext.HasPokemonByName(pokemonAttribute) || !_dbContext.HasPokemonByType(pokemonAttribute))
                {
                    pokemonsAPI = _apiContext.GetPokemons(pokemonAttribute);
                    pokemonsAPI?.ForEach(p => 
                    {
                        if (p?.Id <= 250)
                            AddPokemon(p);
                    });
                }
            });
        }

        #endregion

        #region Private Methods

        private List<Pokemon> GetInitialList()
        {
            List<Pokemon> initialList = new List<Pokemon>();
            List<int> PokemonIdsList = new List<int>() { 25, 1, 4, 7 };

            foreach(var id in PokemonIdsList)
            {
                initialList.Add(Task.Run(async () => await ApiService.ApiPokeById(id)).Result);
            }
            return initialList;
        }

        #endregion
    }
}

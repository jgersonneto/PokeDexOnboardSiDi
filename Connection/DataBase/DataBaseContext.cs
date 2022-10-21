using Connection.Commons;
using Connection.Dispatchers;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using Connection.Factory.DB;

namespace Connection.DataBase
{
    internal class DataBaseContext
    {
        #region Private Variables

        private string _dataBasePath { get; set; }
        private SearchPokemonByIdFromDB _searchPokemonByIdFromDB { get; set; }
        private SearchPokemonByNameFromDB _searchPokemonByNameFromDB { get; set; }
        private SearchPokemonByTypeFromDB _searchPokemonByTypeFromDB { get; set; }
        private Dictionary<int, string> types = new Dictionary<int, string>();
        
        #endregion

        public DataBaseContext(string path)
        {
            _dataBasePath = path;
            GlobalParameters.DataBasePath = _dataBasePath;
            _searchPokemonByIdFromDB = new SearchPokemonByIdFromDB();
            _searchPokemonByNameFromDB = new SearchPokemonByNameFromDB();
            _searchPokemonByTypeFromDB = new SearchPokemonByTypeFromDB();

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

        public async Task CreateDataBase()
        {
            using (var db = new ClientDataBase())
            {
                await db.Database.EnsureCreatedAsync();
            }
        }

        public async Task AddPokemonToDB(Pokemon pokemon)
        {
            foreach (var type in pokemon.Types)
            {
                type.PokemonId = pokemon.Id;
                type.Type.IconName = GetIconImageFromType(type.Type.Name);
            }
            using (var db = new ClientDataBase())
            {
                db.Pokemons.Add(pokemon);
                await db.SaveChangesAsync();
            }
        }

        public List<Pokemon> GetAllPokemons()
        {
            return _searchPokemonByIdFromDB.SearchAndGetPokemon("");
        }

        public async Task<List<Pokemon>> GetPokemons(string pokemonAttribute)
        {
            if (string.IsNullOrEmpty(pokemonAttribute))
                return _searchPokemonByIdFromDB.SearchAndGetPokemon(pokemonAttribute);
            if (types.ContainsValue(pokemonAttribute))
                return _searchPokemonByTypeFromDB.SearchAndGetPokemon(pokemonAttribute);
            if (pokemonAttribute.All(char.IsDigit))
                return _searchPokemonByIdFromDB.SearchAndGetPokemon(pokemonAttribute);
            return _searchPokemonByNameFromDB.SearchAndGetPokemon(pokemonAttribute);
        }

        public bool HasPokemons()
        {
            using (var db = new ClientDataBase())
            {
                return db.Pokemons.ToList().Count > 0;
            }
        }

        public bool HasPokemonById(string id)
        {
            using (var db = new ClientDataBase())
            {
                return db.Pokemons.Any(p => p.Id.ToString().Equals(id));
            }
        }

        public bool HasPokemonByName(string name)
        {
            using (var db = new ClientDataBase())
            {
                return db.Pokemons.Any(p => p.Name.Equals(name));
            }
        }

        public bool HasPokemonByType(string type)
        {
            using (var db = new ClientDataBase())
            {
                return db.TypeElement.Any(p => p.Type.Name.Equals(type));
            }
        }

        #endregion

        #region Private Method

        private string GetIconImageFromType(string name)
        {
            if (name.Equals("normal"))
                return "https://upload.wikimedia.org/wikipedia/commons/thumb/a/aa/Pok%C3%A9mon_Normal_Type_Icon.svg/180px-Pok%C3%A9mon_Normal_Type_Icon.svg.png";
            if (name.Equals("fighting"))
                return "https://upload.wikimedia.org/wikipedia/commons/thumb/b/be/Pok%C3%A9mon_Fighting_Type_Icon.svg/180px-Pok%C3%A9mon_Fighting_Type_Icon.svg.png";
            if (name.Equals("flying"))
                return "https://upload.wikimedia.org/wikipedia/commons/thumb/e/e0/Pok%C3%A9mon_Flying_Type_Icon.svg/180px-Pok%C3%A9mon_Flying_Type_Icon.svg.png";
            if (name.Equals("poison"))
                return "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c4/Pok%C3%A9mon_Poison_Type_Icon.svg/180px-Pok%C3%A9mon_Poison_Type_Icon.svg.png";
            if (name.Equals("ground"))
                return "https://upload.wikimedia.org/wikipedia/commons/thumb/8/8d/Pok%C3%A9mon_Ground_Type_Icon.svg/180px-Pok%C3%A9mon_Ground_Type_Icon.svg.png";
            if (name.Equals("rock"))
                return "https://upload.wikimedia.org/wikipedia/commons/thumb/b/bb/Pok%C3%A9mon_Rock_Type_Icon.svg/180px-Pok%C3%A9mon_Rock_Type_Icon.svg.png";
            if (name.Equals("bug"))
                return "https://upload.wikimedia.org/wikipedia/commons/thumb/3/3c/Pok%C3%A9mon_Bug_Type_Icon.svg/180px-Pok%C3%A9mon_Bug_Type_Icon.svg.png";
            if (name.Equals("ghost"))
                return "https://upload.wikimedia.org/wikipedia/commons/thumb/a/a0/Pok%C3%A9mon_Ghost_Type_Icon.svg/180px-Pok%C3%A9mon_Ghost_Type_Icon.svg.png";
            if (name.Equals("steel"))
                return "https://upload.wikimedia.org/wikipedia/commons/thumb/3/38/Pok%C3%A9mon_Steel_Type_Icon.svg/180px-Pok%C3%A9mon_Steel_Type_Icon.svg.png";
            if (name.Equals("fire"))
                return "https://upload.wikimedia.org/wikipedia/commons/thumb/5/56/Pok%C3%A9mon_Fire_Type_Icon.svg/180px-Pok%C3%A9mon_Fire_Type_Icon.svg.png";
            if (name.Equals("water"))
                return "https://upload.wikimedia.org/wikipedia/commons/thumb/0/0b/Pok%C3%A9mon_Water_Type_Icon.svg/180px-Pok%C3%A9mon_Water_Type_Icon.svg.png";
            if (name.Equals("grass"))
                return "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f6/Pok%C3%A9mon_Grass_Type_Icon.svg/180px-Pok%C3%A9mon_Grass_Type_Icon.svg.png";
            if (name.Equals("electric"))
                return "https://upload.wikimedia.org/wikipedia/commons/thumb/a/a9/Pok%C3%A9mon_Electric_Type_Icon.svg/180px-Pok%C3%A9mon_Electric_Type_Icon.svg.png";
            if (name.Equals("psychic"))
                return "https://upload.wikimedia.org/wikipedia/commons/thumb/a/ab/Pok%C3%A9mon_Psychic_Type_Icon.svg/180px-Pok%C3%A9mon_Psychic_Type_Icon.svg.png";
            if (name.Equals("ice"))
                return "https://upload.wikimedia.org/wikipedia/commons/thumb/8/88/Pok%C3%A9mon_Ice_Type_Icon.svg/180px-Pok%C3%A9mon_Ice_Type_Icon.svg.png";
            if (name.Equals("dragon"))
                return "https://upload.wikimedia.org/wikipedia/commons/thumb/a/a6/Pok%C3%A9mon_Dragon_Type_Icon.svg/180px-Pok%C3%A9mon_Dragon_Type_Icon.svg.png";
            if (name.Equals("dark"))
                return "https://upload.wikimedia.org/wikipedia/commons/thumb/0/09/Pok%C3%A9mon_Dark_Type_Icon.svg/180px-Pok%C3%A9mon_Dark_Type_Icon.svg.png";
            if (name.Equals("fairy"))
                return "https://upload.wikimedia.org/wikipedia/commons/thumb/0/08/Pok%C3%A9mon_Fairy_Type_Icon.svg/180px-Pok%C3%A9mon_Fairy_Type_Icon.svg.png";
            return "https://cdn-icons-png.flaticon.com/512/5259/5259989.png";
        }

        #endregion

        #region Internal Method

        internal Pokemon GetPokemonByIdFind(Pokemon pokemon)
        {
            using (var db = new ClientDataBase())
            {
                return db.Pokemons.Find(pokemon.Id);
            }
        }
        
        #endregion
    }
}

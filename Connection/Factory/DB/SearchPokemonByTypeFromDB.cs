using Connection.DataBase;
using Connection.Dispatchers;
using Connection.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connection.Factory.DB
{
    public class SearchPokemonByTypeFromDB : ISearchPokemon
    {
        public List<Pokemon> SearchAndGetPokemon(string pokemonAttribute)
        {
            List<Types> typesList = null;
            List<PokemonElement> pokemonElementList = null;
            List<Pokemon> pokemonList = new List<Pokemon>();
            List<AbilityElement> abilityElementList = null;
            List<StatElement> StatElementList = null;
            List<TypeElement> TypeElementList = null;
            PokemonPokemon pokemonPokemon = null;

            typesList = CreateTypesList();

            foreach (var types in typesList)
            {
                if (types.name.Equals(pokemonAttribute))
                {
                    pokemonElementList = CreatePokemonElementList();

                    if (pokemonElementList != null)
                    {
                        foreach (var pokemon in pokemonElementList)
                        {
                            if (pokemon.TypesId == types.Id)
                            {
                                pokemonPokemon = CreatePokemonPokemon(pokemon);
                                CreatePokemonListFromPokemonPokemon(pokemonList, pokemonPokemon);
                            }
                        }
                    }

                    if (pokemonList != null)
                    {
                        abilityElementList = CreateAbilityElementList(pokemonList, abilityElementList);
                        StatElementList = CreateStatElementList(pokemonList, StatElementList);
                        TypeElementList = CreateTypeElementList(pokemonList, TypeElementList);
                    }


                    if (TypeElementList != null)
                    {
                        AddTypesInTypeElementList(TypeElementList);
                    }

                    if (StatElementList != null)
                    {
                        AddStatsInStatElementList(StatElementList);
                    }

                    if (abilityElementList != null)
                    {
                        AddAbilityInAbilityElementList(abilityElementList);
                    }

                    if (pokemonList != null)
                    {
                        foreach (var pk in pokemonList)
                        {
                            pk.Abilities = abilityElementList.FindAll(p => p.PokemonId == pk.Id);
                            pk.Stats = StatElementList.FindAll(p => p.PokemonId == pk.Id);
                            pk.Types = TypeElementList.FindAll(p => p.PokemonId == pk.Id);
                            AddSpritesInPokemon(pk);
                            AddOtherInPokemon(pk);
                            AddHomeInPokemon(pk);
                        }
                    }
                }
            }

            return pokemonList;
        }

        public bool ThisPokemonExist(string pokemonAttribute)
        {
            return false;
        }

        #region Private Method

        private void AddHomeInPokemon(Pokemon pk)
        {
            using (var db = new ClientDataBase())
            {
                pk.Sprites.Other.Home = db.Home.Find(pk.Sprites.Other.HomeId);
            }
        }

        private void AddOtherInPokemon(Pokemon pk)
        {
            using (var db = new ClientDataBase())
            {
                pk.Sprites.Other = db.Other.Find(pk.Sprites.OtherId);
            }
        }

        private void AddSpritesInPokemon(Pokemon pk)
        {
            using (var db = new ClientDataBase())
            {
                pk.Sprites = db.Sprites.Find(pk.SpritesId);
            }
        }

        private void AddAbilityInAbilityElementList(List<AbilityElement> abilityElementList)
        {
            foreach (var abilities in abilityElementList)
            {
                using (var db = new ClientDataBase())
                {
                    abilities.Ability = db.TypeClass.Find(abilities.AbilityId);
                }
            }
        }

        private void AddStatsInStatElementList(List<StatElement> StatElementList)
        {
            foreach (var stat in StatElementList)
            {
                using (var db = new ClientDataBase())
                {
                    stat.Stat = db.StatStat.Find(stat.StatId);
                }
            }
        }

        private void AddTypesInTypeElementList(List<TypeElement> TypeElementList)
        {
            foreach (var type in TypeElementList)
            {
                using (var db = new ClientDataBase())
                {
                    type.Type = db.TypeClass.Find(type.TypeId);
                }
            }
        }

        private List<TypeElement> CreateTypeElementList(List<Pokemon> pokemonList, List<TypeElement> TypeElementList)
        {
            using (var db = new ClientDataBase())
            {
                foreach (var pk in pokemonList)
                {
                    return db.TypeElement.ToList().FindAll(p => p.PokemonId == pk.Id);
                }
            }
            return null;
        }

        private List<StatElement> CreateStatElementList(List<Pokemon> pokemonList, List<StatElement> StatElementList)
        {
            using (var db = new ClientDataBase())
            {
                foreach (var pk in pokemonList)
                {
                    return db.StatElement.ToList().FindAll(p => p.PokemonId == pk.Id);
                }
            }
            return null;
        }

        private List<AbilityElement> CreateAbilityElementList(List<Pokemon> pokemonList, List<AbilityElement> abilityElementList)
        {
            using (var db = new ClientDataBase())
            {
                foreach (var pk in pokemonList)
                {
                    return db.AbilityElement.ToList().FindAll(p => p.PokemonId == pk.Id);
                }
            }
            return null;
        }

        private void CreatePokemonListFromPokemonPokemon(List<Pokemon> pokemonList, PokemonPokemon pokemonPokemon)
        {
            using (var db = new ClientDataBase())
            {
                if (db.Pokemons.ToList().Find(p => p.Name.Equals(pokemonPokemon.Name)) != null)
                    pokemonList.Add(db.Pokemons.ToList().Find(p => p.Name.Equals(pokemonPokemon.Name)));
            }
        }

        private PokemonPokemon CreatePokemonPokemon(PokemonElement pokemon)
        {
            using (var db = new ClientDataBase())
            {
                return db.PokemonPokemon.Find(pokemon.PokemonId);
            }
        }

        private List<PokemonElement> CreatePokemonElementList()
        {
            using (var db = new ClientDataBase())
            {
                return db.PokemonElement.ToList();
            }
        }

        private List<Types> CreateTypesList()
        {
            using (var db = new ClientDataBase())
            {
                return db.Types.ToList();
            }
        }

        #endregion
    }
}

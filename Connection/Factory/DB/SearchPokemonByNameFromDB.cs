using Connection.DataBase;
using Connection.Dispatchers;
using Connection.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Connection.Factory.DB
{
    public class SearchPokemonByNameFromDB : ISearchPokemon
    {
        public List<Pokemon> SearchAndGetPokemon(string pokemonAttribute)
        {
            List<Pokemon> pokemonList = null;
            List<AbilityElement> abilityElementList = null;
            List<StatElement> StatElementList = null;
            List<TypeElement> TypeElementList = null;
                        
            using (var db = new ClientDataBase())
            {
                pokemonList = db.Pokemons.ToList().FindAll(p => p.Name.StartsWith(pokemonAttribute));
            }

            using (var db = new ClientDataBase())
            {
                foreach (var pk in pokemonList)
                {
                    abilityElementList = db.AbilityElement.ToList().FindAll(p => p.PokemonId == pk.Id);
                }
            }

            using (var db = new ClientDataBase())
            {
                foreach (var pk in pokemonList)
                {
                    StatElementList = db.StatElement.ToList().FindAll(p => p.PokemonId == pk.Id);
                }
            }

            using (var db = new ClientDataBase())
            {
                foreach (var pk in pokemonList)
                { 
                    TypeElementList = db.TypeElement.ToList().FindAll(p => p.PokemonId == pk.Id);
                }
            }

            if (TypeElementList != null)
            {
                foreach (var type in TypeElementList)
                {
                    using (var db = new ClientDataBase())
                    {
                        type.Type = db.TypeClass.Find(type.TypeId);
                    }
                }
            }

            if (StatElementList != null)
            {
                foreach (var stat in StatElementList)
                {
                    using (var db = new ClientDataBase())
                    {
                        stat.Stat = db.StatStat.Find(stat.StatId);
                    }
                }
            }

            if (abilityElementList != null)
            {
                foreach (var abilities in abilityElementList)
                {
                    using (var db = new ClientDataBase())
                    {
                        abilities.Ability = db.TypeClass.Find(abilities.AbilityId);
                    }
                }
            }

            if (pokemonList != null)
            {
                foreach (var pk in pokemonList)
                {
                    pk.Abilities = abilityElementList.FindAll(p => p.PokemonId == pk.Id);
                    pk.Stats = StatElementList.FindAll(p => p.PokemonId == pk.Id);
                    pk.Types = TypeElementList.FindAll(p => p.PokemonId == pk.Id);

                    using (var db = new ClientDataBase())
                    {
                        pk.Sprites = db.Sprites.Find(pk.SpritesId);
                    }

                    using (var db = new ClientDataBase())
                    {
                        pk.Sprites.Other = db.Other.Find(pk.Sprites.OtherId);
                    }

                    using (var db = new ClientDataBase())
                    {
                        pk.Sprites.Other.Home = db.Home.Find(pk.Sprites.Other.HomeId);
                    }
                }
            }
            return pokemonList;
        }

        public bool ThisPokemonExist(string pokemonAttribute)
        {
            List<Pokemon> pokemonList;

            using (var db = new ClientDataBase())
            {
                pokemonList = db.Pokemons.ToList().FindAll(p => p.Name.StartsWith(pokemonAttribute));
            }
            return pokemonList.Count > 0;
        }
    }
}

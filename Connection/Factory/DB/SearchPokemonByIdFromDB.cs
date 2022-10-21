using Connection.DataBase;
using Connection.Dispatchers;
using Connection.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Connection.Factory.DB
{
    public class SearchPokemonByIdFromDB : ISearchPokemon
    {
        private List<Pokemon> _pokemonList = null;

        public List<Pokemon> SearchAndGetPokemon(string pokemonAttribute)
        {
            if (string.IsNullOrEmpty(pokemonAttribute))
            {
                CreatePokemonWithOutParameters();
                return _pokemonList;
            }

            CreatePokemonWithParameters(pokemonAttribute);
           
            return _pokemonList;
        }

        private void CreatePokemonWithParameters(string pokemonAttribute)
        {
            List<AbilityElement> abilityElementList = null;
            List<StatElement> StatElementList = null;
            List<TypeElement> TypeElementList = null;

            using (var db = new ClientDataBase())
            {
                _pokemonList = db.Pokemons.ToList().FindAll(p => p.Id.ToString().StartsWith(pokemonAttribute));
            }

            using (var db = new ClientDataBase())
            {
                foreach (var pk in _pokemonList)
                {
                    abilityElementList = db.AbilityElement.ToList().FindAll(p => p.PokemonId == pk.Id);
                }
            }

            using (var db = new ClientDataBase())
            {
                foreach (var pk in _pokemonList)
                {
                    StatElementList = db.StatElement.ToList().FindAll(p => p.PokemonId == pk.Id);
                }
            }

            using (var db = new ClientDataBase())
            {
                foreach (var pk in _pokemonList)
                {
                    TypeElementList = db.TypeElement.ToList().FindAll(p => p.PokemonId == pk.Id);
                }
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

            if (_pokemonList != null)
            {
                foreach (var pk in _pokemonList)
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
        }

        private void CreatePokemonWithOutParameters()
        {
            List<AbilityElement> abilityElementList = null;
            List<StatElement> StatElementList = null;
            List<TypeElement> TypeElementList = null;

            using (var db = new ClientDataBase())
            {
                _pokemonList = db.Pokemons.ToList();
            }

            using (var db = new ClientDataBase())
            {
                abilityElementList = db.AbilityElement.ToList();
            }

            using (var db = new ClientDataBase())
            {
                StatElementList = db.StatElement.ToList();
            }

            using (var db = new ClientDataBase())
            {
                TypeElementList = db.TypeElement.ToList();
            }

            AddTypesInTypeElementList(TypeElementList);

            AddStatsInStatElementList(StatElementList);

            AddAbilityInAbilityElementList(abilityElementList);

            foreach (var pk in _pokemonList)
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

        public bool ThisPokemonExist(string pokemonAttribute)
        {            
            List<Pokemon> pokemonList;

            using (var db = new ClientDataBase())
            {
                pokemonList = db.Pokemons.ToList().FindAll(p => p.Id.ToString().StartsWith(pokemonAttribute));
            }
            return pokemonList.Count > 0;
        }
    }
}

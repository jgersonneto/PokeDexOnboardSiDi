using CommunityToolkit.Mvvm.ComponentModel;
using Connection.BLL;
using Connection.Dispatchers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using Windows.Storage;

namespace PokedexCore.ViewModel
{
    public class PokemonDetailsPageViewModel : ObservableObject
    {
        #region Private Variables

        private ObservableCollection<Pokemon> _observerPokemon = new ObservableCollection<Pokemon>();
        private ObservableCollection<TypeElement> _observerTypePokemon = new ObservableCollection<TypeElement>();
        private Pokemon _pokemon = new Pokemon();
        private BoPokemonDataBase _boPokemonDataBase = App.BoPokemonDataBase;

        private string _imagePokemon;
        private string _id;
        private string _pokemonName;
        private string _imageTypePrimary;
        private string _imageTypeSecundary;
        private string _weight;
        private string _heigth;
        private string _baseExperience;
        private int _hp;
        private int _attack;
        private int _defense;
        private int _specialAttack;
        private int _specialDefense;
        private int _speed;

        #endregion

        public PokemonDetailsPageViewModel()
        {
            Initialization();
        }

        #region Properties

        public BoPokemonDataBase BoPokemonDataBase 
        { 
            get => _boPokemonDataBase; 
            set => _boPokemonDataBase = value; 
        }

        public ObservableCollection<TypeElement> ObserverTypePokemon
        {
            get => _observerTypePokemon;
            set => SetProperty(ref _observerTypePokemon, value);
        }

        public ObservableCollection<Pokemon> ObserverPokemon
        {
            get => _observerPokemon;
            set => SetProperty(ref _observerPokemon, value);
        }

        public Pokemon Pokemon 
        {
            get => _pokemon;
            set => SetProperty(ref _pokemon, value);
        }

        public string ImagePokemon
        {
            get => _imagePokemon;
            set => SetProperty(ref _imagePokemon, value);
        }

        public string Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        public string PokemonName
        {
            get => _pokemonName;
            set => SetProperty(ref _pokemonName, value);
        }

        public string ImageTypePrimary
        {
            get => _imageTypePrimary;
            set => SetProperty(ref _imageTypePrimary, value);
        }

        public string ImageTypeSecudary
        {
            get => _imageTypeSecundary;
            set => SetProperty(ref _imageTypeSecundary, value);
        }

        public string Weight
        {
            get => _weight;
            set => SetProperty(ref _weight, value);
        }

        public string Height
        {
            get => _heigth;
            set => SetProperty(ref _heigth, value);
        }

        public string BaseExperience
        {
            get => _baseExperience;
            set => SetProperty(ref _baseExperience, value);
        }

        public int HP
        {
            get => _hp;
            set => SetProperty(ref _hp, value);
        }

        public int Attack
        {
            get => _attack;
            set => SetProperty(ref _attack, value);
        }

        public int Defense
        {
            get => _defense;
            set => SetProperty(ref _defense, value);
        }

        public int SpecialAttack
        {
            get => _specialAttack;
            set => SetProperty(ref _specialAttack, value);
        }

        public int SpecialDefense
        {
            get => _specialDefense;
            set => SetProperty(ref _specialDefense, value);
        }

        public int Speed
        {
            get => _speed;
            set => SetProperty(ref _speed, value);
        }

        #endregion

        #region Private Method

        private void Initialization()
        {
            LoadPokemon();
            if (_observerPokemon.Count != 0)
            {
                foreach (var type in _observerPokemon[0].Types)
                {
                    ObserverTypePokemon.Add(type);
                }
                Id = _observerPokemon[0].Id + "";
                PokemonName = _observerPokemon[0].Name;
                ImagePokemon = _observerPokemon[0].Sprites.Other.Home.Front_Default;
                ImageTypePrimary = _observerPokemon[0].Types[0].Type.IconName;
                if (_observerPokemon[0].Types.Count == 2)
                    ImageTypeSecudary = _observerPokemon[0].Types[1].Type.IconName;
                Weight = _observerPokemon[0].Weight + "";
                Height = _observerPokemon[0].Height + "";
                BaseExperience = _observerPokemon[0].Base_Experience + "";
                HP = CalculatePercent(_observerPokemon[0].Stats[0].Base_Stat);
                Attack = CalculatePercent(_observerPokemon[0].Stats[1].Base_Stat);
                Defense = CalculatePercent(_observerPokemon[0].Stats[2].Base_Stat);
                SpecialAttack = CalculatePercent(_observerPokemon[0].Stats[3].Base_Stat);
                SpecialDefense = CalculatePercent(_observerPokemon[0].Stats[4].Base_Stat);
                Speed = CalculatePercent(_observerPokemon[0].Stats[5].Base_Stat);
            }
        }

        private int CalculatePercent(int valeu)
        {
            int percent = (int)((valeu * 100) / 180);
            return percent;
        }

        private async void LoadPokemon()
        {
            Pokemon.Name = MainWindow.PokeName;
            try
            {
                BoPokemonDataBase = new BoPokemonDataBase(Path.Combine(ApplicationData.Current.LocalFolder.Path, "PokeDexOnBoard.db"));
                List<Pokemon> pokemons = await App.BoPokemonDataBase.GetPokemons(Pokemon.Name);
                foreach (Pokemon pkm in pokemons)
                    ObserverPokemon.Add(pkm);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            
        }

        #endregion
    }
}

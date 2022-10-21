using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Connection.DataBase;
using Connection.Dispatchers;
using PokedexUwp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Storage;
using Windows.UI.Xaml.Controls;

namespace PokedexUwp.ViewModel
{
    public class PokedexPageViewModel : ObservableObject
    {
        #region Private Variables

        private ObservableCollection<string> _suggestionList = new ObservableCollection<string>();
        private AutoSuggestionList _autoSuggestionList = new AutoSuggestionList();

        private ObservableCollection<Pokemon> _observerListPokemons = new ObservableCollection<Pokemon>();
        private List<Pokemon> _pokemonSearchResult = new List<Pokemon>();

        private Pokemon _pokemon = new Pokemon();

        private string _textAutoSuggestBox;
        private string _textAutoSuggestBoxTextChanged;
        private bool _isVisibleAutoSuggestBox = true;
        private bool _isVisibleProgressRing = false;

        #endregion

        public PokedexPageViewModel()
        {
            LoadPokemons();
            SearchPokemonCommand = new RelayCommand(SearchPokemon);
            NavegationPageDetailsCommand = new RelayCommand<Pokemon>(NavegationPDetails);
        }

        #region Properties

        public Pokemon Pokemon
        {
            get => _pokemon;
            set => SetProperty(ref _pokemon, value);
        }
        
        public ObservableCollection<Pokemon> ObserverListPokemons
        {
            get => _observerListPokemons;
            set => SetProperty(ref _observerListPokemons, value);
        }

        public List<Pokemon> PokemonSearchResult
        {
            get => _pokemonSearchResult;
            set => _pokemonSearchResult = value;
        }

        public string TextAutoSuggestBox
        {
            get => _textAutoSuggestBox;
            set => SetProperty(ref _textAutoSuggestBox, value);
        }

        public string TextAutoSuggestBoxTextChanged 
        {
            get => _textAutoSuggestBoxTextChanged;
            set => SetProperty(ref _textAutoSuggestBoxTextChanged, value);
        }

        public bool IsVisibleAutoSuggestBox 
        {
            get => _isVisibleAutoSuggestBox;
            set => SetProperty(ref _isVisibleAutoSuggestBox, value);
        }

        public bool IsVisibleProgressRing
        {
            get => _isVisibleProgressRing;
            set => SetProperty(ref _isVisibleProgressRing, value);
        }

        #endregion

        #region ICommand

        public ICommand NavegationPageDetailsCommand
        {
            get;
            private set;
        }

        public IRelayCommand SearchPokemonCommand { get; }

        #endregion

        #region Private Method

        #region Command Method

        private void NavegationPDetails(Pokemon p)
        {
            //PokemonOther.Clear();
            //PokemonOther.Add(p);
        }

        #endregion

        private void LoadPokemons()
        {
            List<Pokemon> pokemons = App.BoPokemonDataBase.GetAllPokemons();
            foreach (Pokemon pkm in pokemons)
                ObserverListPokemons.Add(pkm);
        }

        #endregion

        #region Public Method

        public async void SearchPokemon()
        {
            IsVisibleAutoSuggestBox = false;
            IsVisibleProgressRing = true;

            var managerConnection = App.BoPokemonDataBase;
            ObserverListPokemons.Clear();
            await Task.Run(() =>
            {
                var taskApi = managerConnection.SearchPokemonsInApi(TextAutoSuggestBox);
                taskApi.Wait();
            });

            PokemonSearchResult = await managerConnection.GetPokemons(TextAutoSuggestBox);
            foreach (var pokemon in PokemonSearchResult)
            {
                _autoSuggestionList.SuggestionList.Add(pokemon.Name);
                ObserverListPokemons.Add(pokemon);
            };
            
            //searchForTenPages();

            IsVisibleAutoSuggestBox = true;
            IsVisibleProgressRing = false;
            TextAutoSuggestBox = "";
        }

        public void GetAutoSuggestionList(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            _suggestionList.Clear();
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {

                var splitText = sender.Text.ToLower().Split(" ");
                foreach (var suggestion in _autoSuggestionList.SuggestionList)
                {
                    var found = splitText.All((key) =>
                    {
                        return suggestion.ToLower().Contains(key);
                    });
                    if (found)
                    {
                        _suggestionList.Add(suggestion);
                    }
                }
                if (_suggestionList.Count == 0)
                {
                    _suggestionList.Add("No results found");
                }
                sender.ItemsSource = _suggestionList;
            }
            TextAutoSuggestBox = sender.Text;
        }

        public void AutoSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            TextAutoSuggestBox = args.SelectedItem.ToString();
            sender.Text = args.SelectedItem.ToString();
        }

        #endregion
    }
}

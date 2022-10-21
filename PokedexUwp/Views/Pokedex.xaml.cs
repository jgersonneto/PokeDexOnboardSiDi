using Connection.Dispatchers;
using System;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace PokedexUwp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Pokedex : Page
    {
        public Pokedex()
        {
            this.InitializeComponent();
        }

        private void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            viewModels2.GetAutoSuggestionList(sender, args);
            //viewModels2.TextAutoSuggestBox = sender.Text;
        }

        private void AutoSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            viewModels2.AutoSuggestBox_SuggestionChosen(sender, args);
        }

        private void AutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            viewModels2.SearchPokemon();
        }

        private async void ButtonPageNavigation(object sender, RoutedEventArgs e)
        {
            if (sender is Button btnPokemon && btnPokemon.CommandParameter is Pokemon param)
                await Launcher.LaunchUriAsync(new Uri($"com.pokedexwpf://?pokemon={param.Name}"));
        }

    }
}

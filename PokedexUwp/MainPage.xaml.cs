using PokedexUwp.Views;
using System;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace PokedexUwp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            FrameMenuItem.Navigate(typeof(Pokedex));
        }

        private void nvSample_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                FrameMenuItem.Navigate(typeof(Pokedex));
            }
            else
            {
                NavigationViewItem item = args.SelectedItem as NavigationViewItem;

                switch (item.Tag.ToString())
                {
                    case "Pokedex":
                        FrameMenuItem.Navigate(typeof(Pokedex));
                        break;
                    case "Add Pokemon":
                        FrameMenuItem.Navigate(typeof(RegisterPokemon));
                        break;
                }
            }
        }

    }
}

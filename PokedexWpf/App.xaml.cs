using Connection.BLL;
using System;
using System.IO;
using System.Windows;
using Windows.Storage;

namespace PokedexWpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static BoPokemonDataBase BoPokemonDataBase { get; internal set; }

        public App()
        {
            StartDataBase();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
        }

        private async void StartDataBase()
        {
            BoPokemonDataBase = new BoPokemonDataBase(Path.Combine(ApplicationData.Current.LocalFolder.Path, "PokeDexOnBoard.db"));
            await BoPokemonDataBase.CreateDataBase();
        }
    }
}

using Connection.BLL;
using System.IO;
using System.Windows;
using Windows.Storage;

namespace PokedexCore
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static BoPokemonDataBase BoPokemonDataBase { get; internal set; }

        public static string[] Arguments { get; internal set; }

        public App()
        {
            StartDataBase();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Arguments = e.Args;
        }

        private async void StartDataBase()
        {
            BoPokemonDataBase = new BoPokemonDataBase(Path.Combine(ApplicationData.Current.LocalFolder.Path, "PokeDexOnBoard.db"));
            await BoPokemonDataBase.CreateDataBase();
        }
    }
}

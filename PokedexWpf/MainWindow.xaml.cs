using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;

namespace PokedexWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnBackUwp_Click(object sender, RoutedEventArgs e)
        {
            Task taskUwp = Task.Run(() => Process.Start("com.pokedexuwp://"));
            taskUwp.Wait();

            this.Close();
        }
    }
}

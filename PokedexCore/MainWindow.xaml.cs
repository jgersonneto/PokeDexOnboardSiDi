using Connection.Dispatchers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PokedexCore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string PokeName { get; set; }

        public MainWindow()
        {
            foreach (string arg in App.Arguments)
                PokeName += $"\r\n{arg}";
            PokeName = $"{PokeName.Split('?')?[1].Split('&')?.First(p => p.Contains("pokemon"))?.Split('=')?[1].Trim()}";
            InitializeComponent();
        }
    }
}

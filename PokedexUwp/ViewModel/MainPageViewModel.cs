using CommunityToolkit.Mvvm.ComponentModel;
using PokedexUwp.Models;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;

namespace PokedexUwp.ViewModel
{
    public class MainPageViewModel : ObservableObject
    {
        private ObservableCollection<Category> categories = new ObservableCollection<Category>();

        public ObservableCollection<Category> Categories
        {
            get => categories;
            set => SetProperty(ref categories, value);
        }

        public Category Category
        {
            get;
            set;
        }

        public MainPageViewModel()
        {
            categories.Add(new Category { Name = "Category 1", Glyph = Symbol.Home, Tooltip = "This is category 1" });
            categories.Add(new Category { Name = "Category 2", Glyph = Symbol.Keyboard, Tooltip = "This is category 2" });
            categories.Add(new Category { Name = "Category 3", Glyph = Symbol.Library, Tooltip = "This is category 3" });
            categories.Add(new Category { Name = "Category 4", Glyph = Symbol.Mail, Tooltip = "This is category 4" });
        }
    }
}

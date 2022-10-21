using Connection.Dispatchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokedexUwp.Models
{
    public class AutoSuggestionList
    {
        private List<string> _suggestionList = new List<string>();

        public AutoSuggestionList()
        {
            SuggestionList.Add("normal");
            SuggestionList.Add("fighting");
            SuggestionList.Add("flying");
            SuggestionList.Add("poison");
            SuggestionList.Add("ground");
            SuggestionList.Add("rock");
            SuggestionList.Add("bug");
            SuggestionList.Add("ghost");
            SuggestionList.Add("steel");
            SuggestionList.Add("fire");
            SuggestionList.Add("water");
            SuggestionList.Add("grass");
            SuggestionList.Add("electric");
            SuggestionList.Add("psychic");
            SuggestionList.Add("ice");
            SuggestionList.Add("dragon");
            SuggestionList.Add("dark");
            SuggestionList.Add("fairy");
            SuggestionList.Add("unknown");
            SuggestionList.Add("shadow");

            var list = LoadPokemons();

            foreach (var pk in list)
            {
                SuggestionList.Add(pk.Name);
            }
        }        

        public List<string> SuggestionList 
        { 
            get => _suggestionList; 
            set => _suggestionList = value; 
        }

        private List<Pokemon> LoadPokemons()
        {
            return App.BoPokemonDataBase.GetAllPokemons();
        }
    }
}

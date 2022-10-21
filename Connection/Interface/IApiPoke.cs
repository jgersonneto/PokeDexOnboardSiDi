using Connection.Dispatchers;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Connection.Interface
{
    public interface IApiPoke
    {
        [Get("/type/{name}")]
        Task<Types> GetTypesAsyncByType(string name);

        [Get("/pokemon/{name}")]
        Task<Pokemon> GetPokemonAsyncByName(string name);

        [Get("/pokemon/{id}")]
        Task<Pokemon> GetPokemonAsyncById(int id);
    }
}

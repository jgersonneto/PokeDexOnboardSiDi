using Connection.Dispatchers;
using Connection.Interface;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Connection.Api
{
    public class ApiService
    {
        #region Public Static Method

        public static async Task<Pokemon> ApiPokeById(int id)
        {
            try
            {
                var idCliente = RestService.For<IApiPoke>("https://pokeapi.co/api/v2");
                var address = await idCliente.GetPokemonAsyncById(id);

                return address;
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro na consulta pokemon: " + e.Message);
            }
            return null;
        }

        public static async Task<Types> ApiPokeByTypes(string type)
        {
            try
            {
                var idCliente = RestService.For<IApiPoke>("https://pokeapi.co/api/v2");

                var address = await idCliente.GetTypesAsyncByType(type);

                return address;
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro na consulta pokemon: " + e.Message);
            }
            return null;
        }

        public static async Task<Pokemon> ApiPokeByName(string name)
        {
            try
            {
                var idCliente = RestService.For<IApiPoke>("https://pokeapi.co/api/v2");

                var address = await idCliente.GetPokemonAsyncByName(name);


                return address;
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro na consulta pokemon: " + e.Message);
            }
            return null;
        }

        #endregion
    }
}

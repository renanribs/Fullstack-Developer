using System;
using System.Text.Json;

namespace AppApi.Services
{
    public class BuscarApiMaximaService
    {


        protected List<T> ListarDaApiMaxima<T>(string Objeto)
        {
            List<T>? resultado = null;

            var client = new HttpClient();
            client.BaseAddress = new Uri("https://private-anon-20d1fb1e40-maximatech.apiary-mock.com/");
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage httpResponseMessage = client.GetAsync(Objeto).Result;
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                resultado = JsonSerializer.Deserialize<List<T>>(httpResponseMessage.Content.ReadAsStringAsync().Result,
                    new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            }
            
            if (resultado == null)
            {
                resultado = new List<T>();
            }

            return resultado;

        }
    }
}

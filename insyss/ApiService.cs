using insyss.Clases;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace insyss
{
    public class ApiService
    {
        private static readonly HttpClient client = new HttpClient();


        public async Task<List<clsTodos>> GetApiDataAsync(string endpoint, Dictionary<string, string> parameters)
        {
            // Construir la URL con los parámetros
            var uriBuilder = new UriBuilder(endpoint);
            var query = System.Web.HttpUtility.ParseQueryString(string.Empty);
            foreach (var param in parameters)
            {
                query[param.Key] = param.Value;
            }
            uriBuilder.Query = query.ToString();

            // Realizar la solicitud GET
            var response = await client.GetAsync(uriBuilder.ToString());
            response.EnsureSuccessStatusCode();

            // Leer y deserializar la respuesta
            var responseBody = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<List<clsTodos>>(responseBody);

            return data;
        }
    }
}

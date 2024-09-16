using insyss.Clases;
using Newtonsoft.Json;
using service.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace insyss
{
    public class ApiService
    {
        private static readonly HttpClient client = new HttpClient();

        public async Task<List<clsTodos>> GetApiDataAsync(string endpoint, Dictionary<string, string> parameters)
        {
           
            // Construir la URL con los parámetros
            var uriBuilder = new UriBuilder(endpoint);
            Utilidades.EscribirEvento("Inicia lectura de EndPoint: "+ endpoint, System.Diagnostics.EventLogEntryType.Information);

            var query = System.Web.HttpUtility.ParseQueryString(string.Empty);

            Utilidades.EscribirEvento("Asigna Parametros", System.Diagnostics.EventLogEntryType.Information);
            foreach (var param in parameters)
            {
                query[param.Key] = param.Value;
            }
            uriBuilder.Query = query.ToString();

            Utilidades.EscribirEvento("Realiza solicitud GET", System.Diagnostics.EventLogEntryType.Information);
            // Realizar la solicitud GET
            var response = await client.GetAsync(uriBuilder.ToString());
            response.EnsureSuccessStatusCode();            

            // Leer y deserializar la respuesta
            var responseBody = await response.Content.ReadAsStringAsync();

            Utilidades.EscribirEvento("Devuelve datos de EndPoint", System.Diagnostics.EventLogEntryType.Information);

            var data = JsonConvert.DeserializeObject<List<clsTodos>>(responseBody);

            Utilidades.EscribirEvento("Registros encontrados: "+ data.Count, System.Diagnostics.EventLogEntryType.Information);

            return data;

        }
    }
}

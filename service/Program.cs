using insyss;
using service.Clases;

public class Program
{
    public static async Task Main(string[] args)
    {
        try
        {
            var apiService = new ApiService();
            var parameters = new Dictionary<string, string>
            {
                // Agrega más parámetros según sea necesario
                //{ "id","1" }
            };

            var data = await apiService.GetApiDataAsync(Propiedades.UrlEndPoint, parameters);

            foreach (var item in data)
            {
                string respuesta = $"ID: {item.id}, Title: {item.title}, Description: {item.description}";
                Console.WriteLine(respuesta);
                Utilidades.EscribirEvento(respuesta, System.Diagnostics.EventLogEntryType.Information);
            }

        }
        catch (Exception ex)
        {
            Utilidades.EscribirEvento("Hubo un error al consultar el método: "+ex.Message, System.Diagnostics.EventLogEntryType.Error);
        }
        
    }
}

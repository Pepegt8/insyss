using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace service.Clases
{
    public static partial class Propiedades
    {
        static Propiedades()
        {
            urlLog = "C:\\LogApi\\LogApi.txt";
            urlEndPoint = "https://container-app-api.whiteflower-63929439.centralus.azurecontainerapps.io/api/todos";
        }

        public static string UrlLog { get { return urlLog; } }
        private static string urlLog = "";

        public static string UrlEndPoint { get { return urlEndPoint; } }
        private static string urlEndPoint = "";
    }
}

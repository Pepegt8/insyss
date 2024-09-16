using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace service.Clases
{
    enum LogEventType
    { Error, Falla, Exito, Notificacion }

    enum LogEventAppType
    { Interfaces, Servicio, Log }

    class LogEvent
    {
        public string Url
        {
            get { return url; }
            set { url = value; }
        }
        private string url;

        public DateTime Tiempo
        {
            get { return tiempo; }
        }
        private DateTime tiempo = DateTime.Now;

        public string Formato
        {
            get { return formato; }
            set { formato = value; }
        }
        private string formato = "{0}";

        public string SeparadorDeLineas
        {
            get { return separadorDeLineas; }
        }
        private string separadorDeLineas = Environment.NewLine;

        public static LogEventAppType AppType { get { return appType; } set { appType = value; } }
        private static LogEventAppType appType = LogEventAppType.Log;

        public LogEvent() { }

        public LogEvent(string strUrl, bool SobreEscribir = false)
        {
            FileStream fileLog;
            if (string.IsNullOrEmpty(strUrl)) throw new ArgumentNullException("url", "No se ha ingresado ningun valor para la ubicacion del archivo.");

            this.url = string.Format(strUrl, DateTime.Now.ToString("yyyyMMdd"));

            string nombreLog = Path.GetFileName(url);


            if (AppType == LogEventAppType.Servicio)
            {
                nombreLog = "SRV_" + nombreLog;
            }
            else if (appType == LogEventAppType.Interfaces)
            {
                nombreLog = "IFS_" + nombreLog;
            }

            this.url = Path.Combine(Path.GetDirectoryName(url), nombreLog);

            if (!File.Exists(url) && SobreEscribir == false)
            {
                if (!Directory.Exists(Path.GetDirectoryName(this.url)))
                    Directory.CreateDirectory(Path.GetDirectoryName(this.url));

                // Si no existe el archivo se crea para su escritura.
                FileStream streamFileLog = File.OpenWrite(url);

                streamFileLog.Close();
            }
            else
            {
                if (SobreEscribir == true)
                {
                    fileLog = File.Create(url);
                    fileLog.Close();
                }

            }
        }

        public void EscribirEvento(string str) { this.EscribirEvento(str, LogEventType.Exito); }

        public void EscribirEvento(string str, LogEventType Tipo)
        {
            if (string.IsNullOrEmpty(this.url)) throw new ArgumentNullException("No se a asignado un valor para la ubicacion del archivo.");

            if (File.Exists(url))
            {
                File.AppendAllText(this.url, string.Format(this.formato + this.separadorDeLineas, str));
            }
            else
            {
                throw new FileNotFoundException("El archivo de log no exite", url);
            }
        }

    }
}

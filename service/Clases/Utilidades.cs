using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace service.Clases
{
    public class Utilidades
    {
        /// <summary>
        /// Typo de log para escribir en el tipo de archivo
        /// </summary>
        internal static LogEventAppType LogAppType
        {
            get { return logAppType; }
            set { logAppType = value; }
        }
        private static LogEventAppType logAppType = LogEventAppType.Log;

        internal static EventLog BitacoraDeEventos { get { return bitacoraDeEventos; } set { bitacoraDeEventos = value; } }
        private static EventLog bitacoraDeEventos = null;

        public static void EscribirEvento(string Mensaje, EventLogEntryType Tipo)
        {
            LogEvent.AppType = LogAppType; // Se setea el tipo de log para anteponer srv al archivo
            LogEvent logEvent = new LogEvent(Propiedades.UrlLog);

            logEvent.Formato = DateTime.Now + " {0}";

            try
            {

                if (bitacoraDeEventos != null) bitacoraDeEventos.WriteEntry(Mensaje, Tipo);

                if (Tipo == EventLogEntryType.Error)
                {
                    logEvent.EscribirEvento("ERROR!: " + Mensaje);
                    Console.WriteLine("Error!: " + Mensaje);
                }
                else if (Tipo == EventLogEntryType.Warning)
                {
                    logEvent.EscribirEvento("==> " + Mensaje);
                    Console.WriteLine("==> " + Mensaje);
                }
                else
                {
                    logEvent.EscribirEvento(Mensaje);
                    Console.WriteLine(Mensaje);
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

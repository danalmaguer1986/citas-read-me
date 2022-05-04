using System;
using System.ComponentModel;
using System.Globalization;

namespace Negocio.Operacion
{
    public class Utilerias
    {
        public static string ObtieneDescripcionFecha(DateTime fecha, string descripcion)
        {
            return "Cita " + descripcion + " "
                            + fecha.ToString("MM/dd/yyyy")
                            + " hora: " + new DateTime(fecha.Year, fecha.Month, fecha.Day, fecha.Hour, 0, 0).ToString("HH:mm:ss tt", CultureInfo.InvariantCulture);
        }
         
        public enum DescripcionCita
        {
            [Description("Cita disponible ")] disponible = 1,
            [Description("Cita reservada ")] reservada = 2 
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Shared
{
    public static class Errors
    {
        public static class General
        { 
            public static string EntityNotFound(string entity) =>
                $"general.entityNotFound|{entity}";
        }
            public static class Citas
        {
            public static string General => "No se logró generar la cita, favor de validar.";
        }
        public static class Catalogos_Usuarios
        {
            public static string NoNombre => "Debe ingresar un nombre.";
            public static string NoPassword => "Debe ingresar un password.";
        }
        public static class Catalogos_Doctores
        {
            public static string NoNombre => "Debe ingresar un nombre.";
            public static string NotFound => "No se encontró el registro.";
        }
    }
}

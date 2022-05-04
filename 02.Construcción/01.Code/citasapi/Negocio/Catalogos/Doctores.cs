using CSharpFunctionalExtensions;
using Negocio.Shared;
using Entity = Negocio.Core.Entity;

namespace Negocio.Catalogos
{
    public class Doctores : Entity
    {
        protected Doctores()
        {

        }
        public static Result<Doctores> Crear(string nombre, string username, string password)
        {
            if (string.IsNullOrEmpty(nombre))
            {
                return Result.Failure<Doctores>(Errors.Catalogos_Doctores.NoNombre);
            } 
            return new Doctores
            {
                Nombre = nombre, 
                Enabled = true
            };
        }
        public virtual string Nombre { get; protected set; } 
        public virtual bool Enabled { get; protected set; }
        public virtual void Actualizar(string nombre, bool enabled)
        {
            Nombre = nombre;
        }
        public virtual void Baja()
        {
            this.Enabled = false;
        } 
    }
}

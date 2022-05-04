using CSharpFunctionalExtensions; 
using Negocio.Shared;
using Entity = Negocio.Core.Entity;

namespace Negocio.Catalogos
{
    public class Usuarios : Entity
    {
        protected Usuarios()
        {

        }
        public static Result<Usuarios> Crear(string nombre, string username, string password)
        {
            if (string.IsNullOrEmpty(nombre))
            {
                return Result.Failure<Usuarios>(Errors.Catalogos_Usuarios.NoNombre);
            }

            if (string.IsNullOrEmpty(password))
            {
                return Result.Failure<Usuarios>(Errors.Catalogos_Usuarios.NoPassword);
            }

            return new Usuarios
            {
                Nombre = nombre,
                Username = username,
                Password = password,
                Enabled = true
            };
        }
        public virtual string Nombre { get; protected set; }
        public virtual string Username { get; protected set; }
        public virtual string Password { get; protected set; }
        public virtual bool Enabled { get; protected set; }
        public virtual void Actualizar(string nombre, bool enabled)
        {
            Nombre = nombre; 
        }
        public virtual void Baja()
        {
            this.Enabled = false;
        }
        public virtual  void CambiarPassword(string password)
        {
            this.Password = password;
        }
    }
}

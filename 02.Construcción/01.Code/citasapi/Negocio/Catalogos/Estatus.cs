using Negocio.Core;

namespace Negocio.Catalogos
{
	public class Estatus : Entity
	{
		protected Estatus()
		{
		}
		#region Propiedades bd

		public virtual string Nombre { get; protected set; }
		public virtual string Tipo { get; protected set; }
		#endregion

		#region Métodos
		public virtual void Actualizar(string nombre, string tipo)
		{

			this.Nombre = nombre;
			this.Tipo = tipo;
		}
		#endregion
	}
}

using Negocio.Core;

namespace Negocio.Catalogos
{
    public class CatalogoGeneral : Entity
    {
        protected CatalogoGeneral()
        {

        }

        #region Propiedades bd
        public virtual string Nombre { get; protected set; }
        public virtual string Tipo { get; protected set; }
        #endregion

    }
}

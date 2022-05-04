using FluentNHibernate.Mapping;
using Negocio.Catalogos;

namespace Infraestructura.DataAccess.Catalogos
{
    public class UsuariosMap : ClassMap<Usuarios>
    {
        public UsuariosMap()
        {
            Table("CAT_Usuarios");

            Id(x => x.Id, "UsuarioId");
            Map(x => x.Nombre);
            Map(x => x.Username);
            Map(x => x.Password);
            Map(x => x.Enabled);

            Cache.ReadWrite();
        }
    }
}

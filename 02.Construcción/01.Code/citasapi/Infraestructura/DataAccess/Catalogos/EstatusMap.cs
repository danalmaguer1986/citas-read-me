using FluentNHibernate.Mapping;
using Negocio.Catalogos;

namespace Infraestructura.DataAccess.Catalogos
{
    public class EstatusMap : ClassMap<Estatus>
    {
        public EstatusMap()
        {
            Table("CAT_Estatus");

            Id(x => x.Id, "EstatusId");

            Map(x => x.Nombre);
            Map(x => x.Tipo);

        }
    }
}



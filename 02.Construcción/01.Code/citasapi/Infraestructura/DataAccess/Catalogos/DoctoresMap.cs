using FluentNHibernate.Mapping;
using Negocio.Catalogos;

namespace Infraestructura.DataAccess.Catalogos
{
    public class DoctoresMap : ClassMap<Doctores>
    {
        public DoctoresMap()
        {
            Table("CAT_Doctores");

            Id(x => x.Id, "DoctorId");
            Map(x => x.Nombre); 
            Map(x => x.Enabled);

            Cache.ReadWrite();
        }
    }
}

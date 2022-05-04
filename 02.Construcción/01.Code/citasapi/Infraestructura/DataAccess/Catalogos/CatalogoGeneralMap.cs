using FluentNHibernate.Mapping;
using Negocio.Catalogos; 
namespace Infraestructura.DataAccess.Catalogos
{
    public class CatalogoGeneralMap : ClassMap<CatalogoGeneral>
    {
        public CatalogoGeneralMap()
        {
            Table("CAT_CatalogoGeneral");

            Id(x => x.Id, "Id");
            Map(x => x.Nombre);
            Map(x => x.Tipo);

            Cache.ReadWrite();
        }
    }
}

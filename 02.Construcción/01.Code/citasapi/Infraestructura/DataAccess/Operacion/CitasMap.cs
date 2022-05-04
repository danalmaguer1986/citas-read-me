using FluentNHibernate.Mapping;
using Negocio.Operacion;
using static Negocio.Operacion.Citas;

namespace Infraestructura.DataAccess.Operacion
{
    internal class CitasMap : ClassMap<Citas>
    {
        public CitasMap()
        {
            Table("CIT_Citas");
            Id(x => x.Id, "CitaId");
            References(e => e.Doctor, "DoctorId");
            References(e => e.Usuario, "UsuarioId").Nullable();
            Map(e => e.FechaCita);
            Map(e => e.HoraCita);
            Map(e => e.Paciente);
            Map(e => e.CorreoElectronico); 
            Map(e => e.Estatus, "EstatusId").CustomType<EstatusCitas>();
            Map(e => e.AuditUser).Nullable();
            Map(e => e.AuditDate).Nullable();
        }
    }
}

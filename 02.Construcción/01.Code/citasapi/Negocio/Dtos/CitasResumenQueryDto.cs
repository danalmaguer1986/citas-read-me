using System; 

namespace Negocio.Dtos
{
    public class CitasResumenQueryDto
    {
        public string Descripcion { get; set; }
        public DateTime FechaCita { get; set; }
        public int HoraCita { get; set; }
        public string Doctor { get; set; }
        public string NombrePaciente { get; set; }
        public string CorreoElectronico { get; set; }
        public bool Disponible { get; set; }
    }
}

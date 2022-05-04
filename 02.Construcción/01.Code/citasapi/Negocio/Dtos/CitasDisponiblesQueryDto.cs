using System; 

namespace Negocio.Dtos
{
    public class CitasDisponiblesQueryDto
    {
        public string Descripcion { get; set; }
        public DateTime FechaCita { get; set; }
        public int HoraCita { get; set; }
        public bool Disponible { get; set; }
    }
}

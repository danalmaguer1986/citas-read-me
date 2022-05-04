namespace Negocio.Dtos
{
    public class IdNombreDto
    {
        public IdNombreDto(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
        }

        public IdNombreDto()
        {
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}

using System.Linq;
using Infraestructura.DataAccess.Core; 
using Microsoft.AspNetCore.Mvc;
using Negocio.Catalogos;
using Negocio.Dtos;

namespace citas_api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/source")]
    [ApiController]
    public class SourceController : BaseController
    {
        public SourceController(UnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        [HttpGet("doctores")]
        public IActionResult Doctores()
        {
            var result = UnitOfWork
                .Query<Doctores>()
                .Where(e => e.Enabled == true)
                .Select(e => new IdNombreDto
                {
                    Id = e.Id,
                    Nombre = e.Nombre
                })
                .ToList();

            return Ok(result);

        }
        [HttpGet("usuarios")]
        public IActionResult Usuarios()
        {
            var result = UnitOfWork
                .Query<Usuarios>()
                .Where(e => e.Enabled == true)
                .Select(e => new IdNombreDto
                {
                    Id = e.Id,
                    Nombre = e.Nombre
                })
                .ToList();

            return Ok(result);

        }
    }
}

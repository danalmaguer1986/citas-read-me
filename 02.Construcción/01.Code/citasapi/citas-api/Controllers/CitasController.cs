using Infraestructura.DataAccess.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Negocio.Core;
using Negocio.Operacion;

namespace citas_api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/citas")]
    [ApiController]
    public class CitasController : BaseController
    {
        private readonly Dispatcher _dispatcher;
        /// <summary>
        /// 
        /// </summary>
        public CitasController(UnitOfWork unitOfWork,
            Dispatcher dispatcher) : base(unitOfWork)
        {
            _dispatcher = dispatcher; 
        }

        [HttpGet("")]
        public IActionResult ObtenerCitas([FromQuery] CitasResumenQuery query)
        {
            var result = _dispatcher.Dispatch(query);
            return Ok(result);
        }
        [HttpGet("diponibles")]
        public IActionResult CitasDisponibles([FromQuery] CitasDisponiblesQuery query)
        {
            var result = _dispatcher.Dispatch(query);
            return Ok(result);
        }
        [HttpPost("")]
        public IActionResult Cita([FromForm] CitasNuevaCommand command)
        {
            var result = _dispatcher.Dispatch(command);
            return FromResult(result);
        }
    }
}

using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TiendaServicios.API.Libreria.Aplicacion;
using static TiendaServicios.API.Libreria.Aplicacion.ConsultarFiltro;

namespace TiendaServicios.API.Libreria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibreriasController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LibreriasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Crear(Nuevo.Ejecuta data)
        {
            return await _mediator.Send(data);
        }

        [HttpGet]
        public async Task<ActionResult<List<LibreriaDto>>> GetLibrerias()
        {
            var librerias = await _mediator.Send(new Consulta.ListaLibrerias());
            return librerias;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LibreriaDto>> GetLibreriaId(string id)
        {
            var consulta = new ConsultaPorId { LibreriaGuid = id };
            var resultado = await _mediator.Send(consulta);
            return resultado;
        }
    }
}

using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.API.CarritoCompras.Persistencia;
using TiendaServicios.API.CarritoCompras.RemoteInterface;

namespace TiendaServicios.API.CarritoCompras.Aplicacion
{
    public class Consulta
    {

        public class Ejecuta : IRequest<CarritoDto>
        {
            public int CarritoSesionId { get; set; }

        }

        public class Manejador : IRequestHandler<Ejecuta, CarritoDto>
        {
            private readonly CarritoContexto carritoContexto;
            private readonly ILibrosService librosService;

            public Manejador(CarritoContexto _carritoContexto, ILibrosService _librosService)
            {
                carritoContexto = _carritoContexto;
                librosService = _librosService;
            }

            public async Task<CarritoDto> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                //Obtenemos el Carrito 
                var carritoSesion = await carritoContexto.CarritoSesion.
                    FirstOrDefaultAsync(x => x.CarritoSesionId ==
                    request.CarritoSesionId);

                //Devuelve la Lista de Productos Detalle Solo para Conocer cada Detalle
                var carritoSesionDetalle = await carritoContexto.CarritoSesionDetalle.
                    Where(x => x.CarritoSesionId ==
                    request.CarritoSesionId).ToListAsync();

                var listaCarritoDto = new List<CarritoDetalleDto>();

                foreach (var libro in carritoSesionDetalle)
                {
                    //Invocamos al Microservicio Externo
                    var response = await librosService.
                        GetLibro(new System.Guid(libro.ProductoSeleccionado));

                    if (response.resultado)
                    {
                        //Se Accede si se Encuentra Algo en la Base de Datos
                        var objetoLibro = response.Libro; //Retorno un libroRemote
                        var carritoDetalle = new CarritoDetalleDto
                        {
                            TituloLibro = objetoLibro.Titulo,
                            FechaPublicacion = objetoLibro.FechaPublicacion,
                            Precio = objetoLibro.Precio,
                            LibroId = objetoLibro.LibreriaMaterialId
                        };

                        listaCarritoDto.Add(carritoDetalle);

                    }
                }

                //Llenamos el Objeto que Realmente es Necesario Retomar
                var carritoSesionDto = new CarritoDto
                {
                    CarritoId = carritoSesion.CarritoSesionId,
                    FechaCreacionSesion = carritoSesion.FechaCreacion,
                    UserName = carritoSesion.UserName,
                    ListaDeProductos = listaCarritoDto
                };

                return carritoSesionDto;
            }
        }

    }
}

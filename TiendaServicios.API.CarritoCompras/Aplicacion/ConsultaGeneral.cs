using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using TiendaServicios.API.CarritoCompras.Persistencia;
using Microsoft.EntityFrameworkCore;
using TiendaServicios.API.CarritoCompras.RemoteServices;
using System;
using TiendaServicios.API.CarritoCompras.RemoteInterface;
using System.Linq;
using AutoMapper;
using TiendaServicios.API.CarritoCompras.Modelo;
using static TiendaServicios.API.CarritoCompras.Aplicacion.ConsultaGeneral;

namespace TiendaServicios.API.CarritoCompras.Aplicacion
{
    public class ConsultaGeneral
    {
        public class ListaCarrito : IRequest<List<CarritoDto>>
        {
        }

        public class Manejador : IRequestHandler<ListaCarrito, List<CarritoDto>>
        {
            private readonly CarritoContexto carritoContexto;
            private readonly ILibrosService librosService;

            public Manejador(CarritoContexto _carritoContexto, ILibrosService _librosService)
            {
                carritoContexto = _carritoContexto;
                librosService = _librosService;
            }

            public async Task<List<CarritoDto>> Handle(ListaCarrito request, CancellationToken cancellationToken) // Función que maneja la solicitud y retorna una lista de CarritoDto
            {
                // Obtiene todos los registros de la tabla CarritoSesion
                var carritosSesion = await carritoContexto.CarritoSesion.ToListAsync();

                // Crea una lista de objetos CarritoDto
                var listaCarritosDto = new List<CarritoDto>();

                // Recorre cada registro de la tabla CarritoSesion
                foreach (var carritoSesion in carritosSesion)
                {
                    //Devuelve la Lista de Productos Detalle Solo para Conocer cada Detalle
                    var carritoSesionDetalle = await carritoContexto.CarritoSesionDetalle
                        // Obtiene todos los registros de la tabla CarritoSesionDetalle que corresponden al carrito de sesión actual
                        .Where(x => x.CarritoSesionId == carritoSesion.CarritoSesionId).ToListAsync();

                    // Crea una lista de objetos CarritoDetalleDto
                    var listaCarritoDetalleDto = new List<CarritoDetalleDto>();

                    // Recorre cada registro de la tabla CarritoSesionDetalle correspondiente al carrito de sesión actual
                    foreach (var libro in carritoSesionDetalle)
                    {
                        //Invocamos al Microservicio Externo
                        var response = await librosService.GetLibro(new System.Guid(libro.ProductoSeleccionado));

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

                            listaCarritoDetalleDto.Add(carritoDetalle);
                        }
                    }

                    //Llenamos el Objeto que Realmente es Necesario Retomar
                    var carritoDto = new CarritoDto
                    {
                        CarritoId = carritoSesion.CarritoSesionId,
                        FechaCreacionSesion = carritoSesion.FechaCreacion,
                        UserName = carritoSesion.UserName,
                        ListaDeProductos = listaCarritoDetalleDto
                    };

                    listaCarritosDto.Add(carritoDto);
                }

                return listaCarritosDto;
            }
        }
    }


}

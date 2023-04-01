using MediatR;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.API.CarritoCompras.Modelo;
using TiendaServicios.API.CarritoCompras.Persistencia;

namespace TiendaServicios.API.CarritoCompras.Aplicacion
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public DateTime FechaCreacionSesion { get; set; }
            public string UserName { get; set; }
            public List<string> ProductoLista { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly CarritoContexto _context;

            public Manejador(CarritoContexto context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var carritoSesion = new CarritoSesion
                {
                    FechaCreacion = request.FechaCreacionSesion,
                    UserName = request.UserName
                };

                _context.CarritoSesion.Add(carritoSesion);

                var result = await _context.SaveChangesAsync();

                if (result == 0)
                {
                    throw new Exception("No se Pudo Insertar en el Carrito de Compras");
                }

                int id = carritoSesion.CarritoSesionId;

                //Insertamos Todos los Elementos de la Lista del Carrito de Compras
                foreach (var p in request.ProductoLista)
                {
                    var detalleSesion = new CarritoSesionDetalle
                    {
                        FechaCreacion = DateTime.Now,
                        CarritoSesionId = id,
                        ProductoSeleccionado = p
                    };

                    _context.CarritoSesionDetalle.Add(detalleSesion);
                }

                var value = await _context.SaveChangesAsync();

                if (value > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("No se Pudo Insertar el Detalle del Carrito de Compras");
            }
        }
    }

}

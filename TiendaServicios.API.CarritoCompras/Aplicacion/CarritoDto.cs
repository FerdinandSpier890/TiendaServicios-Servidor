using System;
using System.Collections.Generic;

namespace TiendaServicios.API.CarritoCompras.Aplicacion
{
    public class CarritoDto
    {
        public int CarritoId { get; set; }

        public DateTime? FechaCreacionSesion { get; set; }

        public string UserName { get; set; }

        public List<CarritoDetalleDto> ListaDeProductos { get; set; }
    }
}

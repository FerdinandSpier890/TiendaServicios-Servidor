using System;

namespace TiendaServicios.API.CarritoCompras.Aplicacion
{
    public class CarritoDetalleDto
    {
        public Guid? LibroId { get; set; }

        public string TituloLibro { get; set; }

        public string AutorLibro { get; set; }

        public double Precio { get; set; }

        public DateTime FechaPublicacion { get; set; }
    }
}

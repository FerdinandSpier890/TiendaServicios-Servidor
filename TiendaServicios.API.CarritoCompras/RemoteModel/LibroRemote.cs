using System;

namespace TiendaServicios.API.CarritoCompras.RemoteModel
{
    public class LibroRemote
    {
        public Guid LibreriaMaterialId { get; set; }

        public string Titulo { get; set; }

        public DateTime FechaPublicacion { get; set; }

        public double Precio { get; set; }

        public Guid? AutorLibro { get; set; }
    }
}

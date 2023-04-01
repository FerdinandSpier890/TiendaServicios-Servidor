using System;
using System.ComponentModel.DataAnnotations;

namespace TiendaServicios.API.Libro.Modelo
{
    public class LibreriaMaterial
    {
        [Key] // Llave Primaria de la Tabla
        public Guid? LibreriaMateriaId { get; set; }

        public string Titulo { get; set; }

        public DateTime? FechaPublicacion { get; set; }
        
        public double Precio { get; set; }

        public Guid? AutorLibro { get; set; }
    }
}

﻿using System;

namespace TiendaServicios.API.Libro.Aplicacion
{
    public class LibroMaterialDto
    {
        public Guid? LibreriaMateriaId { get; set; }

        public string Titulo { get; set; }

        public DateTime? FechaPublicacion { get; set; }

        public double Precio { get; set; }

        public Guid? AutorLibro { get; set; }
    }
}

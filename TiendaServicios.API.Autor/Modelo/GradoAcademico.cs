using System;

namespace TiendaServicios.API.Autor.Modelo
{
    public class GradoAcademico
    {
        public int GradoAcademicoId { get; set; }

        public string Nombre { get; set; }

        public string ControlAcademico { get; set; }

        public DateTime? FechaGrado { get; set; }

        public int AutorLibroInt { get; set; }

        public AutorLibro AutorLibro { get; set; }

        public string GradoAcademicoGuid { get; set; }

    }
}

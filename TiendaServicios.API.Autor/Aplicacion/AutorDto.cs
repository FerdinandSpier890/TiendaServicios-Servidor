using System.Collections.Generic;
using System;
using TiendaServicios.API.Autor.Modelo;

namespace TiendaServicios.API.Autor.Aplicacion
{
    /// <summary>
    /// Entidad del Tipo AutorDto
    /// </summary>
    public class AutorDto
    {
        public int AutorLibroId { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public DateTime? FechaNacimiento { get; set; }

        //Seguimiento de Registro para el Microservicio
        public string AutorLibroGuid { get; set; }
    }
}

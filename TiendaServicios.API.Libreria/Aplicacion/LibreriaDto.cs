using System;

namespace TiendaServicios.API.Libreria.Aplicacion
{
    public class LibreriaDto
    {
        public string LibreriaId { get; set; }

        public string Nombre { get; set; }

        public string CorreoContacto { get; set; }

        public string Direccion { get; set; }

        public string Telefono { get; set; }

        //Seguimiento de Registro para el Microservicio
        public string LibreriaGuid { get; set; }
    }
}

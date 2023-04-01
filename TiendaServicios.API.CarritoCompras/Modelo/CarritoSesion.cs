using System;
using System.Collections;
using System.Collections.Generic;

namespace TiendaServicios.API.CarritoCompras.Modelo
{
    public class CarritoSesion
    {
        public int CarritoSesionId { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public string UserName { get; set; }

        public ICollection<CarritoSesionDetalle> ListaDetalle { get; set; }

        
    }
}

using Microsoft.EntityFrameworkCore;
using TiendaServicios.API.CarritoCompras.Modelo;

namespace TiendaServicios.API.CarritoCompras.Persistencia
{
    public class CarritoContexto : DbContext
    {
        public CarritoContexto(DbContextOptions<CarritoContexto> options) : base(options) 
        { 

        }

        public DbSet<CarritoSesion> CarritoSesion { get; set; }

        public DbSet<CarritoSesionDetalle> CarritoSesionDetalle { get; set; }
    }
}

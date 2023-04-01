using Microsoft.EntityFrameworkCore;
using TiendaServicios.API.Libro.Modelo;

namespace TiendaServicios.API.Libro.Persistencia
{
    public class ContextoLibreria : DbContext
    {
        public ContextoLibreria(DbContextOptions<ContextoLibreria> options) : base(options)
        {
        }
        
        public DbSet<LibreriaMaterial> LibreriaMaterial { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using TiendaServicios.API.Usuarios.Modelo;

namespace TiendaServicios.API.Usuarios.Persistencia
{
    public class ContextoUsuarios : DbContext
    {
        public ContextoUsuarios(DbContextOptions<ContextoUsuarios> options) : base(options)
        {

        }

        public DbSet<Users> Users { get; set; }
    }
}

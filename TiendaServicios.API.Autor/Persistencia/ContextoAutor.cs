using Microsoft.EntityFrameworkCore;
using TiendaServicios.API.Autor.Modelo;

namespace TiendaServicios.API.Autor.Persistencia
{
    public class ContextoAutor : DbContext
    {
        public ContextoAutor(DbContextOptions<ContextoAutor> options) : base(options)
        {
        }

        public DbSet<AutorLibro> AutorLibros { get; set; }

        public DbSet<GradoAcademico> GradoAcademico { get; set; }
    }
}

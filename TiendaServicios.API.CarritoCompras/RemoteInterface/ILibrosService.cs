using System;
using System.Threading.Tasks;
using TiendaServicios.API.CarritoCompras.RemoteModel;

namespace TiendaServicios.API.CarritoCompras.RemoteInterface
{
    public interface ILibrosService
    {
        Task<(bool resultado, LibroRemote Libro, string ErrorMessage)> GetLibro(Guid LibroId);
    }
}

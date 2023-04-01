using TiendaServicios.API.Usuarios.Modelo;

namespace TiendaServicios.API.Usuarios.Interface
{
    public interface ITokenServices
    {
        //Metodo para la Creacion de Token en la Implementación de la Interfaz
        string CreateToken(Users user);
    }
}

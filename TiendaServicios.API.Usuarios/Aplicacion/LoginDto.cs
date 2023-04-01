using System.ComponentModel.DataAnnotations;

namespace TiendaServicios.API.Usuarios.Aplicacion
{
    public class LoginDto
    {
        //Propiedad del Nombre de Usuario
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}

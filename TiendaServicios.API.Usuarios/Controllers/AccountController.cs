using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TiendaServicios.API.Usuarios.Aplicacion;
using TiendaServicios.API.Usuarios.Interface;
using TiendaServicios.API.Usuarios.Modelo;
using TiendaServicios.API.Usuarios.Persistencia;
using TiendaServicios.API.Usuarios.Services;

namespace TiendaServicios.API.Usuarios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ContextoUsuarios _context;
        private readonly ITokenServices _tokenService;

        public AccountController(ContextoUsuarios context, ITokenServices tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await UserExist(registerDto.UserName))
            {
                return BadRequest("El Nombre del Usuario ya se Encuentra Asignado");
            }
            //Encriptación de Contraseñas
            using var hmac = new HMACSHA512();
            var user = new Users
            {
                UserName = registerDto.UserName.ToLower(),

                //Se encripta la contraseña
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            //Retornamos el Usuario con el Token Creado
            return new UserDto
            {
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user)

            };
        }

        //Validación de Usuarios de la Persistencia de Datos
        private async Task<bool> UserExist(string userName)
        {
            return await _context.Users.AnyAsync(x => x.UserName == userName.ToLower());
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.UserName);
            if (user == null)
            {
                return Unauthorized("Usuario Invalido");
            }
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i])
                {
                    return Unauthorized("Password Invalido");
                }
            }
            return new UserDto
            {
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

    }
}

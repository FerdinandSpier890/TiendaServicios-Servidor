using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TiendaServicios.API.CarritoCompras.RemoteInterface;
using TiendaServicios.API.CarritoCompras.RemoteModel;

namespace TiendaServicios.API.CarritoCompras.RemoteServices
{
    public class LibrosService : ILibrosService
    {
        private readonly IHttpClientFactory httpClientFactory;
        
        private readonly ILogger<LibrosService> logger;

        public LibrosService(IHttpClientFactory httpClientFactory, ILogger<LibrosService> logger)
        {
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
        }

        public async Task<(bool resultado, LibroRemote Libro, string ErrorMessage)> GetLibro(Guid LibroId)
        {
            try
            {
                //Creamos Nuestro Objeto que se Comunicara con Nuestro Endpoint Libros
                var cliente = httpClientFactory.CreateClient("Libros");

                //Nos Comunicamos con Nuestro Endpoint que Estamos Solicitando
                var response = await cliente.GetAsync($"api/Libros/{LibroId}"); //Devuelve un JSON

                if (response.IsSuccessStatusCode)
                {
                    var contenido = await response.Content.ReadAsStringAsync(); //Leemos el Contenido de la Respuesta

                    //Especificamos que no hay Problema  por la Estructura del JSON como Venga, Mayusculas y Minusculas
                    var options = new JsonSerializerOptions()
                    {
                        PropertyNameCaseInsensitive = true,
                    };

                    var resultado = JsonSerializer.Deserialize<LibroRemote>(contenido, options);
                    return (true, resultado, null);
                }

                return (false, null, response.ReasonPhrase);

            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}

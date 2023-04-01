using FluentValidation;
using MediatR;
using System;
using System.Threading.Tasks;
using System.Threading;
using TiendaServicios.API.Libreria.Persistencia;
using TiendaServicios.API.Libreria.Modelo;
using FireSharp.Interfaces;
using FireSharp.Config;
using FireSharp;

namespace TiendaServicios.API.Libreria.Aplicacion
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public string LibreriaId { get; set; }

            public string Nombre { get; set; }

            public string CorreoContacto { get; set; }

            public string Direccion { get; set; }

            public string Telefono { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.LibreriaId).NotEmpty();
                RuleFor(x => x.Nombre).NotEmpty();
                RuleFor(x => x.CorreoContacto).NotEmpty();
                RuleFor(x => x.Direccion).NotEmpty();
                RuleFor(x => x.Telefono).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            IFirebaseClient firebaseClient;

            public Manejador()
            {
                IFirebaseConfig firebaseConfig = new FirebaseConfig
                {
                    AuthSecret = "hJ2mFcZtJC5I119Btpq1W4z9fjLCoJUjsSo3ibpr",
                    BasePath = "https://tiendaservicios-2e3f4-default-rtdb.firebaseio.com/"
                };

                firebaseClient = new FirebaseClient(firebaseConfig);
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var libreria = new Librerias
                {
                    LibreriaId = request.LibreriaId,
                    Nombre = request.Nombre,
                    CorreoContacto = request.CorreoContacto,
                    Direccion = request.Direccion,
                    Telefono = request.Telefono,
                    LibreriaGuid = Convert.ToString(Guid.NewGuid())
                };
                
                var response = await firebaseClient.SetAsync("Librerias/" + libreria.LibreriaGuid, new LibreriaDto { 
                    LibreriaId = libreria.LibreriaId, 
                    Nombre = libreria.Nombre, 
                    CorreoContacto = libreria.CorreoContacto, 
                    Direccion = libreria.Direccion, 
                    Telefono = libreria.Telefono, 
                    LibreriaGuid = libreria.LibreriaGuid });

                var valor = response.StatusCode;

                if (valor > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("No se Pudo Insertar el Libro");
            }
        }
    }
}

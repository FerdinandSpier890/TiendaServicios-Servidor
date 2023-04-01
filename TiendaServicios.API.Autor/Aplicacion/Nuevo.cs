using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.API.Autor.Modelo;
using TiendaServicios.API.Autor.Persistencia;

namespace TiendaServicios.API.Autor.Aplicacion
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public string Nombre { get; set; }

            public string Apellido { get; set; }

            public DateTime? FechaNacimiento { get; set; }
        }

        //Clase para Validar la Clase ejecuta a Traves del APIFluent Validator
        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                RuleFor(p => p.Nombre).NotEmpty(); //No Acepta Valores Nulos
                RuleFor(p => p.Apellido).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            public readonly ContextoAutor _contexto;

            public Manejador(ContextoAutor contexto)
            {
                _contexto = contexto;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                // Se Crea la Instancia del Autor-Libro Ligada al Contexto
                var autorLibro = new AutorLibro
                {
                    Nombre = request.Nombre,
                    Apellido = request.Apellido,
                    FechaNacimiento = request.FechaNacimiento,
                    AutorLibroGuid = Convert.ToString(Guid.NewGuid())
                };

                // Agregamos el Objeto del Tipo Autor-Libro
                _contexto.AutorLibros.Add(autorLibro);

                // Insertamos el Valor
                var respuesta = await _contexto.SaveChangesAsync();
                if(respuesta > 0)
                {
                    return Unit.Value; // Numero de Filas Afectadas
                }
                throw new Exception("No se pudo Insertar el Autor del Libro");
            }
        }
    }
}

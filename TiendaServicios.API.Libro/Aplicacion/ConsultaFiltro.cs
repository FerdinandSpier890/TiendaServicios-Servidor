using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.API.Libro.Modelo;
using TiendaServicios.API.Libro.Persistencia;

namespace TiendaServicios.API.Libro.Aplicacion
{
    public class ConsultaFiltro
    {
        public class LibroUnico : IRequest<LibroMaterialDto>
        {
            public Guid? LibreriaMateriaId { get; set; }

        }

        public class Manejador : IRequestHandler<LibroUnico, LibroMaterialDto>
        {
            private readonly ContextoLibreria _contexto;
            private readonly IMapper _mapper;

            public Manejador(ContextoLibreria contexto, IMapper mapper)
            {
                _contexto = contexto;
                _mapper = mapper;
            }

            public async Task<LibroMaterialDto> Handle(LibroUnico request, CancellationToken cancellationToken)
            {
                var libro = await _contexto.LibreriaMaterial.Where(x => x.LibreriaMateriaId == request.LibreriaMateriaId).FirstOrDefaultAsync();
                if (libro == null)
                {
                    throw new Exception("No se Encontró el Libro");
                }
                var libroDto = _mapper.Map<LibreriaMaterial, LibroMaterialDto>(libro);
                return libroDto;
            }
        }
    }
}

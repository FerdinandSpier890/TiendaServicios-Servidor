using AutoMapper;
using TiendaServicios.API.Autor.Modelo;

namespace TiendaServicios.API.Autor.Aplicacion
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AutorLibro, AutorDto>();
        }
    }
}

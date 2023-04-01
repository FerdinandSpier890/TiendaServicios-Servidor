using AutoMapper;
using TiendaServicios.API.Libreria.Modelo;

namespace TiendaServicios.API.Libreria.Aplicacion
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Librerias, LibreriaDto>();

        }
    }
}

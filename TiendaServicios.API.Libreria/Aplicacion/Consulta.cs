using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using TiendaServicios.API.Libreria.Modelo;
using TiendaServicios.API.Libreria.Persistencia;
using FireSharp.Interfaces;
using FireSharp.Config;
using FireSharp;
using System;

namespace TiendaServicios.API.Libreria.Aplicacion
{
    public class Consulta
    {
        public class ListaLibrerias : IRequest<List<LibreriaDto>>
        {
            public ListaLibrerias()
            {

            }
        }

        public class Manejador : IRequestHandler<ListaLibrerias, List<LibreriaDto>>
        {
            IFirebaseClient firebaseClient;
            private readonly IMapper _mapper;

            public Manejador(IMapper mapper)
            {
                IFirebaseConfig firebaseConfig = new FirebaseConfig
                {
                    AuthSecret = "hJ2mFcZtJC5I119Btpq1W4z9fjLCoJUjsSo3ibpr",
                    BasePath = "https://tiendaservicios-2e3f4-default-rtdb.firebaseio.com/"
                };

                firebaseClient = new FirebaseClient(firebaseConfig);
                _mapper = mapper;
            }

            
            public async Task<List<LibreriaDto>> Handle(ListaLibrerias request, CancellationToken cancellationToken)
            {
                var response = await firebaseClient.GetAsync("Librerias");
                if (response == null)
                {
                    // Handle record not found
                    throw new Exception("No hay Librerias Registradas");
                }
                var librerias = response.ResultAs<Dictionary<string, Librerias>>();

                var libreriasDto = _mapper.Map<List<LibreriaDto>>(librerias.Values);

                return libreriasDto;

            }
            
        }
    }
}

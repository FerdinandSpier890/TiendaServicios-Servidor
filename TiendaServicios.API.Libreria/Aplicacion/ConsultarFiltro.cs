using AutoMapper;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using TiendaServicios.API.Libreria.Modelo;
using System;

namespace TiendaServicios.API.Libreria.Aplicacion
{
    public class ConsultarFiltro
    {
        public class ConsultaPorId : IRequest<LibreriaDto>
        {
            public string LibreriaGuid { get; set; }
        }

        public class Manejador : IRequestHandler<ConsultaPorId, LibreriaDto>
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

            public async Task<LibreriaDto> Handle(ConsultaPorId request, CancellationToken cancellationToken)
            {
                var response = await firebaseClient.GetAsync($"Librerias/{request.LibreriaGuid}");
                if (response == null)
                {
                    // Handle record not found
                    throw new Exception($"La Libreria con Guid {request.LibreriaGuid} No se encontró");
                }
                var libreria = response.ResultAs<Librerias>();
                return _mapper.Map<LibreriaDto>(libreria);
            }
        }

    }
}

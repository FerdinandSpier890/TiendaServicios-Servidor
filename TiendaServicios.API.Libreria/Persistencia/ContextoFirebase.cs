using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TiendaServicios.API.Libreria.Modelo;

namespace TiendaServicios.API.Libreria.Persistencia
{
    public class ContextoFirebase
    {
        private readonly IFirebaseClient client;

        public ContextoFirebase(string firebaseUrl, string firebaseSecret)
        {
            var config = new FirebaseConfig
            {
                BasePath = firebaseUrl,
                AuthSecret = firebaseSecret
            };

            client = new FireSharp.FirebaseClient(config);

        }

        public async Task<FirebaseResponse> SetData(string path, object data)
        {
            return await client.SetAsync(path, data);
        }

        public async Task<FirebaseResponse> UpdateData(string path, object data)
        {
            return await client.UpdateAsync(path, data);
        }

        public async Task<FirebaseResponse> DeleteData(string path)
        {
            return await client.DeleteAsync(path);
        }

        public async Task<FirebaseResponse> GetData(string path)
        {
            return await client.GetAsync(path);
        }
    }

}

using MongoDB.Driver;
using TiendaServicios.API.Mongo.Entities;

namespace TiendaServicios.API.Mongo.Data
{
    public interface ICatalogContext
    {
        IMongoCollection<Product> Products { get; }
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using TiendaServicios.API.Mongo.Entities;

namespace TiendaServicios.API.Mongo.Repositories
{
    public interface IProductRepository
    {
        Task CreateProduct(Product product);

        Task<bool> DeleteProduct(string id);

        Task<Product> GetProduct(string id);

        Task<IEnumerable<Product>> GetProductsByCategory(string categoryName);

        Task<IEnumerable<Product>> GetProductByName(string name);

        Task<IEnumerable<Product>> GetProducts();

        Task<bool> UpdateProduct(Product product);
    }
}

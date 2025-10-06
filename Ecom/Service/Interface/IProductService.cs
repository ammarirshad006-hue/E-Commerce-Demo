using Ecom.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecom.Service.Interface
{
    public interface IProductService
    {  
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product?> GetProductByIdAsync(int id);
        Task<Product> AddProductAsync(Product product);
        Task<Product> UpdateProductAsync(Product product);
        Task<IEnumerable<Product>> GetLowStockProductsAsync(int threshold);

    }
}

using Ecom.Models;

namespace Ecom.Repository.Interface
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();

        Task<Product?> GetById(int id);

        Task<Product> AddAsync(Product product);

        Task<Product> UpdateAsync(Product product);

        Task<IEnumerable<Product>> GetLowStockAsync(int threshold);

    }
}

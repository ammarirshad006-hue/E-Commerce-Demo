using Ecom.Models;

namespace Ecom.Repository.Interface
{
    public interface IStockRepositery
    {
        Task<IEnumerable<Stock>> GetAllAsync();
        Task<Stock> GetById(int id);

        Task<Stock> AddAsync(Stock stock);

        Task<Stock> UpdateAsync(Stock stock);

        Task<Stock> DeleteAsync(int id);
    }
}

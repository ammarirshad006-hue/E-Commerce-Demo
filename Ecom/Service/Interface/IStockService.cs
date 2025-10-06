using Ecom.Models;

namespace Ecom.Service.Interface
{
    public interface IStockService
    {
        Task<IEnumerable<Stock>> GetAllStockAsync();

        Task<Stock> GetStockByIdAsync(int id);


        Task<Stock> AddStockAsync(Stock stock);

        Task<Stock> UpdateStockAsync(Stock stock);

        Task<Stock> DeleteStockAsync(int id);
       
    
    }
}

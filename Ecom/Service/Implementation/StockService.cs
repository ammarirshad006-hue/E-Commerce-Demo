using Ecom.Models;
using Ecom.Repository.Interface;
using Ecom.Service.Interface;

namespace Ecom.Service.Implementation
{
    public class StockService : IStockService
    {
        private readonly IStockRepositery _stockRepository;

        public StockService(IStockRepositery stockRepository) // ✅ inject repository
        {
            _stockRepository = stockRepository;
        }

        public async Task<Stock> AddStockAsync(Stock stock)
        {
            return await _stockRepository.AddAsync(stock);
        }

        public async Task<Stock> DeleteStockAsync(int id)
        {
            return await _stockRepository.DeleteAsync(id);
        }

        public Task<IEnumerable<Stock>> GetAllStockAsync()
        {
            return _stockRepository.GetAllAsync();
        }

        public Task<Stock> GetStockByIdAsync(int id)
        {
            return _stockRepository.GetById(id);
        }

        public async Task<Stock> UpdateStockAsync(Stock stock)
        {
            return await _stockRepository.UpdateAsync(stock);
        }
    }
}

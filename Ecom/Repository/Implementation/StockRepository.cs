using Ecom.Data;
using Ecom.Models;
using Ecom.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Ecom.Repository.Implementation
{
    public class StockRepository : IStockRepositery
    {
        private readonly MyDbContext _context;
        public StockRepository(MyDbContext context)
        {
            _context = context;
        }



        public async Task<Stock> AddAsync(Stock stock)
        {
            await _context.AddAsync(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<Stock> DeleteAsync(int id)
        {


            var stock = await _context.Stocks.FindAsync(id);
            if (stock == null)
            {
                return null;
            }

            _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync();

            return stock;
        }

        

        public async Task<IEnumerable<Stock>> GetAllAsync()
        {
            return await _context.Stocks
                              .Include(s => s.Product)
                              .ToListAsync();
        }

        public async Task<Stock> GetById(int id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _context.Stocks
                              .Include(s => s.Product)
                              .FirstOrDefaultAsync(s => s.Id ==id);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task<Stock> UpdateAsync(Stock stock)
        {
            _context.Stocks.Update(stock);
            await _context.SaveChangesAsync();
            return stock;
        }
        
    }
    
}

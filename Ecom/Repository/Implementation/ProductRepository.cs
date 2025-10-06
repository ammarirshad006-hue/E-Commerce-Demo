using Ecom.Controllers;
using Ecom.Data;
using Ecom.Models;
using Ecom.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Ecom.Repository.Implementation
{
    public class ProductRepository : IProductRepository
    {
        private readonly MyDbContext _context;
      

        public ProductRepository(MyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products
                                 .Include(p => p.Stock)
                                 .ToListAsync();
        }

        public async Task<Product?> GetById(int id)
        {
            return await _context.Products
                                 .Include(p => p.Stock)
                                 .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product> AddAsync(Product product)
        {
            try
            {
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
                return product;
            }
            catch (Exception)
            {
                throw; // preserves stack trace
            }
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            
            _context.Products.Update(product);  
            await _context.SaveChangesAsync();  
            return product;
        }

        public async Task<IEnumerable<Product>> GetLowStockAsync(int threshold)
        {
          var product = await _context.Products
                                   .Include(p => p.Stock)
                                   .ToListAsync();
            var lowStockProducts = product
                        .Where(p => p.Stock.Sum(s => s.Quantity) < threshold)
                        .ToList();

            return lowStockProducts;
        }

    }

}


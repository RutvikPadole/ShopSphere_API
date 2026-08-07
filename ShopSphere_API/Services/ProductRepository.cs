using Microsoft.EntityFrameworkCore;
using ShopSphere_API.Data;
using ShopSphere_API.Interfaces;
using ShopSphere_API.Entities;

namespace ShopSphere_API.Services
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task <Product> GetByIDAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task AddAsync (Product product)
        {
             await _context.Products.AddAsync(product);
        }

        public async Task DeleteAsync(Product product)
        {
            return await _context.Products.RemoveAsync(product);
        }

        public async Task SaveAsync()
        {
            return await _context.Products.SaveChangesAsync();
        }
    }


}

using Microsoft.EntityFrameworkCore;
using ShopSphere_API.Data;
using ShopSphere_API.Entities;
using ShopSphere_API.Interfaces;

namespace ShopSphere_API.Repositories
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

        public async Task<Product?> GetByIdAsync(int id)

        {
            return await _context.Products.FindAsync(id);
        }

        public async Task AddAsync(Product product)

        {
            await _context.Products.AddAsync(product);
        }

        public void Update(Product product)

        {
            _context.Products.Update(product);
        }

        public void Delete(Product product)

        {
            _context.Products.Remove(product);
        }

        public async Task SaveAsync()

        {
            await _context.SaveChangesAsync();
        }
    }
}
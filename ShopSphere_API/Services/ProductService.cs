using ShopSphere_API.DTOs;
using ShopSphere_API.Interfaces;

namespace ShopSphere_API.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;
        public ProductService(IProductRepository repo)
        {
            _repo = repo;
        }
        public async Task <IEnumerable<ProductDto>> GetAllProducts()
        {
            var products = await _repo.GetAllAsync();
            return products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
            });
        }
    }
}

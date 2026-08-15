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
        public async Task<IEnumerable<ProductDto>> GetAllProducts()
        {
            var products = await _repo.GetAllAsync();
            return products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
            });
        }

        public async Task<ProductDto> GetProductById(int id)
        {
            var p = await _repo.GetByIdAsync(id);

            if (p == null) return null;

            return new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price
            };

        }
      

        public async Task CreateProduc(CreateProductDto dto)
        {
            var product = new Product
            {
                Name = dto.Name,
                Price = dto.Price,
                Description = dto.Description,
                CAtegoryId = dto.CategoryId
            };

            await _repo.AddAsync(Product);
            await _repo.SaveAsync();

        }
    }
}

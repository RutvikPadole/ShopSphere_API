using ShopSphere_API.DTOs;
using ShopSphere_API.Entities;
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
                Price = p.Price
            });
        }

        public async Task<ProductDto?> GetProductById(int id)
        {
            var product = await _repo.GetByIdAsync(id);

            if (product == null)
                return null;

            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            };
        }

        public async Task CreateProduct(CreateProductDto dto)
        {
            var product = new Product
            {
                Name = dto.Name,
                Price = dto.Price,
                Description = dto.Description,
                CategoryId = dto.CategoryId
            };

            await _repo.AddAsync(product);

            await _repo.SaveAsync();
        }

        public async Task<bool> UpdateProduct(
            int id,
            CreateProductDto dto)
        {
            var product = await _repo.GetByIdAsync(id);

            if (product == null)
                return false;

            product.Name = dto.Name;
            product.Price = dto.Price;
            product.Description = dto.Description;
            product.CategoryId = dto.CategoryId;

            _repo.Update(product);

            await _repo.SaveAsync();

            return true;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var product = await _repo.GetByIdAsync(id);

            if (product == null)
                return false;

            _repo.Delete(product);

            await _repo.SaveAsync();

            return true;
        }
    }
}
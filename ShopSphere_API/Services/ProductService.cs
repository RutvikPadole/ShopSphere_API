using AutoMapper;
using ShopSphere_API.DTOs;
using ShopSphere_API.Entities;
using ShopSphere_API.Interfaces;

namespace ShopSphere_API.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repo, IMapper mapper)
        {
            _repo = repo;
           _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProducts()
        {
            var products = await _repo.GetAllAsync();

            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<ProductDto?> GetProductById(int id)
        {
            var product = await _repo.GetByIdAsync(id);

            if (product == null)
                return null;

          return _mapper.Map<ProductDto>(product);
        }

        public async Task CreateProduct(CreateProductDto dto)
        {
            var product = _mapper.Map<Product>(dto);
            
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
using ShopSphere_API.DTOs;

namespace ShopSphere_API.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProducts();

        Task<ProductDto?> GetProductById(int id);

        Task CreateProduct(CreateProductDto dto);

        Task<bool> UpdateProduct(int id, CreateProductDto dto);

        Task<bool> DeleteProduct(int id);
    }
}
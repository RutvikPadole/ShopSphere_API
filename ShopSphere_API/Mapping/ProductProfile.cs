
using ShopSphere_API.DTOs;
using ShopSphere_API.Entities;


namespace ShopSphere_API.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>();

            CreateMap<CreateProductDto, Product>();
        }
       
    }
}

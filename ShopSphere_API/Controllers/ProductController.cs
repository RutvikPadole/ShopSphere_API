using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopSphere_API.DTOs;
using ShopSphere_API.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace ShopSphere_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;
        private readonly IValidator<CreateProductDto> _validator;

        public ProductController(IProductService service, IValidator<CreateProductDto> validator)
        {
            _service = service;
            _validator = validator;

        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _service.GetAllProducts();

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _service.GetProductById(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto dto)
        {
            var validationResult = await _validator.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            await _service.CreateProduct(dto);

            return Ok("Created");

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(
            int id,
            CreateProductDto dto)
        {
            var result = await _service.UpdateProduct(id, dto);

            if (!result)
                return NotFound();

            return Ok("Product updated successfully");

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _service.DeleteProduct(id);

            if (!result)
                return NotFound();

            return Ok("Product deleted successfully");
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using ShopSphere_API.Data;
using ShopSphere_API.Entities;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Authorization;
using ShopSphere_API.DTOs;
using ShopSphere_API.Interfaces;

[Route("api/[Controller]")]
[ApiController]

public class ProductController : ControllerBase
{
    private readonly IProductService _service;
    public ProductController(IProductService service)
    {
        _service = service;
    }

    [Authorize]
    [HttpGet]

    public async Task<IActionResult> GetProducts()
    {
        var products = await _service.GetAllProducts();

        return Ok(products);
    }

    [HttpGet("{id}")]

    public async Task <IActionResult> GetProduct (int id)
    {
        var products = await _service.GetProductById(id);

        if (products == null)
            return NotFound();

        return Ok(products);

    }

    [HttpPost]

    public async Task<IActionResult> CreateProducts(CreateProductDto dto)
    {
        var product = new Product
        {
            Name = dto.Name,
            Price = dto.Price,
            Description = dto.Description,
            CategoryId = dto.CategoryId
        };

        await _service.CreateProduct(dto);

        return Ok("Created");
    }

    [HttpPut("{id}")]

    public async Task<IActionResult> UpdateProduct(int id, Product product)
    {
        if (id != product.Id)
            return BadRequest();

        _service.Entry(product).State = EntityState.Modified;
        await _service.SaveChangesAsync();

        return Ok(product);

    }

    [HttpDelete("{id}")]

    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null)
            return NotFound();

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return Ok("Deleted Successfully");
    }
}

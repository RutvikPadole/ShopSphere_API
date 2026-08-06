using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using ShopSphere_API.Data;
using ShopSphere_API.Entities;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Authorization;

[Route("api/[Controller]")]
[ApiController]

public class ProductController : ControllerBase
{
    private readonly AppDbContext _context;
    public ProductController(AppDbContext context)
    {
        _context = context;
    }
    [Authorize]
    [HttpGet]

    public async Task<IActionResult> GetProducts()
    {
        var products = await _context.Products.ToListAsync();

        return Ok(products);
    }

    [HttpGet("{id}")]

    public async Task <IActionResult> GetProduct (int id)
    {
        var products = await _context.Products.FindAsync(id);

        if (products == null)
            return NotFound();

        return Ok(products);

    }

    [HttpPost]

    public async Task<IActionResult> CreateProducts(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return Ok(product);

    }

    [HttpPut("{id}")]

    public async Task<IActionResult> UpdateProduct(int id, Product product)
    {
        if (id != product.Id)
            return BadRequest();

        _context.Entry(product).State = EntityState.Modified;
        await _context.SaveChangesAsync();

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

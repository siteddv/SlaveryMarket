using Microsoft.AspNetCore.Mvc;
using SlaveryMarket.Data.Entity;
using SlaveryMarket.Data.Repository;
using SlaveryMarket.Dtos;

namespace SlaveryMarket.Controllers;

public class ProductController : BaseController
{
    private readonly ProductRepository _repository;

    public ProductController(ProductRepository repository)
    {
        _repository = repository;
    }

    [HttpPost("add-product")]
    public IActionResult AddProduct([FromBody] ProductDto product)
    {
        Product newProduct = new Product
        {
            Name = product.Name,
            Description = product.Description,
            Price = product.Price
        };
        
        var addedProduct =  _repository.Add(newProduct);
        return Ok(addedProduct);
    }
    
    [HttpGet("get-products")]
    public IActionResult GetProducts()
    {
        var products = _repository.GetAll();
        if(products.Count == 0)
        {
            return NotFound();
        }
        return Ok(products);
    }
}
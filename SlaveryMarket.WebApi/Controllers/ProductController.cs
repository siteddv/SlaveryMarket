using Microsoft.AspNetCore.Mvc;
using SlaveryMarket.BL.Dtos;
using SlaveryMarket.BL.Services;
using SlaveryMarket.Data.Entity;
using SlaveryMarket.Data.Repository;

namespace SlaveryMarket.Controllers;

public class ProductController(ProductService service) : BaseController
{
    [HttpPost("add-product")]
    public IActionResult AddProduct([FromBody] ProductDto product)
    {
        var addedProduct = service.AddProduct(product);
        return Ok(addedProduct);
    }
    
    [HttpGet("get-products")]
    public IActionResult GetProducts()
    {
        var products = service.GetProducts();
        if(products.Count == 0)
            return NotFound();
        
        return Ok(products);
    }
}
using SlaveryMarket.BL.Dtos;
using SlaveryMarket.Data.Entity;
using SlaveryMarket.Data.Repository;

namespace SlaveryMarket.BL.Services;

public class ProductService(ProductRepository productRepository, 
    SimpleRepository<ProductCategory> productCategoryRepository)
{
    public Product AddProduct(ProductDto product)
    {
        var newProduct = new Product
        {
            Name = product.Name,
            Description = product.Description,
            Price = product.Price
        };
        
        var addedProduct = productRepository.Add(newProduct);
        
        productCategoryRepository.Add(new ProductCategory
        {
            ProductId = addedProduct.Id,
            CategoryId = product.CategoryId
        });
        
        productRepository.SaveChanges();
        
        return addedProduct;
    }
    
    public List<Product> GetProducts()
    {
        return productRepository.GetAll();
    }
}
using Microsoft.AspNetCore.Mvc;
using ProductModels;
using ProductRepository.Implementation;
using ProductRepository.Interface;
using ProductService.Interface;

namespace ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductDetailsService _productService;

        public ProductController(IProductDetailsService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _productService.GetAllProductAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            if (product == null)
            {
                return BadRequest("Product is null.");
            }
                var createdProduct = await _productService.AddProductWithStockTransactionAsync(product);
                return CreatedAtAction(nameof(GetProduct), new { id = createdProduct.Id }, createdProduct);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }
            await _productService.UpdateProductAsync(product);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteProductAsync(id);
            return NoContent();
        }

        // PUT: api/products/decrement-stock/{id}/{amount}
        [HttpPut("decrement-stock/{id}/{amount}")]
        public async Task<IActionResult> DecrementStock(int id, int amount)
        {
            var product = await _productService.DecrementStockAsync(id, amount);
            return Ok(product);
        }
        // PUT: api/products/decrement-stock/{id}/{amount}
        [HttpPut("add-to-stock/{id}/{amount}")]
        public async Task<IActionResult> AddToStock(int id, int amount)
        {
            var product = await _productService.IncrementStockAsync(id, amount);

            return Ok(product);
        }
    }
}

using ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Interface
{
    public interface IProductDetailsService
    {
        Task<IEnumerable<Product>> GetAllProductAsync();
        Task<Product> AddProductAsync(Product product);
        Task<Product> GetProductByIdAsync(int id);
        Task<Product> UpdateProductAsync(Product product);
        Task<Product> DeleteProductAsync(int id);
        Task<Product> DecrementStockAsync(int productId, int amount);
        Task<Product> IncrementStockAsync(int productId, int amount);
        Task<Product> AddProductWithStockTransactionAsync(Product product);
    }
}

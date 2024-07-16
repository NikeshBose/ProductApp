using ProductModels;
using ProductRepository.Interface;
using ProductService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Implementation
{
    public class ProductDetailsService : IProductDetailsService
    {
        private readonly IProductRepository _productRepository;
        private readonly IStockTransactionRepository _stockTransactionRepository;

        public ProductDetailsService(IProductRepository productRepository, IStockTransactionRepository stockTransactionRepository)
        {
            _productRepository = productRepository;
            _stockTransactionRepository = stockTransactionRepository;
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            await _productRepository.Add(product);
            return product;
        }

        public async Task<Product> DecrementStockAsync(int productId, int amount)
        {
            var product = await _productRepository.GetById(productId);
            if (product == null)
            {
                throw new Exception("Product not found.");
            }

            if (amount % product.Price != 0)
            {
                throw new Exception("Amount must be a multiple of the product price.");
            }

            var quantityToDecrement = amount / (int)product.Price;

            if (product.Stock < quantityToDecrement)
            {
                throw new Exception("Insufficient stock.");
            }

            product.Stock -= quantityToDecrement;
            await _productRepository.Update(product);

            var stockTransaction = new StockTransaction
            {
                ProductID = product.Id,
                TransactionType = "Decrement",
                QuantityChange = quantityToDecrement,
            };

            await _stockTransactionRepository.Add(stockTransaction);

            return product;

        }

        public async Task<Product> DeleteProductAsync(int id)
        {
            var product = await _productRepository.GetById(id);
            if (product == null)
            {
                throw new Exception("Product not found.");
            }

            await _productRepository.Delete(id);
            return product;
        }

        public async Task<IEnumerable<Product>> GetAllProductAsync()
        {
            return await _productRepository.GetAll();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _productRepository.GetById(id);
        }

        public async Task<Product> IncrementStockAsync(int productId, int amount)
        {
            var product = await _productRepository.GetById(productId);
            if (product == null)
            {
                throw new Exception("Product not found.");
            }

            if (amount % product.Price != 0)
            {
                throw new Exception("Amount must be a multiple of the product unit price.");
            }

            int unitsToIncrement = amount / (int)product.Price;


            product.Stock += unitsToIncrement;
            await _productRepository.Update(product);

            var stockTransaction = new StockTransaction
            {
                ProductID = product.Id,
                TransactionType = "Increment",
                QuantityChange = unitsToIncrement,
            };

            await _stockTransactionRepository.Add(stockTransaction);
            return product;
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            await _productRepository.Update(product);
            return product;
        }
        public async Task<Product> AddProductWithStockTransactionAsync(Product product)
        {
            await _productRepository.BeginTransaction(); // Ensure your repository supports transactions

            try
            {
                // Add the product
                var addedProduct = await _productRepository.Add(product);

                // Create a stock transaction for initial stock
                var stockTransaction = new StockTransaction
                {
                    ProductID = addedProduct.Id,
                    TransactionType = "Initial Stock",
                    QuantityChange = addedProduct.Stock,

                };

                await _stockTransactionRepository.Add(stockTransaction);

                await _productRepository.CommitTransaction(); // Commit the transaction if everything succeeds

                return addedProduct;
            }
            catch (Exception)
            {
                await _productRepository.RollBackTransaction(); // Rollback the transaction if there's an exception
                throw; // Re-throw the exception to propagate it up
            }
        }
    }
        }
   


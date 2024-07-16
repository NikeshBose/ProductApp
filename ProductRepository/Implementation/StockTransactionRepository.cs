using ProductModels;
using ProductRepository.Context;
using ProductRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductRepository.Implementation
{
    public class StockTransactionRepository : GenericRepository<StockTransaction>, IStockTransactionRepository
    {
        public StockTransactionRepository(ProductContext context) : base(context)
        {
        }
    }
}

using Microsoft.EntityFrameworkCore;
using ProductCommon;
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
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly ProductContext _context;
       // private readonly RedisIdGenerator _idGenerator;       //Incase we go with Redis

        public ProductRepository(ProductContext context) : base(context)
        {
            _context = context;
        }

        //Incase we go with Redis

        //public ProductRepository(ProductContext context, RedisIdGenerator idGenerator) : base(context)
        //{
        //    _context = context;
        //    _idGenerator = idGenerator;                      
        //}
    }
}


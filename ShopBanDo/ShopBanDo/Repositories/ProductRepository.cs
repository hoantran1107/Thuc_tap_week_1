using ShopBanDo.Interface;
using ShopBanDo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBanDo.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        // ham khoi tao lay tu lop ch la GenericRepository<Product>
        public ProductRepository(dbshopContext context) : base(context)
        {
        }

        public IEnumerable<Product> GetActiveProducts()
        {
            return _context.Products.Where(x => x.Active == true).OrderByDescending(x => x.DateCreated).ToList();
        }
    }
}

using Microsoft.EntityFrameworkCore;
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
            return _context.Products.Where(x => x.Active == true && x.UnitslnStock > 0).OrderByDescending(x => x.DateCreated);
        }
        public IEnumerable<Product> FindId(int id)
        {
            return _context.Products.Where(x => x.CatId==id && x.Active == true && x.UnitslnStock >0).OrderByDescending(x => x.DateCreated);
        }
        //public IEnumerable<Product> FindProducts()
        //{
        //    return _context.Products.Where(x => x.Active == true).OrderByDescending(x => x.DateCreated);
        //}
        public IEnumerable<Product> FindProduct(string key)
        {
            if(key != null)
                return _context.Products.Where(x => x.ProductName!.Contains(key) || x.Alias!.Contains(key));
            return _context.Products.Where(x => x.Active == true).OrderByDescending(x => x.DateCreated);
        }
        public int GetProductStock(int id)
        {
            var Stock = _context.Products.AsNoTracking().Where(x => x.ProductId == id).FirstOrDefault().UnitslnStock;
            if (Stock == null) throw new Exception("No Stock in Product");
            else
            return (int)Stock;
        }
    }
}

using ShopBanDo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBanDo.Interface
{
    public interface IProductRepository :IGenericRepository<Product>
    {
        //ke thua tu interface IGenericRepository<T>
        //Them 1 phuong thuc GetActiveProducts, tra ve  IEnumerable<Product>
        IEnumerable<Product> GetActiveProducts();
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using ShopBanDo.Models;
using ShopBanDo.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBanDo.Controllers
{
    public class ProductController : Controller
    {
        //khai báo db dùng
        private readonly dbshopContext _context;
        private ProductRepository _product;
        //khởi tạo cho class ProductController với tham số là dbContext
        public ProductController(dbshopContext context)
        {
            _context = context;
            _product = new ProductRepository(context);
        }
        [Route("shop-product", Name = ("ShopProduct"))]
        public IActionResult Index(int? page)
        {
            
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 12;
            //ASNoTracking dont create snapshot when save to database
            //better performent
            IEnumerable<Product> item;

            item = _product.GetActiveProducts();
            //IsCustomers IOrderQueryable <> ToList()
            //Make change in View
            PagedList<Product> models = new PagedList<Product>(item, pageNumber, pageSize);

            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.CurrentPage = pageNumber;
            ViewBag.Total = models.PageCount;
            ViewBag.TotalItem = item.Count();

            return View(models);  
            
        }
        [Route("/{Alias}-{id}", Name = ("ProductDetails"))]
        public IActionResult Detail(string Alias, int id)
        {
            
                
                var product = _context.Products.Include(x => x.Cat).FirstOrDefault(x => x.ProductId == id);
                if (product == null)
                {
                    return RedirectToAction("Index");
                }

                var lsproduct = _context.Products
                   .AsNoTracking()
                   .Where(x => x.CatId == product.CatId && x.Active == true && x.UnitslnStock >0 && x.ProductId != id)
                   .Take(4)
                   .OrderByDescending(x => x.DateCreated)
                   .ToList();
                ViewBag.SanPham = lsproduct;


                return View(product);
           
          
        }
        [Route("/List/{Alias}-{Catid}", Name = ("ListProduct"))]
        public IActionResult List(string Alias, int Catid, int page =1)
        {
           
            var pageSize = 12;
            var danhmuc = _context.Categories.Find(Catid);
            IEnumerable<Product> item;
            item = _product.FindId(Catid);
            //IsCustomers IOrderQueryable <> ToList()
            //Make change in View

            PagedList<Product> models = new PagedList<Product>(item, page, pageSize);

            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.CurrentPage = page;
            ViewBag.Total = models.PageCount;
            ViewBag.TotalItem = item.Count();
            ViewBag.Cat= danhmuc;
            ViewBag.First = models.FirstItemOnPage;
            ViewBag.Last = models.LastItemOnPage;


                return View(models);  
            
        }
        [Route("/search", Name = ("SearchProduct"))]
        public IActionResult Search(string searchString, int page = 1)
        {

            var pageSize = 12;

            IEnumerable<Product> item;

            item = _product.FindProduct(searchString);
            ViewBag.Key = searchString;

            PagedList<Product> models = new PagedList<Product>(item, page, pageSize);

            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.CurrentPage = page;
            ViewBag.Total = models.PageCount;
            ViewBag.TotalItem = item.Count();

            ViewBag.First = models.FirstItemOnPage;
            ViewBag.Last = models.LastItemOnPage;

            return View(models);


        }
    }
}

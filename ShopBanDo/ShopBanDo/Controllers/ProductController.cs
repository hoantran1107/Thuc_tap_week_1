using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using ShopBanDo.Models;
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
        //khởi tạo cho class ProductController với tham số là dbContext
        public ProductController(dbshopContext context)
        {
            _context = context;
        }
        [Route("shop-product.html", Name = ("ShopProduct"))]
        public IActionResult Index(int? page)
        {
            try
            {
                var pageNumber = page == null || page <= 0 ? 1 : page.Value;
                var pageSize = 12;
                //ASNoTracking dont create snapshot when save to database
                //better performent
                var lsProduct = _context.Products
                    .AsNoTracking()
                    .OrderByDescending(x => x.DateCreated);
                //IsCustomers IOrderQueryable <> ToList()
                //Make change in View
                PagedList<Product> models = new PagedList<Product>(lsProduct, pageNumber, pageSize);
                ViewBag.CurrentPage = pageNumber;
                ViewBag.Total = models.PageCount;
                ViewBag.TotalItem = lsProduct.Count();

                return View(models);
            }
            catch 
            {
                return RedirectToAction("Index", "Home");
            }
            
        }
        [Route("/{Alias}-{id}.html", Name = ("ProductDetails"))]
        public IActionResult Detail(string Alias, int id)
        {
            try
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
            catch
            {
                return RedirectToAction("Index", "Home");
            }
          
        }
        [Route("/List/{Alias}-{Catid}.html", Name = ("ListProduct"))]
        public IActionResult List(string Alias, int Catid, int page =1)
        {
            try
            {
                var pageSize = 12;
                var danhmuc = _context.Categories.Find(Catid);
                var lsProduct = _context.Products
                    .AsNoTracking()
                    .Where(x => x.CatId == Catid)
                    .OrderByDescending(x => x.DateCreated);
                //IsCustomers IOrderQueryable <> ToList()
                //Make change in View
                PagedList<Product> models = new PagedList<Product>(lsProduct, page, pageSize);
                ViewBag.CurrentPage = page;
                ViewBag.Total = models.PageCount;
                ViewBag.TotalItem = lsProduct.Count();
                ViewBag.Cat= danhmuc;
                ViewBag.First = models.FirstItemOnPage;
                ViewBag.Last = models.LastItemOnPage;


                return View(models);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
            
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using ShopBanDo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authentication;
using PagedList;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

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
                var Categories = _context.Categories;
                ViewBag.Categories = Categories.ToList();

                ViewBag.CurrentPage = pageNumber;
                ViewBag.Total = models.PageCount;
                ViewBag.TotalItem = lsProduct.Count();
                ViewBag.First = models.FirstItemOnPage;
                ViewBag.Last = models.LastItemOnPage;


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
        public IActionResult List( int Catid, int page =1)
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
                var Categories = _context.Categories;
                ViewBag.Categories = Categories.ToList();

                ViewBag.CurrentPage = page;
                ViewBag.Total = models.PageCount;
                ViewBag.TotalItem = lsProduct.Count();
               
                ViewBag.First = models.FirstItemOnPage;
                ViewBag.Last = models.LastItemOnPage;

                 ViewBag.Cat= danhmuc;


                return View(models);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
            
        }
       
        [Route("/search.html", Name = ("SearchProduct"))]
        
        public  IActionResult Search(string searchString ,int page =1)
        {  

            try
            {
                var pageSize = 12;

                var item = from mb in _context.Products.AsNoTracking()
                           select mb;

                if (!String.IsNullOrEmpty(searchString))
                {

                    HttpContext.Session.SetString("Key", searchString);
                    searchString = searchString.ToLower();
                    item = item.Where(s => s.ProductName!.Contains(searchString));

                }
                else
                {

                    string key = HttpContext.Session.GetString("Key");
                    if (key != null)
                    {
                        item = item.Where(s => s.ProductName!.Contains(key));
                    }


                }
                PagedList<Product> models = new PagedList<Product>(item, page, pageSize);

                    var Categories = _context.Categories;
                    ViewBag.Categories = Categories.ToList();

                    ViewBag.CurrentPage = page;
                    ViewBag.Total = models.PageCount;
                    ViewBag.TotalItem = item.Count();
                    ViewBag.First =models.FirstItemOnPage;
                    ViewBag.Last = models.LastItemOnPage;

                    ViewBag.Key = searchString;
                    return View(models);
 
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShopBanDo.Models;
using ShopBanDo.ModelView;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBanDo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly dbshopContext _context;
        public HomeController(ILogger<HomeController> logger,dbshopContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            //tao ra instance homeView gom List<ProductHomeVM>,List<TinTuc>,1 page quang cao
            //model chinh
            HomeViewVM model = new HomeViewVM();
            //lay ra san pham duoc xuat hien o trang home
            var lsProducts = _context.Products.AsNoTracking()
                .Where(x => x.Active == true && x.HomeFlag == true && x.UnitslnStock >0)
                .OrderByDescending(x => x.DateCreated)
                .ToList();
            //tao ra 1 list productHomeVM
            List<ProductHomeVM> lsProductViews = new List<ProductHomeVM>();
            //lay ra cac danh muc san pham
            var lsCats = _context.Categories
                .AsNoTracking()
                .Where(x => x.Published == true)
                .OrderByDescending(x => x.Ordering)
                .ToList();
            //duyet cac sanh mục
            foreach (var item in lsCats)
            {
                //ProductHomeVM co danh muc va cac san pham thuoc danh muc
                ProductHomeVM productHome = new ProductHomeVM();
                //moi cai productHome co thuoc tinh category la danh muc
                productHome.category = item;
                //1 list can pham thuc danh muc do
                productHome.lsProducts = lsProducts.Where(x => x.CatId == item.CatId).ToList();
                //dua cai producthome vao list
                lsProductViews.Add(productHome);
                //lay ra page quang cao
                var quangcao = _context.Pages
                    .AsNoTracking()
                    .FirstOrDefault(x => x.Published == true);
                //lay ra 3 tin tuc
                var TinTuc = _context.TinTucs
                    .AsNoTracking()
                    .Where(x => x.Published == true && x.IsNewfeed == true)
                    .OrderByDescending(x => x.CreatedDate)
                    .Take(3)
                    .ToList();
                //1 la list gom (list<product>,category cua cac san pham do
                //model.Products= {{ao,ao thun,ao dai tay}, men },{{quan,quan dui},girl}
                model.Products = lsProductViews;
                //lay ra 1 page quang cao
                model.Page = quangcao;
                //3 tin tuc o la isnewfeed
                model.TinTucs = TinTuc;
                //tat ca san pham duoc ghim o trang home
                ViewBag.AllProducts = lsProducts;
            }
            return View(model);
        }
        [Route("/contact", Name = "Contact")]
        public IActionResult Contact()
        {
            return View();
        }
        [Route("/about", Name = "About")]
        public IActionResult About()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

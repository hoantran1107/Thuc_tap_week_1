namespace WebShop.Controllers
{
    using AspNetCoreHero.ToastNotification.Abstractions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using ShopBanDo.Extension;
    using ShopBanDo.Helpper;
    using ShopBanDo.Models;
    using ShopBanDo.ModelView;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CheckoutController : Controller
    {
        private readonly dbshopContext _context;

        public INotyfService _notyfService { get; }

        private readonly ILogger<CheckoutController> _logger;

        public CheckoutController(dbshopContext context, INotyfService notyfService,ILogger<CheckoutController> logger)
        {
            _context = context;
            _notyfService = notyfService;
            _logger = logger;
        }

        public List<CartItem> GioHang
        {
            get
            {
                var gh = HttpContext.Session.Get<List<CartItem>>("GioHang");
                if (gh == default(List<CartItem>))
                {
                    gh = new List<CartItem>();
                }
                return gh;
            }
        }

        [Route("checkout.html", Name = "Checkout")]
        public IActionResult Index(string returnUrl = null)
        {
            var cart = HttpContext.Session.Get<List<CartItem>>("GioHang");
            var taikhoanID = HttpContext.Session.GetString("CustomerId");
            MuaHangVM model = new MuaHangVM();
            if (taikhoanID == null)
            {
                ViewBag.GioHang = cart;
                return View(model);
            }

            var khachhang = _context.Customers.AsNoTracking().SingleOrDefault(x => x.CustomerId == Convert.ToInt32(taikhoanID));
            model.CustomerId = khachhang.CustomerId;
            model.FullName = khachhang.FullName;
            model.Email = khachhang.Email;
            model.Phone = khachhang.Phone;
            model.Address = khachhang.Address;

            /*   ViewData["lsTinhThanh"] = new SelectList(_context.Locations.Where(x => x.Levels == 1).OrderBy(x => x.Type).ToList(), "Location", "Name");*/
            ViewBag.GioHang = cart;
            return View(model);
        }

        [HttpPost]
        [Route("checkout.html", Name = "Checkout")]
        public IActionResult Index(MuaHangVM muaHang)
        {
            var cart = HttpContext.Session.Get<List<CartItem>>("GioHang");
            var taikhoanID = HttpContext.Session.GetString("CustomerId");
            MuaHangVM model = new MuaHangVM();

            if (taikhoanID == null)
            {
                ViewBag.GioHang = cart;
                return View(model);
            }

            var khachhang = _context.Customers.AsNoTracking().SingleOrDefault(x => x.CustomerId == Convert.ToInt32(taikhoanID));
            model.CustomerId = khachhang.CustomerId;
            model.FullName = khachhang.FullName;
            model.Email = khachhang.Email;
            model.Phone = khachhang.Phone;
            model.Address = khachhang.Address;
            khachhang.Address = muaHang.Address;
            _context.Update(khachhang);
            _context.SaveChanges();

            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.GioHang = cart;
                    return View(model);
                }

                Order donhang = new Order();
                donhang.CustomerId = model.CustomerId;
                donhang.Address = model.Address;
                donhang.OrderDate = DateTime.Now;
                donhang.TransactStatusId = 1;//Don hang moi
                donhang.Deleted = false;
                donhang.Paid = false;
                donhang.Note = Utilities.StripHTML(model.Note);
                donhang.Total = Convert.ToInt32(cart.Sum(x => x.TotalMoney));
                _context.Add(donhang);
                _context.SaveChanges();
                _logger.LogInformation("Created Order");
                foreach (var item in cart)
                {
                    OrderDetail orderDetail = new OrderDetail();
                    orderDetail.OrderId = donhang.OrderId;
                    orderDetail.ProductId = item.product.ProductId;
                    orderDetail.Quantity = item.amount;
                    orderDetail.Total = item.amount * item.product.Price;
                    orderDetail.Price = item.product.Price;
                    orderDetail.CreateDate = DateTime.Now;
                    _context.Add(orderDetail);
                }
                _context.SaveChanges();
                _logger.LogInformation("Created Order Detail");
                HttpContext.Session.Remove("GioHang");
                _notyfService.Success("Checkout success");
                return RedirectToAction("Success");
            }
            catch
            {
                ViewBag.GioHang = cart;
                return View(model);
            }
        }

        [Route("dat-hang-thanh-cong.html", Name = "Success")]
        public IActionResult Success()
        {

            var taikhoanID = HttpContext.Session.GetString("CustomerId");
            if (string.IsNullOrEmpty(taikhoanID))
            {
                return RedirectToAction("Login", "Accounts", new { returnUrl = "/dat-hang-thanh-cong.html" });
            }
            var khachhang = _context.Customers.AsNoTracking().SingleOrDefault(x => x.CustomerId == Convert.ToInt32(taikhoanID));
            var donhang = _context.Orders
                .Where(x => x.CustomerId == Convert.ToInt32(taikhoanID))
                .OrderByDescending(x => x.OrderDate)
                .FirstOrDefault();
            MuaHangSuccessVM successVM = new MuaHangSuccessVM();
            successVM.FullName = khachhang.FullName;
            successVM.DonHangID = donhang.OrderId;
            successVM.Phone = khachhang.Phone;
            successVM.Address = khachhang.Address;
            return View(successVM);
        }
    }
}

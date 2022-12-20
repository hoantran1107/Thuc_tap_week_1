namespace WebShop.Controllers
{
    using AspNetCoreHero.ToastNotification.Abstractions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using ShopBanDo.Extension;
    using ShopBanDo.Helpper;
    using ShopBanDo.Interface;
    using ShopBanDo.Models;
    using ShopBanDo.ModelView;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CheckoutController : Controller
    {
        private readonly dbshopContext _context;
        private readonly IOrderRepository _orderRepository;
        private readonly IGenericRepository <OrderDetail> _orderDetailRepository;
        private readonly IUnitOfWork _uow;
        
        public INotyfService _notyfService { get; }
        private readonly ILogger<CheckoutController> _logger;
        public CheckoutController(dbshopContext context,
                                  IOrderRepository orderRepository,
                                  IGenericRepository<OrderDetail> orderDetailRepository,
                                  IUnitOfWork uow, 
                                  INotyfService notyfService,
                                  ILogger<CheckoutController> logger)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _uow = uow;
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
        // transaction
        [HttpPost]
        [Route("checkout.html", Name = "Checkout")]
        public async Task<IActionResult> Index()
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
            //khachhang.Address = model.Address;
            _context.Update(khachhang);
            _context.SaveChanges();
            if (ModelState.IsValid)
            {
                //Khoi tao don hang
                Order donhang = new Order();
                donhang.CustomerId = model.CustomerId;
                donhang.Address = model.Address;
                donhang.OrderDate = DateTime.Now;
                donhang.TransactStatusId = 1;//Don hang moi
                donhang.Deleted = false;
                donhang.Paid = false;
                donhang.Note = Utilities.StripHTML(model.Note);
                donhang.Total = Convert.ToInt32(cart.Sum(x => x.TotalMoney));
                //tao ordertail
                foreach (var item in cart)
                {
                    OrderDetail orderDetail = new OrderDetail();
                    orderDetail.OrderId = donhang.OrderId;
                    orderDetail.ProductId = item.product.ProductId;
                    orderDetail.Quantity = item.amount;
                    orderDetail.Total = item.amount * item.product.Price;
                    orderDetail.Price = item.product.Price;
                    orderDetail.CreateDate = DateTime.Now;
                    try
                    {
                        _orderDetailRepository.Add(orderDetail, true);
                    }
                    catch (Exception)
                    {
                        await _uow.Rollback();
                        _notyfService.Error("Error!");
                    }
                }
                try
                {
                    await _orderRepository.CreateOrder(donhang);
                    await _uow.Commit();
                    TempData["Success"] = "Success!";
                    //clear gio hang
                    HttpContext.Session.Remove("GioHang");
                    //Xuat thong bao
                    _notyfService.Success("Your order has created successfully");
                    //cap nhat thong tin khach hang
                    return RedirectToAction("Success");
                }
                catch (Exception ex)
                {
                    await _uow.Rollback();
                    _notyfService.Error("Error!");
                }
            }
            return RedirectToAction("Index");
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

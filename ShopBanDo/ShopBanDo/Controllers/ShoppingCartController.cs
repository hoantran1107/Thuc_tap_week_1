using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopBanDo.Extension;
using ShopBanDo.Models;
using ShopBanDo.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopBanDo.Repositories;

namespace ShopBanDo.Controllers
{
    /*[Authorize]*/
    public class ShoppingCartController : Controller
    {
        private readonly dbshopContext _context;
        private readonly ProductRepository _product;

        public INotyfService _notyfService { get; }

        public ShoppingCartController(dbshopContext context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
            _product = new ProductRepository(context);
        }

        public List<CartItem> GioHang
        {
            get
            {
                //Session lay ra session gio hang
                var gh = HttpContext.Session.Get<List<CartItem>>("GioHang");
                if (gh == default(List<CartItem>))
                {
                    gh = new List<CartItem>();
                }
                return gh;
            }
        }

        // them moi san pham vao gio hang
        [HttpPost]
        [Route("api/cart/add")]
        public IActionResult AddToCart(int productID, int? amount)
        {
            List<CartItem> cart = GioHang;

            try
            {
                //Them san pham vao gio hang
                //lay ra san pham trong gio hang
                CartItem item = cart.SingleOrDefault(p => p.product.ProductId == productID);
                if (item != null) // da co san pham => cap nhat so luong
                {
                    if (amount.HasValue)
                    {

                    int stock = _product.GetProductStock(productID);
                        if (amount + item.amount > stock) item.amount = stock;
                        else item.amount = item.amount + amount.Value;
                        //luu lai session
                        HttpContext.Session.Set<List<CartItem>>("GioHang", cart);
                    }
                    else
                    {
                        item.amount++;
                        HttpContext.Session.Set<List<CartItem>>("GioHang", cart);
                    }
                }
                else 
                //san pham chua co trong gio hang lay ra san pham bang id roi them vao gio voi so luong
                {
                    Product hh = _context.Products.SingleOrDefault(p => p.ProductId == productID);
                    item = new CartItem
                    {
                        amount = amount.HasValue ? amount.Value : 1,
                        product = hh
                    };
                    cart.Add(item);//Them vao gio
                }

                //Luu lai Session
                //Set<T>("key",value)
                HttpContext.Session.Set<List<CartItem>>("GioHang", cart);
                _notyfService.Success("Add product Success");
                /*return RedirectToAction("Index");*/
                //trả về json tiếp tục mua hàng
                return Json(new { success = true });
            }
            catch
            {
                return Json(new { success = false });
            }
        }
        //xoa san pham khoi gio hang
        [HttpPost]
        [Route("api/cart/remove")]
        public ActionResult Remove(int productID)
        {
            try
            {
                List<CartItem> cart = GioHang;
                CartItem item = cart.SingleOrDefault(x => x.product.ProductId == productID);
                if(item != null)
                {
                    //neu ton tai san pham trong gio hang thi xoa
                    cart.Remove(item);
                    cart.Count();
                }
                //luu lai sesson
                HttpContext.Session.Set<List<CartItem>>("GioHang", cart);
                return Json(new { success = true });
            }
            catch 
            {

                return Json(new { success = false });
            }
        }
        //cap nhat gio hang
        [HttpPost]
        [Route("api/cart/update")]
        public IActionResult UpdateCart(int productID, int? amount)
        {
            //Lay gio hang ra de xu ly
            var cart = HttpContext.Session.Get<List<CartItem>>("GioHang");
            try
            {
                if (cart != null)
                {
                    
                    CartItem item = cart.SingleOrDefault(p => p.product.ProductId == productID);
                    if (item != null && amount.HasValue) // da co -> cap nhat so luong
                    {
                        int stock = _product.GetProductStock(productID);
                        if (amount  < stock) item.amount = amount.Value;
                        else
                        {
                            item.amount = stock;
                            _notyfService.Warning("Amout more than stock");
                        }
                    }
                    //Luu lai session
                    HttpContext.Session.Set<List<CartItem>>("GioHang", cart);
                }
                return Json(new { success = true});
            }
            catch
            {
                return Json(new { success = false });
            }
        }
        //xoa tat ca san pham khoi gi hang
        [HttpPost]
        [Route("api/cart/clear")]
        public IActionResult ClearCart()
        {
            try
            {
                //Luu lai session
                HttpContext.Session.Remove("GioHang");
                return Json(new { success = true });
            }
            catch
            {
                return Json(new { success = false });
            }
        }
        //trang gio hang
        [Route("cart", Name = "Cart")]
        public IActionResult Index()
        {
            /*List<int> lsProductIDs = new List<int>();

            var cart = GioHang;

            foreach (var item in cart)
            {
                lsProductIDs.Add(item.product.ProductId);
            }

            List<Product> lsProducts = _context.Products.OrderByDescending(x => x.ProductId)
                .Where(x => x.BestSeller == true && !lsProductIDs.Contains(x.ProductId))
                .Take(6)
                .ToList();
            ViewBag.lsSanPham = lsProducts;*/


            return View(GioHang);
        }
        
    }
}

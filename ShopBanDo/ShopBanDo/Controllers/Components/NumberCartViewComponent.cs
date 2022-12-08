using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopBanDo.Extension;
using ShopBanDo.ModelView;

namespace ShopBanDo.Controllers.Components
{
    public class NumberCartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var cart = HttpContext.Session.Get<List<CartItem>>("GioHang");
            int soluongsanpham = 0;
            if(cart != null)
            {
                soluongsanpham = cart.Count();
            }
            return View(cart);
        }
    }
}

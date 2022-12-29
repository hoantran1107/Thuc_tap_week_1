using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ShopBanDo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBanDo.Controllers
{
    public class PagesController : Controller
    {
        private readonly dbshopContext _context;
        public PagesController(dbshopContext context)
        {
            _context = context;
        }
        //Get: page/Alias
        [Route("/page/{Alias}", Name = "PageDetails")]
        public async Task<IActionResult> Details(string Alias)
        {
            if (string.IsNullOrEmpty(Alias))
            {
                return RedirectToAction("Index","Home");
            }

            var page = await _context.Pages
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Alias == Alias);
            if (page == null)
            {
                return RedirectToAction("Index", "Home");
            }
            
            return View(page);
        }
    }
}

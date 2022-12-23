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
    public class BlogController : Controller
    {
        private readonly dbshopContext _context;
        public BlogController(dbshopContext context)
        {
            _context = context;
        }
        [Route("blogs", Name = ("Blog"))]
        public IActionResult Index(int? page)
        {
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            //1 PAGE GOT 20 ROW
            var pageSize = 9;
            //ASNoTracking dont create snapshot when save to database
            //better performent
            var lsTinTuc = _context.TinTucs
                .AsNoTracking()
                .OrderByDescending(x => x.PostId);
            //IsCustomers IOrderQueryable <> ToList()
            //Make change in View
            PagedList<TinTuc> models = new PagedList<TinTuc>(lsTinTuc, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;
            ViewBag.Total = models.PageCount;


            return View(models);
        }
        [Route("/news/{Alias}-{id}", Name = "TinChiTiet")]
        public async Task<IActionResult> Details(int? id)
        {
            //Next Blog 
            //Pass Blog
            if (id == null)
            {
                return NotFound();
            }

            var tinTuc = await _context.TinTucs
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.PostId == id);
            if (tinTuc == null)
            {
                return RedirectToAction("Index");
            }
            var BaiVietLienQuan = _context.TinTucs
                .AsNoTracking()
                .Where(x => x.Published == true && x.PostId != id)
                .Take(3)
                .OrderByDescending(x => x.CreatedDate)
                .ToList();

            ViewBag.BaiVietLienQuan = BaiVietLienQuan;
            return View(tinTuc);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopBanDo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopBanDo.Extension;
using ShopBanDo.Repositories;
using ShopBanDo.Interface;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace ShopBanDo.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly dbshopContext _context;
        //private readonly ShowAdminAvatarViewModel _adminAvatarViewModel;
        private OrderRespository _order;
        private ProductRepository _product;
        private GenericRepository<Customer> _customer;
        public HomeController (dbshopContext context)
        {
            _context = context;
            _order = new OrderRespository(context);
            _product = new ProductRepository(context);
            _customer = new GenericRepository<Customer>(context);
            //_adminAvatarViewModel= adminAvatarViewModel;
        }
        public ActionResult Index()
        {
            
            DateTime datenow = DateTime.Parse(DateTime.Now.ToShortDateString());
            Tab();
            AreaChar(datenow.AddDays(-14),datenow);
            //var accountAvatar = _context.Accounts.FirstOrDefault(x => x.AccountId == Convert.ToInt32(User.FindFirst("UserId").Value));
            //_adminAvatarViewModel.ImagePath = accountAvatar.ImagePath;
            //_adminAvatarViewModel.ImageName = accountAvatar.ImageName;
            TabProductLowStock();
            return View();
        }
        [HttpPost]
        public ActionResult Index(DateTime start,DateTime end)
        {
            Tab();
            AreaChar(start, end);
            TabProductLowStock();
            return View();
        }
        private void Tab()
        {
            //thong ke so tien trong ngay
            var date = DateTime.Now.ToShortDateString();
            var pass = DateTime.Now.AddDays(-1).ToShortDateString();
            var query = from c in _order.GetAll().AsQueryable() where c.PaymentDate != null select new { c.Total, c.PaymentDate,c.Paid } ;
            var r = from c in query.AsNoTracking().AsEnumerable() select new { c.Total, ShortDate = c.PaymentDate.Value.ToShortDateString(),c.Paid };
            var income = r.Where(x => x.ShortDate != null && x.ShortDate == date  && x.Paid == true).Sum(x => x.Total);
            var passIncome = r.Where(x => x.ShortDate == pass && x.Paid == true).Sum(x => x.Total);
            var totalOrder = r.Where(x => x.ShortDate == date).Count();
            
            ViewBag.Income = Extension.Extension.ToVnd(income.Value);
            if (income == 0) ViewBag.IncomePersent = 0;
            else ViewBag.IncomePersent = (income - passIncome) / income;
            ViewBag.TotalOrder = totalOrder;
            ViewBag.TotalItem = _product.GetActiveProducts().Count();
            ViewBag.TotalCustomer = _customer.GetAll().Where(x=>x.Active == true).Count();
        }
        
        private void AreaChar(DateTime start, DateTime end)
        {
            var startday = start.ToShortDateString();
            int num = (end - start).Days;
            double tong = 0;
            List<Double> ListIncome = new List<Double>();
            List<string> ListDate = new List<string>();
            for (int i = 0; i <= num; i++)
            {
                DateTime f1 = end.AddDays(-num + i);
                DateTime f2 = f1.AddDays(1);
                var q = _order.GetAll().Where(t => t.PaymentDate > f1 && t.PaymentDate < f2).Sum(t => t.Total);
                if (q == null)
                    q = 0;
                tong += (double)q;
                ListIncome.Add((Double)q);
                ListDate.Add(f1.Day.ToString() + " / " + f1.Month.ToString());
            }
            ViewBag.tong_tien = "Total money " + start.ToShortDateString() + " to " + end.ToShortDateString() + " : " + String.Format("{0:0,0.00}", tong) + " VND";
            ViewBag.C1sl = JsonConvert.SerializeObject(ListIncome); 
            ViewBag.C1name = JsonConvert.SerializeObject(ListDate,Formatting.None);
            ViewBag.Count = ListIncome.Count();
            ViewBag.Start = start;
            ViewBag.End = end;
        }

        private void TabProductLowStock()
        {
            //thong ke sản phẩm gân hết hàng

            var product = _context.Products.Where(x => x.UnitslnStock < 10 && x.Active == true);
            ViewBag.product = product;
        }
    }
}

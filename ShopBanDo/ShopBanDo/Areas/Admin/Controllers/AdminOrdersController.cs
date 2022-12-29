    using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using PagedList.Core;
    using ShopBanDo.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
namespace ShopBanDo.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy ="Staff")]
    public class AdminOrdersController : Controller
    {
        private readonly dbshopContext _context;
        public INotyfService _notyfService { get; }

        public AdminOrdersController(dbshopContext context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
        }

        // GET: Admin/AdminOrders
        public IActionResult Index(int? page)
        {
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 20;
            var lsOrder = _context.Orders.Include(o => o.Customer).Include(o => o.TransactStatus).OrderByDescending(x => x.OrderDate);
            PagedList<Order> models = new PagedList<Order>(lsOrder, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;
            return View(models);
        }

        // GET: Admin/AdminOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.TransactStatus)
                .FirstOrDefaultAsync(m => m.OrderId == id);

            if (order == null)
            {
                return NotFound();
            }
            List<OrderDetail> orderDetail = _context.OrderDetails.Include(o => o.Product).Where(x => x.OrderId == id).ToList();
            ViewBag.OrderDetails = orderDetail;
            return View(order);
        }

        // GET: Admin/AdminOrders/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId");
            ViewData["TransactStatusId"] = new SelectList(_context.TransactStatuses, "TransactStatusId", "TransactStatusId");
            return View();
        }

        // POST: Admin/AdminOrders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,CustomerId,OrderDate,ShipDate,TransactStatusId,Deleted,Paid,PaymentDate,PaymentId,Note,Total,LocationId,District,Ward,Address")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", order.CustomerId);
            ViewData["TransactStatusId"] = new SelectList(_context.TransactStatuses, "TransactStatusId", "TransactStatusId", order.TransactStatusId);
            return View(order);
        }

        public async Task<IActionResult> ChangeStatus(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .AsNoTracking()
                .Include(x => x.Customer)
                .FirstOrDefaultAsync(x => x.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["Trangthai"] = new SelectList(_context.TransactStatuses, "TransactStatusId", "Status", order.TransactStatusId);
            List<OrderDetail> orderDetail = _context.OrderDetails.Include(o => o.Product).Where(x => x.OrderId == id).ToList();
            ViewBag.OrderDetails = orderDetail;

            var list_TransacStatues = _context.TransactStatuses;
            List<SelectListItem> transactStatuses = new List<SelectListItem>();
            foreach (TransactStatus item in list_TransacStatues)
            {
                if (order.TransactStatusId > item.TransactStatusId)
                {
                    transactStatuses.Add(new SelectListItem { Text = item.Status, Value = item.TransactStatusId.ToString(), Disabled = true });
                }
                else
                {
                    transactStatuses.Add(new SelectListItem { Text = item.Status, Value = item.TransactStatusId.ToString(), Disabled = false });
                }
            }
            ViewBag.TransactStatuses = transactStatuses;

            ViewData["Trangthai"] = new SelectList(_context.TransactStatuses, "TransactStatusId", "Status", order.TransactStatusId);
            return PartialView("ChangeStatus", order);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeStatus(int id, [Bind("OrderId,CustomerId,OrderDate,ShipDate,TransactStatusId,Deleted,Paid,PaymentDate,PaymentId,Note,Total,LocationId,District,Ward,Address")] Order order)
        {
            bool checkStatus = false;
            if (id != order.OrderId)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                
                return PartialView("ChangeStatus", order);
            }
            var donhang = await _context.Orders.AsNoTracking().Include(x => x.Customer).FirstOrDefaultAsync(x => x.OrderId == id);
            if (donhang != null)
            {
                donhang.Paid = order.Paid;
                donhang.Deleted = order.Deleted;
                if(donhang.TransactStatusId == 3) checkStatus = true;
                donhang.TransactStatusId = order.TransactStatusId;
                if (donhang.Paid == true) donhang.PaymentDate = DateTime.Now;
                if (donhang.TransactStatusId == 5) donhang.Deleted = true;
                if (donhang.TransactStatusId == 3) donhang.ShipDate = DateTime.Now;
                // update product quantity if TransactStatusId = 4 (delivered)
                if (donhang.TransactStatusId == 3 && checkStatus == false)
                {
                    var orderDetails = _context.OrderDetails.Where(x => x.OrderId == id).ToList();
                    foreach (var item in orderDetails)
                    {
                        var product = _context.Products.FirstOrDefault(x => x.ProductId == item.ProductId);
                        
                        product.UnitslnStock = product.UnitslnStock - item.Quantity;
                        if(product.UnitslnStock < 0)
                        {
                            _notyfService.Error("Out of Stock"); 
                        }
                        else
                        {
                            _context.Products.Update(product);
                        }
                    }
                }
                // update product quantity if TransactStatusId = 6 (returned)
                if (donhang.TransactStatusId == 6)
                {
                    var orderDetails = _context.OrderDetails.Where(x => x.OrderId == id).ToList();
                    foreach (var item in orderDetails)
                    {
                        var product = _context.Products.FirstOrDefault(x => x.ProductId == item.ProductId);
                        product.UnitslnStock = product.UnitslnStock + item.Quantity;
                        _context.Products.Update(product);
                    }
                }

            }
            _context.Update(donhang);
            await _context.SaveChangesAsync();
            _notyfService.Success("Order update success");
            List<OrderDetail> orderDetail = _context.OrderDetails.Include(o => o.Product).Where(x => x.OrderId == id).ToList();

            ViewBag.OrderDetails = orderDetail;
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/AdminOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", order.CustomerId);
            ViewData["TransactStatusId"] = new SelectList(_context.TransactStatuses, "TransactStatusId", "TransactStatusId", order.TransactStatusId);
            return View(order);
        }

        // POST: Admin/AdminOrders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,CustomerId,OrderDate,ShipDate,TransactStatusId,Deleted,Paid,PaymentDate,PaymentId,Note,Total,LocationId,District,Ward,Address")] Order order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", order.CustomerId);
            ViewData["TransactStatusId"] = new SelectList(_context.TransactStatuses, "TransactStatusId", "TransactStatusId", order.TransactStatusId);
            return View(order);
        }

        // GET: Admin/AdminOrders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.TransactStatus)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Admin/AdminOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }
    }
}

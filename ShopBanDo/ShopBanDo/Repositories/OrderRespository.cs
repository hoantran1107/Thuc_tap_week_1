using Microsoft.EntityFrameworkCore;
using ShopBanDo.Interface;
using ShopBanDo.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBanDo.Repositories
{
    public class OrderRespository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRespository(dbshopContext context) : base(context)
        {
            
        }

       /* public IEnumerable<Order> GetOrder()
        {
            var query = from c in _context.Orders.AsQueryable() where c.PaymentDate != null select new { c.Total, c.PaymentDate, c.Paid };
            var r = from c in query.AsNoTracking().AsEnumerable() select new { c.Total, ShortDate = c.PaymentDate.Value.ToShortDateString(), c.Paid };
            return r;
        }*/
    }
}

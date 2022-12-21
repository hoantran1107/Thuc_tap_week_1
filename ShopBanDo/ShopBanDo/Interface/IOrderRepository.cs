using ShopBanDo.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBanDo.Interface
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        /*IEnumerable<Order> GetOrder();*/
        public List<Order> GetListOrderOfCustomer(int CustomerID);
        void CreateOrder(Order order);
        
    }
}

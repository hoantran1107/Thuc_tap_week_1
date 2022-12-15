using ShopBanDo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBanDo.Interface
{
    interface ICustomerRepository : IGenericRepository<Customer>
    {
        public Customer FindCustomerUsingSession(string CustomerID);
        public Customer FindCustomerUsingUserEmail(string UserEmail);
        public void UpdatePass(Customer customer,bool SaveChange);
    }
}

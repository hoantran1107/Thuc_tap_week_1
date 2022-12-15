using Microsoft.EntityFrameworkCore;
using ShopBanDo.Interface;
using ShopBanDo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBanDo.Repositories
{
    public class CustomerRespository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRespository(dbshopContext context) : base(context)
        {

        }

        public Customer FindCustomerUsingSession(string CustomerID)
        {
            return _context.Customers.AsNoTracking().SingleOrDefault(x => x.CustomerId == Convert.ToInt32(CustomerID));
        }
        public Customer FindCustomerUsingUserEmail(string UserEmail)
        {
            return _context.Customers.AsNoTracking().SingleOrDefault(x => x.Email.Trim() == UserEmail);
        }

        public void UpdatePass(Customer customer, bool SaveChange)
        {
            _context.Update(customer);
            _context.SaveChangesAsync();
        }
    }
}

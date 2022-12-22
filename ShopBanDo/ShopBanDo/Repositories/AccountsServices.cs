using ShopBanDo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBanDo.Repositories
{
    public class AccountsServices : GenericRepository<Account>
    {
        public AccountsServices(dbshopContext context) : base(context)
        {
        }
        
    }
}

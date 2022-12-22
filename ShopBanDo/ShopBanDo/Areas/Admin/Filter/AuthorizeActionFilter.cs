using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using ShopBanDo.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBanDo.Areas.Admin.Filter
{
    public class AuthorizeActionFilter : Attribute, IAuthorizationFilter
    {
        public AuthorizeActionFilter()
        {
           
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var accountId = context.HttpContext.Session.GetString("AccountId");
            if (accountId == null)
            {
                context.Result = new RedirectResult("/Admin/LoginAdmin/Login");
            }
        }
        
    }
}

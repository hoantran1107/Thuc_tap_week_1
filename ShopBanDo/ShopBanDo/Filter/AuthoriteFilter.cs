using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBanDo.NewFolder
{
    public class AuthoriteFilter : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var taikhoanID = context.HttpContext.Session.GetString("CustomerId");
            if (taikhoanID == null)
            {
                context.Result = new RedirectResult("/account/login");
            }
        }
    }
}

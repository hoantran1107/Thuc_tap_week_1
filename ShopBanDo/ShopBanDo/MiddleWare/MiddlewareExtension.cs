using Microsoft.AspNetCore.Builder;
using ShopBanDo.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBanDo.MiddleWare
{
    public static class MiddlewareExtension
    {
        public static IApplicationBuilder RegisterMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware(typeof(ExceptionMiddleware));
            return app;
        }
    }
}

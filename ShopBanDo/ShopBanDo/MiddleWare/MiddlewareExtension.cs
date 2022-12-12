using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBanDo.Extension
{
    public static class MiddlewareExtension
    {
        public static IApplicationBuilder RegisterMiddleware(this IApplicationBuilder app)
        {
            //add cac middleware muon su dung 
            app.UseMiddleware(typeof(ExceptionMiddleware));
            app.UseMiddleware(typeof(DbUpdateConcurrencyExceptionMiddleWare));
            return app;
        }
    }
}

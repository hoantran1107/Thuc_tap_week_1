using Microsoft.AspNetCore.Builder;
using ShopBanDo.Middleware;

namespace ShopBanDo.Extension
{
    public static class MiddlewareExtension
    {
        public static IApplicationBuilder RegisterMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware(typeof(ExampleMiddleware));
            return app;
        }
    }
}

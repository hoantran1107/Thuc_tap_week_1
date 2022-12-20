using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ShopBanDo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBanDo.Extension
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _loger;

        public ExceptionMiddleware(RequestDelegate next,ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _loger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {

            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {

                _loger.LogError($"Something went wrong: {ex}");
                await HanleExeptionAsync(httpContext,ex);
            }
        }

        private  Task HanleExeptionAsync(HttpContext httpContext,Exception ex)
        {
            httpContext.Response.ContentType = "application/json";
            return httpContext.Response.WriteAsync(new ErrorDetails
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = "Internal Sever Error form the custom middleware : " + ex.ToString()
            }.ToString()); ; ;
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
}

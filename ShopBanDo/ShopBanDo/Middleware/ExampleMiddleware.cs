using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ShopBanDo.Middleware
{
    public class ExampleMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly ILogger _logger;

        /// <summary>
        /// Header key of request
        /// Define id of request for tracing
        /// </summary>
        private const string RequestId = "REQUEST-ID";

        private const string DefautError = "Internal Server Error";

        public ExampleMiddleware(RequestDelegate requestDelegate, ILogger<ExampleMiddleware> logger)
        {
            _requestDelegate = requestDelegate;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                var headerValues = httpContext.Request.Headers;     // Get all header value
                if (!headerValues.TryGetValue(RequestId, out var value) || string.IsNullOrWhiteSpace(value))
                {
                    // if not exist request id in header, auto generate
                    value = Guid.NewGuid().ToString("N");
                    httpContext.Request.Headers.Add(RequestId, value);
                }
                await _requestDelegate.Invoke(httpContext);
            }
            catch(Exception e)
            {
                await HandleExceptionAsync(httpContext, e);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception e)
        {
            _logger.LogError(e, DefautError);
            httpContext.Response.StatusCode = 500;
            httpContext.Response.ContentType = "application/json";
            await httpContext.Response.WriteAsync(DefautError);
        }
    }
}

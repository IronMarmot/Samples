using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _14_MiddlewareDemo.Middlewares
{
    class MyMiddleware
    {
        RequestDelegate _next;
        ILogger<MyMiddleware> _logger;
        public MyMiddleware(RequestDelegate next, ILogger<MyMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            using (_logger.BeginScope("TraceIdentifier:{0}",context.TraceIdentifier))
            {
                _logger.LogInformation("Start");
                await _next(context);
                _logger.LogInformation("End"); 
            }
        }
    }
}

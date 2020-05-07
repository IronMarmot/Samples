using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _14_MiddlewareDemo.Middlewares
{
    public static class MyMiddlewareExtension
    {
        public static IApplicationBuilder UseMyMiddleware(this IApplicationBuilder application)
        {
            application.UseMiddleware<MyMiddleware>();
            return application;
        }
    }
}

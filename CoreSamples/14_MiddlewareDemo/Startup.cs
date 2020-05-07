using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _14_MiddlewareDemo.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace _14_MiddlewareDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            /*app.Use(async (context, next) =>
            {
                //await context.Response.WriteAsync("Hello");
                await next();
                if (context.Response.HasStarted)
                {
                    //һ���Ѿ���ʼ������������޸���Ӧͷ������
                }
                await context.Response.WriteAsync("Hello2");
            });*/

            

            app.MapWhen(context =>
            {
                return context.Request.Query.Keys.Contains("abc");
            }, abcBuilder => {
                abcBuilder.Run(async context =>
                {
                    await context.Response.WriteAsync("new abc");
                });
            });

            app.Map("/abc", abcBuilder =>
            {
                abcBuilder.Use(async (context, next) =>
                {
                    //await context.Response.WriteAsync("Hello");
                    await next();
                    await context.Response.WriteAsync("Hello2");
                });
            });

            //custom middleware
            //app.UseMiddleware<MyMiddleware>();
            app.UseMyMiddleware();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

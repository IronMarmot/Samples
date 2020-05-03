using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using _03_DependencyInjectionScopeAndDisposableDemo.services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace _03_DependencyInjectionScopeAndDisposableDemo
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
            /*services.AddSingleton<IOrderService>(factory =>
            {
                return new OrderService();
            });*/
            //services.AddScoped<IOrderService, OrderService>();
            //services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IOrderService,orders>();
            //IOrderService order = new OrderService();
            //services.AddSingleton<IOrderService>(order);

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var service = app.ApplicationServices.GetService<IOrderService>();
            var service1 = app.ApplicationServices.GetService<IOrderService>();
            var service2 = app.ApplicationServices.GetService<IOrderService>();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

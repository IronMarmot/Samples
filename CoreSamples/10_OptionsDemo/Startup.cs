using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _10_OptionsDemo.service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;

namespace _10_OptionsDemo
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
            ChangeToken.OnChange(() => Configuration.GetReloadToken(), () =>
            {
                Console.WriteLine("���¼�������");
            });

            //�����ȡOrderOptions��ֵ
            //services.AddSingleton<OrderOptions>();
            //services.AddSingleton<IOrderService, OrderService>();

            //ѡ����IOptions����
            /*services.Configure<OrderOptions>(Configuration.GetSection("MaxOrderCount"));
            services.AddSingleton<IOrderService, OrderService>();*/

            //���ַ�����װ
            services.AddOrderService(Configuration.GetSection("MaxOrderCount"));

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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

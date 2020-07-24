using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _02_DependencyInjection.services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace _02_DependencyInjection
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
            #region 服务注册
            services.AddSingleton<IMySingletonService, MySingletonService>();
            services.AddScoped<IMyScopedService, MyScopedService>();
            services.AddTransient<IMYTransientService, MyTransientService>();
            #endregion

            #region 其他注册方式
            //直接注入实例
            //services.AddSingleton<IOrderService>(new OrderService());
            ////工厂注册
            //services.AddSingleton<IOrderService>(factory =>
            //{
            //    return new OrderServiceEx();
            //});//可以注入，实现不同
            #endregion

            #region 尝试注册
            //TryAdd...，相同类型的不会重复注入
            services.TryAddSingleton<IOrderService, OrderService>();//不会注入，相同类型的都不会注入
            //services.TryAddSingleton<IOrderService, OrderServiceEx>();//不会注入，相同类型的都不会注入

            //TryAddIEnumerable,可以注入不同实现
            services.TryAddEnumerable(ServiceDescriptor.Singleton<IOrderService, OrderServiceEx>());//可以注入
            //services.TryAddEnumerable(services.AddSingleton<IOrderService, OrderServiceEx>());//注入报错,System.ArgumentException
            //注入报错,System.ArgumentException
            //services.TryAddEnumerable(ServiceDescriptor.Singleton<IOrderService>(factory =>
            //{
            //    return new OrderServiceEx();
            //}));
            #endregion

            #region 服务替换和移除
            services.Replace(ServiceDescriptor.Singleton<IOrderService, OrderService>());//移除第一个相同类型的服务，并添加该服务
            services.Remove(ServiceDescriptor.Singleton<IOrderService, OrderService>());//不起作用，不是service的方法
            services.RemoveAt(0);//不起作用，不是service的方法
            //services.RemoveAll<IOrderService>();
            #endregion

            #region 泛型注册
            services.AddSingleton(typeof(IGenericServie<>),typeof(GenericService<>));
            #endregion

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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

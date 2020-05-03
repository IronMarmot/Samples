using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _04_DIAutofacDemo.services;
using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.DynamicProxy;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace _04_DIAutofacDemo
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

        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            #region 常规注册
            containerBuilder.RegisterType<MyService>();//通过IMyService不能获取
            containerBuilder.RegisterType<MyService>().As<IMyService>();
            containerBuilder.RegisterType<MyServiceV2>().As<IMyService>();
            #endregion

            #region 命名注册
            //containerBuilder.RegisterType<MyService>().Named<IMyService>("myService");
            #endregion

            #region 属性注入
            //containerBuilder.RegisterType<MyNameService>();
            //containerBuilder.RegisterType<MyServiceV2>().As<IMyService>().PropertiesAutowired();
            #endregion

            #region AOP
            //containerBuilder.RegisterType<Interceptor>();
            //containerBuilder.RegisterType<MyService>().As<IMyService>().InterceptedBy(typeof(Interceptor)).EnableInterfaceInterceptors();
            #endregion

            #region 子容器，除了使用scope方式，还可以使用该方式
            containerBuilder.RegisterType<MyServiceV2>().InstancePerMatchingLifetimeScope("myscope");
            #endregion
        }

        public ILifetimeScope AutofacContainer { get; private set; }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            this.AutofacContainer = app.ApplicationServices.GetAutofacRoot();

            //var service = this.AutofacContainer.Resolve<IMyService>();
            //service.show();

            /*//命名方式获取
            IMyService myService = this.AutofacContainer.ResolveNamed<IMyService>("myService");
            myService.show();*/

            using (var myscope = AutofacContainer.BeginLifetimeScope("myscope"))
            {
                var service0 = myscope.Resolve<MyServiceV2>();
                using (var scope = myscope.BeginLifetimeScope())
                {
                    var service1 = myscope.Resolve<MyServiceV2>();
                    var service2 = myscope.Resolve<MyServiceV2>();
                    Console.WriteLine($"service1==service2?{service1 == service2}");
                    Console.WriteLine($"service1==service2?{service0 == service1}");
                }
            }

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

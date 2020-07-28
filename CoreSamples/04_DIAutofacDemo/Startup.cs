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

        /// <summary>
        /// autofac的服务注册方法，相当于configureService方法
        /// 当使用ConfigureContainer时，默认的ConfigureService方法会被autofac方法接替
        /// </summary>
        /// <param name="containerBuilder"></param>
        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            #region 常规注册
            containerBuilder.RegisterType<MyService>();//通过IMyService不能获取
            containerBuilder.RegisterType<MyService>().As<IMyService>();
            containerBuilder.RegisterType<MyServiceV2>().As<IMyService>();
            #endregion

            #region 命名注册
            //命名注册时，一定是包含类型的，因此保证通过名称一定可以获取。Named有2种方式。
            //containerBuilder.RegisterType<MyService>().Named<IMyService>("myService");
            #endregion

            #region 属性注入
            //属性注入分为2步，先注入属性类型，再在属性类上开启属性注入。
            //containerBuilder.RegisterType<MyNameService>();
            //containerBuilder.RegisterType<MyServiceV2>().As<IMyService>().PropertiesAutowired();
            #endregion

            #region AOP
            //分3步，先注册拦截器，通过InterceptedBy定义服务允许的拦截器类型，再开启拦截器
            //如果注册多个拦截器，那么拦截器都会执行，而且是按照注册的顺序执行
            containerBuilder.RegisterType<Interceptor>();
            containerBuilder.RegisterType<Intercepotor2>();
            containerBuilder.RegisterType<MyService>().As<IMyService>().InterceptedBy(new Type[] { typeof(Intercepotor2),typeof(Interceptor) }).EnableInterfaceInterceptors();
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
                    var service1 = scope.Resolve<MyServiceV2>();
                    var service2 = scope.Resolve<MyServiceV2>();
                    Console.WriteLine($"service1==service2?{service1 == service2}");
                    Console.WriteLine($"service0==service1?{service0 == service1}");
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

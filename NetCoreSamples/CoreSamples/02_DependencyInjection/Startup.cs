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
            #region ����ע��
            services.AddSingleton<IMySingletonService, MySingletonService>();
            services.AddScoped<IMyScopedService, MyScopedService>();
            services.AddTransient<IMYTransientService, MyTransientService>();
            #endregion

            #region ����ע�᷽ʽ
            //ֱ��ע��ʵ��
            services.AddSingleton<IOrderService>(new OrderService());
            ////����ע��
            //services.AddSingleton<IOrderService>(factory =>
            //{
            //    return new OrderServiceEx();
            //});//����ע�룬ʵ�ֲ�ͬ
            #endregion

            #region ����ע��
            //TryAdd...����ͬ���͵Ĳ����ظ�ע��
            services.TryAddSingleton<IOrderService, OrderService>();//����ע�룬��ͬ���͵Ķ�����ע��
            services.TryAddSingleton<IOrderService, OrderServiceEx>();//����ע�룬��ͬ���͵Ķ�����ע��

            //TryAddIEnumerable,����ע�벻ͬʵ��
            //services.TryAddEnumerable(services.AddSingleton<IOrderService, OrderService>());//ע�뱨��,System.ArgumentException
            services.TryAddEnumerable(ServiceDescriptor.Singleton<IOrderService, OrderServiceEx>());//����ע��

            //ע�뱨��,System.ArgumentException
            //services.TryAddEnumerable(ServiceDescriptor.Singleton<IOrderService>(factory =>
            //{
            //    return new OrderServiceEx();
            //}));
            #endregion

            #region �����滻���Ƴ�
            services.Replace(ServiceDescriptor.Singleton<IOrderService, OrderServiceEx>());//�����ԣ�����ֻ���滻�ǳ���ע��ķ���
            services.Remove(ServiceDescriptor.Singleton<IOrderService, OrderService>());
            //services.RemoveAt(0);
            //services.RemoveAll<IOrderService>();
            #endregion

            #region ����ע��
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

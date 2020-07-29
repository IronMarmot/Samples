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

/*
 * ����˵���м���õ�use/usewhen/map/mapwhen����
    map/mapwhen��ʹ�м����·��������ƥ��ľ�ִ�з�֧��
    ��use/usewhen��ͬ����������֧��������·������ն��м����������¼������ܵ���
    ����Run�����ᴴ��һ���ն��м���Ӷ�ʵ�ֶ�·,���������ڲ����ܵ���next��
    ��use�ɽ��������ί��������һ��

    ע���м����ע��˳�򣬲ο�https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/middleware/?view=aspnetcore-3.1
**/
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

            //�� Use ���������ί��������һ�� next ������ʾ�ܵ��е���һ��ί�С� ��ͨ�������� next ����ʹ�ܵ���·��
            //Run ί�в����յ� next ������ ��һ�� Run ί��ʼ��Ϊ�նˣ�������ֹ�ܵ��� Run ��һ��Լ���� ĳЩ�м��������ܻṫ���ڹܵ�ĩβ���� Run[Middleware] ����

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


            //MapWhen ���ڸ���ν�ʵĽ����������ܵ���֧
            app.MapWhen(context =>
            {
                return context.Request.Query.Keys.Contains("abc");
            }, abcBuilder => {
                abcBuilder.Run(async context =>
                {
                    //run�ڲ����ܵ���next()
                    await context.Response.WriteAsync("new abc");
                });
            });

            //Map ��չ����Լ���������ܵ���֧�� Map ���ڸ�������·����ƥ��������������ܵ���֧�� �������·���Ը���·����ͷ����ִ�з�֧��
            //ʹ�� Map ʱ������ HttpRequest.Path ��ɾ��ƥ���·���Σ������ÿ�����󽫸�·����׷�ӵ� HttpRequest.PathBase��
            //Map ֧��Ƕ�� 
                //app.Map("/level1", level1App => {
                //level1App.Map("/level2a", level2AApp => {
                //    // "/level1/level2a" processing
                //});
            //����ͬʱƥ������   app.Map("/map1/seg1", HandleMultiSeg)
            app.Map("/abc", abcBuilder =>
            {
                abcBuilder.Use(async (context, next) =>
                {
                    //await context.Response.WriteAsync("Hello");
                    await next();
                    await context.Response.WriteAsync("Hello2");
                });
            });

            app.Use(async (context, next) =>
            {
                //await context.Response.WriteAsync("Hello");
                await next();
                await context.Response.WriteAsync("Hello2");
            });

            //UseWhen Ҳ���ڸ���ν�ʵĽ����������ܵ���֧�� �� MapWhen ��ͬ���ǣ���������֧��������·������ն��м����������¼������ܵ�
            app.UseWhen(context =>
            {
                return context.Request.Query.ContainsKey("abc");
            }, app =>
             {
                 app.Use(async (context, next) =>
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

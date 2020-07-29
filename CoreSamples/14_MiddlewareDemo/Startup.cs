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
 * 简单来说，中间件用到use/usewhen/map/mapwhen四种
    map/mapwhen会使中间件短路，即遇到匹配的就执行分支。
    而use/usewhen不同，如果这个分支不发生短路或包含终端中间件，则会重新加入主管道。
    调用Run方法会创建一个终端中间件从而实现短路,所以在其内部不能调用next。
    用use可将多个请求委托链接在一起。

    注意中间件的注册顺序，参考https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/middleware/?view=aspnetcore-3.1
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

            //用 Use 将多个请求委托链接在一起。 next 参数表示管道中的下一个委托。 可通过不调用 next 参数使管道短路。
            //Run 委托不会收到 next 参数。 第一个 Run 委托始终为终端，用于终止管道。 Run 是一种约定。 某些中间件组件可能会公开在管道末尾运行 Run[Middleware] 方法

            /*app.Use(async (context, next) =>
            {
                //await context.Response.WriteAsync("Hello");
                await next();
                if (context.Response.HasStarted)
                {
                    //一旦已经开始输出，则不能再修改响应头的内容
                }
                await context.Response.WriteAsync("Hello2");
            });*/


            //MapWhen 基于给定谓词的结果创建请求管道分支
            app.MapWhen(context =>
            {
                return context.Request.Query.Keys.Contains("abc");
            }, abcBuilder => {
                abcBuilder.Run(async context =>
                {
                    //run内部不能调用next()
                    await context.Response.WriteAsync("new abc");
                });
            });

            //Map 扩展用作约定来创建管道分支。 Map 基于给定请求路径的匹配项来创建请求管道分支。 如果请求路径以给定路径开头，则执行分支。
            //使用 Map 时，将从 HttpRequest.Path 中删除匹配的路径段，并针对每个请求将该路径段追加到 HttpRequest.PathBase。
            //Map 支持嵌套 
                //app.Map("/level1", level1App => {
                //level1App.Map("/level2a", level2AApp => {
                //    // "/level1/level2a" processing
                //});
            //还可同时匹配多个段   app.Map("/map1/seg1", HandleMultiSeg)
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

            //UseWhen 也基于给定谓词的结果创建请求管道分支。 与 MapWhen 不同的是，如果这个分支不发生短路或包含终端中间件，则会重新加入主管道
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

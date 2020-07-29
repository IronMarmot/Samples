using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _15_ExceptionDemo.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

/*
 * 异常处理方式：
 *      开发人员异常页
 *      异常处理程序页
 *      异常处理程序lambda
 *      UseStatusCodePages
 *          包含格式字符串的 UseStatusCodePages
 *          包含 lambda 的 UseStatusCodePages
 *          UseStatusCodePagesWithRedirects（向客户端发送“302 - 已找到”状态代码。将客户端重定向到 URL 模板中的位置。）
 *          UseStatusCodePagesWithReExecute（向客户端返回原始状态代码。通过使用备用路径重新执行请求管道，从而生成响应正文。）
 *      禁用状态代码页
 *      
 * 异常访问
 *      使用 IExceptionHandlerPathFeature 访问错误处理程序控制器或页中的异常和原始请求路径
 *      
 * 异常类型处理
 *      响应头
 *          应用无法更改响应的状态代码。
            任何异常页或处理程序都无法运行。 必须完成响应或中止连接。
        服务器异常处理
            应用程序无法处理的请求将由服务器进行处理。 
            当服务器处理请求时，发生的任何异常都将由服务器的异常处理进行处理。 应用的自定义错误页面、异常处理中间件和筛选器都不会影响此行为。
        启动异常处理
            应用程序启动期间发生的异常仅可在承载层进行处理。 可以将主机配置为，捕获启动错误和捕获详细错误。
        数据库错误页
            应仅在开发环境中启用此页。
            UseDatabaseErrorPage 需要 Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore NuGet 包。
        异常筛选器
            异常筛选器适合捕获 MVC 操作内发生的异常，但它们不如异常处理中间件灵活。 建议使用中间件。 仅在需要根据选定 MVC 操作以不同方式执行错误处理时，才使用筛选器。
        模型状态错误
 * https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/error-handling?view=aspnetcore-3.1
 */
namespace _15_ExceptionDemo
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
            //services.AddMvc();
            //services.AddControllers();
            //services.AddMvc().AddMvcOptions(options =>
            //{
            //    //options.Filters.Add<MyExceptionFilter>();
            //    options.Filters.Add<MyExceptionFilterAttribute>();
            //}).AddJsonOptions(jsonoptions =>
            //{
            //    jsonoptions.JsonSerializerOptions.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
            //});
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //app.UseExceptionHandler("/error");
            app.UseExceptionHandler(configure =>
            {
                configure.Run(async context =>
                {
                    IExceptionHandlerPathFeature exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                    Exception ex = exceptionHandlerPathFeature?.Error;

                    var knowException = ex as IKnowException;
                    if (knowException == null)
                    {
                        knowException = KnowException.Unkown;
                        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    }
                    else
                    {
                        knowException = KnowException.FromKnowException(knowException);
                        context.Response.StatusCode = StatusCodes.Status200OK;
                    }
                    var jsonOptions = context.RequestServices.GetService<IOptions<JsonOptions>>();
                    context.Response.ContentType = "application/json;charset=utf-8";
                    await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(knowException, jsonOptions.Value.JsonSerializerOptions));
                });
            });

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

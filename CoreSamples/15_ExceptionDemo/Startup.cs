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
            services.AddMvc().AddMvcOptions(options =>
            {
                //options.Filters.Add<MyExceptionFilter>();
                options.Filters.Add<MyExceptionFilterAttribute>();
            }).AddJsonOptions(jsonoptions =>
            {
                jsonoptions.JsonSerializerOptions.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
            });
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
            //app.UseExceptionHandler(configure =>
            //{
            //    configure.Run(async context=>
            //    {
            //        IExceptionHandlerPathFeature exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
            //        Exception ex = exceptionHandlerPathFeature?.Error;

            //        var knowException = ex as IKnowException;
            //        if (knowException == null)
            //        {
            //            knowException = KnowException.Unkown;
            //            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            //        }
            //        else
            //        {
            //            knowException = KnowException.FromKnowException(knowException);
            //            context.Response.StatusCode = StatusCodes.Status200OK;
            //        }
            //        var jsonOptions = context.RequestServices.GetService<IOptions<JsonOptions>>();
            //        context.Response.ContentType = "application/json;charset=utf-8";
            //        await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(knowException,jsonOptions.Value.JsonSerializerOptions));
            //    });
            //});

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

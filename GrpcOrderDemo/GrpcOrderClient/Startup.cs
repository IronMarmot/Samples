using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GrpcOrderService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GrpcOrderClient
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            services.AddGrpcClient<Order.OrderClient>(option =>
            {
                //option.Address = new Uri("https://localhost:5001");
                option.Address = new Uri("http://localhost:5002");
            });
            //.ConfigurePrimaryHttpMessageHandler(provider =>
            //    {
            //        var handler = new SocketsHttpHandler();
            //        handler.SslOptions.RemoteCertificateValidationCallback = (a, b, c, d) => true; //允许无效、或自签名证书
            //        return handler;
            //    });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    int orderId;
                    Order.OrderClient orderClient = context.RequestServices.GetService<Order.OrderClient>();
                    try
                    {
                        var OrderResponse = orderClient.CreateOrder(new OrderRequest() { Id = 123, Name = "Phone", Price = 500.0 });
                        orderId = OrderResponse.OrderId;
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }
                    context.Response.ContentType = "text/plain; charset=utf-8";
                    await context.Response.WriteAsync($"订单创建成功，订单编号：{orderId}");
                });
            });
        }
    }
}

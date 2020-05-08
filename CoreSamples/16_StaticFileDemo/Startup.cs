using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace _16_StaticFileDemo
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
            services.AddDirectoryBrowser();//Ŀ¼
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            //Ĭ���ļ�
            //app.UseDefaultFiles();
            //����Ŀ¼���
            //app.UseDirectoryBrowser();

            app.UseStaticFiles();//Ĭ�϶�Ӧwwwroot

            //�����Զ����ļ�·����ӳ�䵽�ض���path
            //app.UseStaticFiles(new StaticFileOptions { RequestPath = "/file", FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "file")) });

            //HTML5 History·��ģʽ
            //����/api������ȫ���ض���index.html
            app.MapWhen(context =>
            {
                return !context.Request.Path.Value.StartsWith("/api");
            }, configuration =>
             {
                 RewriteOptions rewriteOptions = new RewriteOptions();
                 rewriteOptions.AddRedirect(".*", "/index.html");
                 configuration.UseRewriter(rewriteOptions);

                 configuration.UseStaticFiles();

                /*//���Ƽ������ص���Ϣͷȱ����Ϣ 
                 configuration.Run(async handler =>
                 {
                     IFileInfo fileInfo = env.WebRootFileProvider.GetFileInfo("index.html");
                     handler.Response.ContentType = "text/html";
                     using (FileStream fileStream=new FileStream(fileInfo.PhysicalPath,FileMode.Open,FileAccess.Read))
                     {
                         await StreamCopyOperation.CopyToAsync(fileStream, handler.Response.Body, null, 64 * 1024, handler.RequestAborted);
                     }
                 });*/
             });
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

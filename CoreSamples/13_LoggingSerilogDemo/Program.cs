using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Formatting.Compact;

namespace _13_LoggingSerilogDemo
{
    public class Program
    {
        //配置定义
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")??"Production"}.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

        public static int Main(string[] args)
        {
            //Log.Logger定义
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(Configuration)
                .MinimumLevel.Debug()//设置最低等级level
                .Enrich.FromLogContext()
                .WriteTo.Console(new RenderedCompactJsonFormatter())
                .WriteTo.File(formatter: new CompactJsonFormatter(), "logs\\myapp.txt", rollingInterval: RollingInterval.Day)//在设定的路径下，用文件名和设定的时间周期生成文件名。
                //.WriteTo.Debug()
                .CreateLogger();
            try
            {
                Log.Information("Information:Starting web host");
                Log.Debug("Debug!!!");
                Log.Warning("Warning!!!");
                CreateHostBuilder(args).Build().Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "host terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
           
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
            .UseSerilog(dispose:true);
    }
}

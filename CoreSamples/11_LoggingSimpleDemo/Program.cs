using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace _11_LoggingSimpleDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("settings.json");
            IConfigurationRoot configurationRoot = configurationBuilder.Build();

            IServiceCollection serviceDescriptors = new ServiceCollection();
            serviceDescriptors.AddSingleton<IConfiguration>(p => configurationRoot);
            serviceDescriptors.AddLogging(configure =>
            {
                configure.AddConfiguration(configurationRoot.GetSection("Logging"));
                configure.AddConsole();//这里如果要在配置文件中定义level，那么，节的名称必须是”Console“，区分大小写的，否则不生效
            });

            

            {
                ServiceProvider serviceProvider = serviceDescriptors.BuildServiceProvider();
                //创建指定名称的logger
                ILoggerFactory loggerFactory = serviceProvider.GetService<ILoggerFactory>();
                ILogger logger = loggerFactory.CreateLogger("alogger");

                //创建本程序的logger
                //ILoggerFactory loggerFactory = serviceProvider.GetService<ILoggerFactory>();
                //ILogger logger = loggerFactory.CreateLogger("_11_LoggingSimpleDemo.Program");

                //ILogger logger = serviceProvider.GetService<ILogger<Program>>();

                logger.LogTrace("trace");
                logger.LogDebug("debug");
                logger.LogInformation("information");
                logger.LogWarning("warning");
            }

            {
               /* //在服务中定义logger，这样可在操作服务时直接打印日志
                //推荐方式：推荐直接在服务中定义logger，这样其命名是固定的 命名空间+类名，其实asp.net core自身的controller就是这样实现的
                serviceDescriptors.AddScoped<OrderService>();
                ServiceProvider serviceProvider = serviceDescriptors.BuildServiceProvider();

                OrderService orderService = serviceProvider.GetService<OrderService>();
                orderService.PrintLog();*/
            }

            Console.ReadKey();
        }
    }
}

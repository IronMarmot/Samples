using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace _12_LoggingScopeDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("settings.json");
            IConfigurationRoot configurationRoot = configurationBuilder.Build();

            IServiceCollection serviceDescriptors = new ServiceCollection();
            serviceDescriptors.AddLogging(configure => {
                configure.AddConfiguration(configurationRoot.GetSection("logging"));
                configure.AddConsole();
                configure.AddDebug();
            });

            serviceDescriptors.AddSingleton<IConfiguration>(p => configurationRoot);
            IServiceProvider serviceProvider = serviceDescriptors.BuildServiceProvider();

            ILogger logger = serviceProvider.GetService<ILogger<Program>>();
            logger.LogDebug("this is debug");
            logger.LogInformation("this is information");
            logger.LogWarning("this is warning");
            System.Threading.Thread.Sleep(100);
            Console.WriteLine("=========以下是scope========");

            while (Console.ReadKey().Key != ConsoleKey.Escape)
            {
                using (logger.BeginScope("ScopedId:{scopedId}", Guid.NewGuid()))
                {
                    logger.LogDebug("this is debug");
                    logger.LogInformation("this is information");
                    logger.LogWarning("this is warning");
                }
                Console.WriteLine("====================");
                System.Threading.Thread.Sleep(1000);
            }

            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }
}

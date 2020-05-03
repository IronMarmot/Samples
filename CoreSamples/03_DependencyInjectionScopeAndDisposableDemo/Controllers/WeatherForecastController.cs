using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _03_DependencyInjectionScopeAndDisposableDemo.services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace _03_DependencyInjectionScopeAndDisposableDemo.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet]
        public int GetService([FromServices] IOrderService orderService1,
            [FromServices] IOrderService orderService2)
        {
            Console.WriteLine("=======创建子容器对象========");
            using (IServiceScope scope = HttpContext.RequestServices.CreateScope())
            {
                var service = scope.ServiceProvider.GetService<IOrderService>();
                var service1 = scope.ServiceProvider.GetService<IOrderService>();
                var service2 = scope.ServiceProvider.GetService<IOrderService>();
            }
            Console.WriteLine("============子容器对象释放完成===========");

            Console.WriteLine("接口处理结束");
            return 1;
        }

        //[HttpGet]
        //public int GetService([FromServices] IOrderService orderService1,
        //[FromServices] IOrderService orderService2,
        //[FromServices] IHostApplicationLifetime hostApplicationLifetime,
        //    [FromQuery]bool stop)
        //{
        //    Console.WriteLine("=======创建子容器对象========");
        //    using (IServiceScope scope = HttpContext.RequestServices.CreateScope())
        //    {
        //        var service = scope.ServiceProvider.GetService<IOrderService>();
        //        var service1 = scope.ServiceProvider.GetService<IOrderService>();
        //        var service2 = scope.ServiceProvider.GetService<IOrderService>();
        //    }
        //    Console.WriteLine("============子容器对象释放完成===========");

        //    Console.WriteLine("接口处理结束");
        //    if (stop)
        //    {
        //        hostApplicationLifetime.StopApplication();
        //    }
        //    return 1;
        //}

        [HttpGet]
        public int appExit([FromServices] IHostApplicationLifetime hostApplicationLifetime,
            [FromQuery]bool stop)
        {
            if (stop)
            {
                hostApplicationLifetime.StopApplication();
            }
            return 1;
        }
    }
}

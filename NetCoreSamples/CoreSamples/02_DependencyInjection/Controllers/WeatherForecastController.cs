using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _02_DependencyInjection.services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace _02_DependencyInjection.Controllers
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

        public WeatherForecastController(ILogger<WeatherForecastController> logger,IOrderService orderService,IGenericServie<IOrderService> genericServie)
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
        public int GetServices([FromServices]IMySingletonService mySingleton1,
            [FromServices]IMySingletonService mySingleton2,
            [FromServices]IMyScopedService myScopedService1,
            [FromServices]IMyScopedService myScopedService2,
            [FromServices]IMYTransientService mYTransientService1,
            [FromServices]IMYTransientService mYTransientService2)
        {
            Console.WriteLine($"mySingleton1:{mySingleton1.GetHashCode()}");
            Console.WriteLine($"mySingleton2:{mySingleton2.GetHashCode()}");
            Console.WriteLine($"myScopedService1:{myScopedService1.GetHashCode()}");
            Console.WriteLine($"myScopedService2:{myScopedService2.GetHashCode()}");
            Console.WriteLine($"mYTransientService1:{mYTransientService1.GetHashCode()}");
            Console.WriteLine($"mYTransientService2:{mYTransientService2.GetHashCode()}");
            Console.WriteLine("=========请求结束============");

            return 1;
        }

        [HttpGet]
        public int GetServiceList([FromServices] IEnumerable<IOrderService> orderServices)
        {
            foreach (var item in orderServices)
            {
                Console.WriteLine($"{item.GetType()}-{item.GetHashCode()}");
            }
            return 1;
        }
    }
}

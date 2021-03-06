﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _10_OptionsDemo.service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace _10_OptionsDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
        public void Get([FromServices]IOrderService service)
        {
            service.ShowMaxOrderCount();
        }
    }
}

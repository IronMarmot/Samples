using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace _11_LoggingSimpleDemo
{
    //logger集成到服务，可在方法中直接添加logger输出
    public class OrderService
    {
        ILogger<OrderService> logger;

        public OrderService(ILogger<OrderService> logger)
        {
            this.logger = logger;
        }

        public void PrintLog()
        {
            logger.LogDebug(" Debug: Time now:{time}",DateTime.Now.ToString());
            logger.LogInformation(" info: Time now:{time}",DateTime.Now.ToString());
        }
    }
}

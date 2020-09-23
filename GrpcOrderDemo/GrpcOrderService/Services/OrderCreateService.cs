using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace GrpcOrderService
{
    public class OrderCreateService : Order.OrderBase
    {
        private readonly ILogger<OrderCreateService> _logger;
        public OrderCreateService(ILogger<OrderCreateService> logger)
        {
            _logger = logger;
        }

        public override Task<OrderResponse> CreateOrder(OrderRequest request, ServerCallContext context)
        {
            return Task.FromResult(new OrderResponse
            {
                OrderId = 12345
            }); ;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _03_DependencyInjectionScopeAndDisposableDemo.services
{
    public interface IOrderService
    {

    }

    public class OrderService : IOrderService, IDisposable
    {
        public void Dispose()
        {
            Console.WriteLine($"DisposableOrderService Disposabled:{this.GetHashCode()}");
        }
    }
}

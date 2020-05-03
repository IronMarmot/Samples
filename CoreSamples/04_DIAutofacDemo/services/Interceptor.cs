using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _04_DIAutofacDemo.services
{
    public class Interceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            Console.WriteLine($"{invocation.Method.Name}执行前");
            invocation.Proceed();
            Console.WriteLine($"{invocation.Method.Name}执行后");
        }
    }
}

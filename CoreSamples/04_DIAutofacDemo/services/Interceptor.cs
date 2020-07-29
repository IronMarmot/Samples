using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _04_DIAutofacDemo.services
{
    public class Interceptor : IInterceptor
    {
        /// <summary>
        /// Providers the main DynamicProxy extension point that allows member invocation.
        /// </summary>
        /// <param name="invocation"></param>
        public void Intercept(IInvocation invocation)
        {
            Console.WriteLine($"Intercepotor.{invocation.Method.Name}执行前");
            invocation.Proceed();
            Console.WriteLine($"Intercepotor.{invocation.Method.Name}执行后");
        }
    }

    public class Intercepotor2 : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            Console.WriteLine($"Intercepotor2.{invocation.Method.Name}执行前");
            invocation.Proceed();
            Console.WriteLine($"Intercepotor2.{invocation.Method.Name}执行后");
        }
    }
}

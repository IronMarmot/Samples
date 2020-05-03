using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _04_DIAutofacDemo.services
{
    public interface IMyService {
        void show();
    }
    public class MyService : IMyService
    {
        public void show()
        {
            Console.WriteLine($"this is MyService.show，{GetHashCode()}");
        }
    }

    public class MyServiceV2 : IMyService
    {
        public MyNameService myNameService { get; set; }
        public void show()
        {
            Console.WriteLine($"this is MyServiceV2.show，{GetHashCode()},MyNameService是否为空:{myNameService==null}");
        }
    }

    public class MyNameService { }
}

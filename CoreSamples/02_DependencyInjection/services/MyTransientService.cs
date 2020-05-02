using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _02_DependencyInjection.services
{
    public interface IMYTransientService { }

    public class MyTransientService: IMYTransientService
    {
    }
}

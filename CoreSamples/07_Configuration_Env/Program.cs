using Microsoft.Extensions.Configuration;
using System;

namespace _07_Configuration_Env
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            //configurationBuilder.AddEnvironmentVariables();
            //IConfigurationRoot configurationRoot = configurationBuilder.Build();
            //Console.WriteLine($"key1:{configurationRoot["key1"]}");
            //Console.WriteLine($"key2:{configurationRoot["key2"]}");

            //IConfigurationSection section1 = configurationRoot.GetSection("env1");
            //Console.WriteLine($"key3:{section1["key3"]}");
            //Console.WriteLine($"key4:{section1["key4"]}");
            //Console.WriteLine("Hello World!");


            #region 前缀过滤
            //只注册特定前缀的环境变量，且以去掉前缀的剩余串为键值
            //当只需注册特定一些环境变量，而不是全部时，可用前缀过滤来区分
            configurationBuilder.AddEnvironmentVariables("env1");
            IConfigurationRoot configurationRoot = configurationBuilder.Build();
            Console.WriteLine($"env1:{configurationRoot[""]}");
            Console.WriteLine($"env1_:{configurationRoot["_"]}");
            Console.WriteLine($"env1:key3:{configurationRoot[":key3"]}");
            Console.WriteLine($"env1__key4:{configurationRoot["__key4"]}");//不能获取，猜测__是环境变量独有的识别为section的方式，因此必须通过section获取

            IConfigurationSection configurationSection = configurationRoot.GetSection("");
            Console.WriteLine($"key3:{configurationSection["key3"]}");
            Console.WriteLine($"key4:{configurationSection["key4"]}"); 
            #endregion
            Console.WriteLine();
        }
    }
}

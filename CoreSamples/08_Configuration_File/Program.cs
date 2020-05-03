using Microsoft.Extensions.Configuration;
using System;

namespace _08_Configuration_File
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            //optional:false是默认值，若不存在则报错
            //reloadOnchange:true是默认值，配置文件变更时，可获取新值
            configurationBuilder.AddJsonFile("settings.json",optional:false,reloadOnChange:true);
            configurationBuilder.AddIniFile("settings.ini");

            IConfigurationRoot configurationRoot = configurationBuilder.Build();
            Console.WriteLine($"key1:{configurationRoot["key1"]}");
            Console.WriteLine($"key2:{configurationRoot["key2"]}");
            Console.WriteLine($"key3:{configurationRoot["Section1:key3"]}");//也可获取

            IConfigurationSection configurationSection = configurationRoot.GetSection("Section1");
            Console.WriteLine($"key3:{configurationSection["key3"]}");
            Console.WriteLine($"key4:{configurationSection["key4"]}");
            Console.WriteLine($"key5:{configurationSection["key5"]}");

            //ini
            Console.WriteLine($"key6:{configurationRoot["key6"]}");

            //这里还需要注意，后加载的会覆盖先加载的值
            Console.WriteLine("Hello World!");
        }
    }
}

using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;

namespace _05_ConfigurationDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddInMemoryCollection(
                new Dictionary<string, string>()
                {
                    {"key1","value1" },
                    {"key2","value2" },
                    {"section1:key3","value3" },
                    {"section2:section3:key4","value4" }
                }
                );

            IConfigurationRoot configurationRoot = configurationBuilder.Build();
            Console.WriteLine($"key1:{configurationRoot["key1"]}");
            Console.WriteLine($"key2:{configurationRoot["key2"]}");
            Console.WriteLine($"key3:{configurationRoot["section1:key3"]}");

            IConfigurationSection section1 = configurationRoot.GetSection("section1");
            Console.WriteLine($"section1_key3:{section1["key3"]}");

            IConfigurationSection section3 = configurationRoot.GetSection("section2:section3");
            Console.WriteLine($"section2_section3_key4:{section3["key4"]}");

            Console.WriteLine("Hello World!");
        }
    }
}

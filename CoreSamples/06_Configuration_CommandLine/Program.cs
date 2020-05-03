using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace _06_Configuration_CommandLine
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddCommandLine(args);

            IConfigurationRoot configurationRoot = configurationBuilder.Build();
            Console.WriteLine($"cmd1:{configurationRoot["cmd1"]}");
            Console.WriteLine($"cmd2:{configurationRoot["cmd2"]}");
            Console.WriteLine($"cmd3:{configurationRoot["cmd3"]}");
            Console.WriteLine($"cmd4:{configurationRoot["cmd4"]}");
            Console.WriteLine($"cmd5:{configurationRoot["cmd5"]}");
            Console.WriteLine($"cmd6_cmd6:{configurationRoot["cmd6:cmd6"]}");

            IConfigurationSection configurationSection = configurationRoot.GetSection("cmd6");
            Console.WriteLine($"cmd6:{configurationSection["cmd6"]}");

            //命令替换
            ////测试时将lanchsettings.json中的注释代码启用
            ////可用于类似命令行的-h代替--help场景
            ////在未定义mapper之前，直接在命令行使用会报错
            ///mapper不能包含重复key
            //var mapper = new Dictionary<string, string>() { { "--k1", "cmd1" }, { "-k2", "cmd2" } };
            //configurationBuilder.AddCommandLine(args, mapper);

            //IConfigurationRoot configurationRoot = configurationBuilder.Build();
            //Console.WriteLine($"--key1:{configurationRoot["--k1"]}");
            //Console.WriteLine($"key1:{configurationRoot["cmd1"]}");
            //Console.WriteLine($"-key2:{configurationRoot["-k2"]}");
            //Console.WriteLine($"key2:{configurationRoot["cmd2"]}");

            Console.WriteLine("Hello World!");
        }
    }
}

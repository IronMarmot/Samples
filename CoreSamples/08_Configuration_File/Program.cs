using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System;

namespace _08_Configuration_File
{
    class Program
    {
        static void Main(string[] args)
        {
            {
                /*IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
                //optional:false是默认值，若不存在则报错
                //reloadOnchange:true是默认值，配置文件变更时，可获取新值
                configurationBuilder.AddJsonFile("settings.json", optional: false, reloadOnChange: true);
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
                Console.WriteLine("Hello World!");*/
            }

            {
                /*//监控程序只能监听配置文件的变化，内存配置、命令行配置、环境变量配置可以通过自定义的监听逻辑实现。
                IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
                configurationBuilder.AddJsonFile("settings.json",optional:false,reloadOnChange:true);

                IConfigurationRoot configurationRoot = configurationBuilder.Build();
                //只能获取一次
                *//*IChangeToken changeToken = configurationRoot.GetReloadToken();

                
                changeToken.RegisterChangeCallback(state =>
                {
                    Console.WriteLine($"key1:{configurationRoot["key1"]}");
                    Console.WriteLine($"key2:{configurationRoot["key2"]}");
                }, configurationRoot);*//*

                //ChangeToken.OnChange()可获取多次
                ChangeToken.OnChange(() => configurationRoot.GetReloadToken(), () =>
                {
                    Console.WriteLine($"key1:{configurationRoot["key1"]}");
                    Console.WriteLine($"key2:{configurationRoot["key2"]}");
                });

                Console.ReadKey();*/
            }
            {
                //强类型绑定
                IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
                configurationBuilder.AddJsonFile("settings.json");

                IConfigurationRoot configurationRoot = configurationBuilder.Build();

                ConfigModel configModel = new ConfigModel() { key1 = "value1new", key2 = 10 ,key4=30};
                //可以进行对此绑定，应该还可以绑定到不同的对象。
                configurationRoot.Bind(configModel);
                //获取节，并绑定
                configurationRoot.GetSection("Section1").Bind(configModel,options=> { options.BindNonPublicProperties = true; });

                Console.WriteLine(configModel.key1);
                Console.WriteLine(configModel.key2);
                Console.WriteLine(configModel.key4);
                Console.WriteLine(configModel.key5);

                Console.ReadLine();
            }
        }
    }

    class ConfigModel
    {
        public String key1 { get; set; }
        public int key2 { get; set; }
        public int key4 { get; set; }
        public bool key5 { get; private set; } = false;
    }
}

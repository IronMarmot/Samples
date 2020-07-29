using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System;

namespace _09_Configuration_Custom
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            //configurationBuilder.Add(new MyConfigurationSource());
            configurationBuilder.AddSource();
            configurationBuilder.AddCustomSource();

            IConfigurationRoot configurationRoot= configurationBuilder.Build();
            ChangeToken.OnChange(()=> configurationRoot.GetReloadToken(), () =>
            {
                Console.WriteLine(configurationRoot["time"]);
            });

            ChangeToken.OnChange(() =>configurationRoot.GetReloadToken(), () =>
            {
                //处理逻辑
            });
            //Console.WriteLine(configurationRoot["time"]);

            Console.ReadKey();
        }
    }
}

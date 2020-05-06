using _10_OptionsDemo.service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _10_OptionsDemo
{
    public static class OrderServiceExtension
    {
        public static IServiceCollection AddOrderService(this IServiceCollection services,IConfiguration configuration)
        {
            services.Configure<OrderOptions>(configuration);

            #region 数据验证的3种方式
            //直接注册验证函数
            //services.AddOptions<OrderOptions>().Configure(options=>
            //{
            //    configuration.Bind(options);
            //}).Validate(options =>
            //{
            //    return options.MaxOrderCount <= 100;
            //}, "MaxOrderCount不能大于100");

            //实现IValidateOptions接口
            /*services.AddOptions<OrderOptions>().Configure(options =>
            {
                configuration.Bind(options);
            }).Services.AddSingleton<IValidateOptions<OrderOptions>, OrderOptionsValidate>();*/

            //使用DataAnnotations
            /*services.AddOptions<OrderOptions>().Configure(options =>
            {
                configuration.Bind(options);
            }).ValidateDataAnnotations();*/
            #endregion

            //作用域类型：每次重新访问都会获取其值
            services.AddScoped<IOrderService, OrderService>();
            //services.AddSingleton<IOrderService, OrderService>();
            services.PostConfigure<OrderOptions>(options => { options.MaxOrderCount += 10; });
            return services;
        }
    }
}

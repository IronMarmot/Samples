using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _10_OptionsDemo.service
{
    public interface IOrderService
    {
        void ShowMaxOrderCount();
    }
    public class OrderService : IOrderService
    {
        #region 服务读取OrderOptions的值
        //OrderOptions orderOptions;
        //public OrderService(OrderOptions orderOptions)
        //{
        //    this.orderOptions = orderOptions;
        //} 
        //public void ShowMaxOrderCount()
        //{
        //    Console.WriteLine($"MaxOrderCount:{orderOptions.MaxOrderCount}");
        //}
        #endregion

        #region 当服务需要从配置中读取值时，如何操作?当然可以在OrderOptions中用配置框架来读取值，但这样的话，服务就依赖配置框架了，因此，我们使用IOptions选项框架
        /*IOptions<OrderOptions> options;
        public OrderService(IOptions<OrderOptions> options)
        {
            this.options = options;
        }

        public void ShowMaxOrderCount()
        {
            Console.WriteLine($"MaxOrderCount:{options.Value.MaxOrderCount}");
            Console.WriteLine($"TotalOrderCount:{options.Value.TotalCount}");
        }*/
        #endregion

        #region 使用配置框架时，如何获取值变更

        #region 如果服务注册为Scoped模式，使用IOptionsSnapshot<>类型
        /*IOptionsSnapshot<OrderOptions> optionsSnapshot;
        public OrderService(IOptionsSnapshot<OrderOptions> optionsSnapshot)
        {
            this.optionsSnapshot = optionsSnapshot;
        }

        public void ShowMaxOrderCount()
        {
            Console.WriteLine($"MaxOrderCount:{optionsSnapshot.Value.MaxOrderCount}");
        }*/
        #endregion

        #region 若服务注册为Singleton模式，则必须使用IOptionsMonitor<>，该接口也可用于Scoped模式
        IOptionsMonitor<OrderOptions> optionsMonitor;
        public OrderService(IOptionsMonitor<OrderOptions> optionsMonitor)
        {
            this.optionsMonitor = optionsMonitor;

            //监听配置，配置改变时触发
            optionsMonitor.OnChange(options =>
            {
                Console.WriteLine($"配置发生了变更，新值为{optionsMonitor.CurrentValue.MaxOrderCount}");
            });

        }

        public void ShowMaxOrderCount()
        {
            Console.WriteLine($"MaxOrderCount:{optionsMonitor.CurrentValue.MaxOrderCount}");
        }
        #endregion

        #endregion
    }

    public class OrderOptions
    {
        [Range(1,20)]
        public int MaxOrderCount { get; set; } = 100;
        public int TotalCount { get; set; } = 500;
    }

    public class OrderOptionsValidate : IValidateOptions<OrderOptions>
    {
        public ValidateOptionsResult Validate(string name, OrderOptions options)
        {
            if (options.MaxOrderCount > 100)
            {
                return ValidateOptionsResult.Fail("MaxOrderCount 不能大于100!!!");
            }
            else
            {
                return ValidateOptionsResult.Success;
            }
        }
    }
}

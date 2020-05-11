using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace _17_RoutingDemo.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        /// <summary>
        /// 自定义约束
        /// </summary>
        /// <param name="id">必须可以转为long</param>
        /// <returns></returns>
        [HttpGet("{id:isLong}")]
        public bool OrderExist([FromRoute]string id)
        {
            return true;
        }

        /// <summary>
        /// max
        /// </summary>
        /// <param name="id">最大20</param>
        /// <param name="linkGenerator"></param>
        /// <returns></returns>
        [HttpGet("{id:max(20)}")]
        public bool Max([FromRoute]long id, [FromServices]LinkGenerator linkGenerator)
        {
            
            var a = linkGenerator.GetPathByAction(HttpContext,
                action: "Reque",
                controller: "Order",
                values: new { name = "abc" });
            Console.WriteLine($"ActionPath:{a}");

            var uri = linkGenerator.GetUriByAction(HttpContext,
                action: "Reque",
                controller: "Order",
                values: new { name = "abc" });
            Console.WriteLine($"ActionUri:{uri}");

            return true;
        }

        /// <summary>
        /// 必填
        /// </summary>
        /// <param name="ss">必填</param>
        /// <returns></returns>
        [HttpGet("{name:required}")]
        [Obsolete]
        public bool Reque(string name)
        {
            return true;
        }


        /// <summary>
        /// 正则验证
        /// </summary>
        /// <param name="number">以三个数字开始</param>
        /// <returns></returns>
        [HttpGet("{number:regex(^\\d{{3}}$)}")]
        [HttpDelete]
        public bool Number(string number)
        {
            return true;
        }
    }
}
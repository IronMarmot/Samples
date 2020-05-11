using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _15_ExceptionDemo.Exceptions
{
    public class MyExceptionFilterAttribute:ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            IKnowException knowException = context.Exception as IKnowException;
            if (knowException == null)
            {
                knowException = KnowException.Unkown;
                context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            }
            else
            {
                knowException = KnowException.FromKnowException(knowException);
                context.HttpContext.Response.StatusCode = StatusCodes.Status200OK;
            }
            context.Result = new JsonResult(knowException) { ContentType = "application/json; charset=utf-8" };
        }
    }
}

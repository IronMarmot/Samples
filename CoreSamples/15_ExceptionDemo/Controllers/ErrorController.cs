using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _15_ExceptionDemo.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace _15_ExceptionDemo.Controllers
{
    public class ErrorController : Controller
    {
        [Route("/error")]
        public IActionResult Index()
        {
            IExceptionHandlerPathFeature exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            Exception ex=exceptionHandlerPathFeature?.Error;

            var knowException = ex as IKnowException;
            if (knowException==null)
            {
                var logger = HttpContext.RequestServices.GetService<ILogger<MyExceptionFilterAttribute>>();
                logger.LogError(ex,ex.Message);
                knowException = KnowException.Unkown;
            }
            else
            {
                knowException = KnowException.FromKnowException(knowException);
            }
            return View(knowException);
        }
    }
}
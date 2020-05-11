using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _17_RoutingDemo.Constraint
{
    public class MyRouteConstraint : IRouteConstraint
    {
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (routeDirection == RouteDirection.IncomingRequest)
            {
                var v = values[routeKey];
                if (long.TryParse(v.ToString(), out var number))
                {
                    return true;
                }
            }
            return false;
        }
    }
}

using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace ToDoList.Filters
{
    public class ProxyAddressFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Request.Headers.TryGetValue("X-Forwarded-For", out var forwardedFor))
                context.HttpContext.Connection.RemoteIpAddress = IPAddress.Parse(forwardedFor);

            base.OnActionExecuting(context);
        }
    }
}

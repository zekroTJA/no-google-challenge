using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using ToDoList.Modules;

namespace ToDoList.Filters
{
    public class RateLimit : ActionFilterAttribute
    {
        private readonly RateLimiterBucket bucket;

        public RateLimit(int burst, int recoverSeconds)
        {
            var reover = TimeSpan.FromSeconds(recoverSeconds);
            bucket = new RateLimiterBucket(burst, reover, (b, r) => new RateLimiter(b, r));
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = context.Controller as ControllerBase;
            if (controller == null)
            {
                base.OnActionExecuting(context);
                return;
            }

            var remoteAddress = context.HttpContext.Connection.RemoteIpAddress.ToString();  
            if (string.IsNullOrEmpty(remoteAddress))
            {
                base.OnActionExecuting(context);
                return;
            }

            var limiter = bucket.Get(remoteAddress);

            if (!limiter.TryReserve())
            {
                context.Result = controller.StatusCode(429, "too many requests");
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}

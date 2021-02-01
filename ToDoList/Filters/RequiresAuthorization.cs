using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Controllers;
using ToDoList.Database;
using ToDoList.Modules;

namespace ToDoList.Filters
{
    public class RequiresAuthorization : ActionFilterAttribute
    {
        private readonly Context db;
        private readonly IAuthorization auth;

        public RequiresAuthorization(Context _db, IAuthorization _auth)
        {
            db = _db;
            auth = _auth;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var controller = context.Controller as AuthorizedController;
            if (controller == null)
                throw new Exception("the controller is not an AuthorizedController");

            if (!context.HttpContext.Request.Cookies.TryGetValue(Constants.SESSION_COOKIE_NAME, out var token))
            {
                context.Result = controller.Unauthorized();
                return;
            }

            try
            {
                controller.AuthClaims = auth.ValidateAuth(token);
                controller.AuthorizedUser = await db.Users.FindAsync(controller.AuthClaims.UserId);
                if (controller.AuthorizedUser == null)
                    throw new Exception("user not found");
            } catch
            {
                context.Result = controller.Unauthorized();
                return;
            }
                

            await next();
        }
    }
}

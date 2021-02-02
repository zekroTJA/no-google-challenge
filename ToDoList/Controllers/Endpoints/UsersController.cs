using Microsoft.AspNetCore.Mvc;
using ToDoList.Filters;
using ToDoList.Models.Responses;

namespace ToDoList.Controllers.Endpoints
{
    /// <summary>
    /// Endpoint controller to get users.
    /// (Currently only to get self user)
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(RequiresAuthorization))]
    public class UsersController : AuthorizedControllerBase
    {
        // -------------------------------------------------------------------------------
        // --- GET /api/users/me

        [HttpGet("me")]
        public ActionResult<UserView> GetMe() =>
            Ok(new UserView(AuthorizedUser));
    }
}

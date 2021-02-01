using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ToDoList.Filters;
using ToDoList.Models.Responses;

namespace ToDoList.Controllers.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(RequiresAuthorization))]
    public class UsersController : AuthorizedController
    {
        // -------------------------------------------------------------------------------
        // --- GET /api/users/me

        [HttpGet("me")]
        public ActionResult<UserView> GetMe() =>
            Ok(new UserView(AuthorizedUser));
    }
}

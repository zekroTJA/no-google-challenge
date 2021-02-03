using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ToDoList.Database;
using ToDoList.Filters;
using ToDoList.Models.Requests;
using ToDoList.Models.Responses;
using ToDoList.Modules;

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
        private readonly Context db;
        private readonly IPasswordHasher hasher;

        public UsersController(Context _db, IPasswordHasher _hasher)
        {
            db = _db;
            hasher = _hasher;
        }

        // -------------------------------------------------------------------------------
        // --- GET /api/users/me

        [HttpGet("me")]
        public ActionResult<UserView> GetMe() =>
            Ok(new UserView(AuthorizedUser));

        // -------------------------------------------------------------------------------
        // --- POST /api/users/me/resetpassword

        [HttpPost("me/resetpassword")]
        public async Task<IActionResult> ResetPassword([FromBody] PasswordReset reset)
        {
            if (!hasher.CompareHashAndPassword(AuthorizedUser.PasswordHash, reset.CurrentPassword))
                return BadRequest("invalid current password");

            AuthorizedUser.PasswordHash = hasher.HashFromPassword(reset.NewPassword);
            db.Update(AuthorizedUser);
            await db.SaveChangesAsync();

            return NoContent();
        }
    }
}

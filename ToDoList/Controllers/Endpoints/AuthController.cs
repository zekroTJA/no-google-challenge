using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Database;
using ToDoList.Models;
using ToDoList.Models.Requests;
using ToDoList.Models.Responses;
using ToDoList.Modules;

namespace ToDoList.Controllers.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly Context db;
        private readonly IPasswordHasher hasher;
        private readonly IAuthorization auth;

        public AuthController(Context _db, IPasswordHasher _hasher, IAuthorization _auth)
        {
            db = _db;
            hasher = _hasher;
            auth = _auth;
        }

        // -------------------------------------------------------------------------------
        // --- POST /api/auth/register

        [HttpPost("[action]")]
        public async Task<ActionResult<UserView>> Register(AuthCredentials creds)
        {
            if (await db.Users.AnyAsync(u => u.LoginName == creds.LoginName))
                return BadRequest("login name already exists");

            var user = new User()
            {
                LoginName = creds.LoginName,
                PasswordHash = hasher.HashFromPassword(creds.Password),
            };

            db.Add(user);
            await db.SaveChangesAsync();

            return Created($"/users/{user.Id}", new UserView(user));
        }

        // -------------------------------------------------------------------------------
        // --- POST /api/auth/login

        [HttpPost("[action]")]
        public async Task<ActionResult<UserView>> Login(AuthCredentials creds)
        {
            var user = await db.Users.Where(u => u.LoginName == creds.LoginName).FirstOrDefaultAsync();
            if (user == null)
                return Unauthorized();

            if (!hasher.CompareHashAndPassword(user.PasswordHash, creds.Password))
                return Unauthorized();

            var sessionJwt = auth.GetAuthToken(new AuthClaims(user.Id));
            var cookieOptions = new CookieOptions()
            {
                HttpOnly = true,
                MaxAge = Constants.SESSION_EXPIRATION,
#if RELEASE
                Secure = true,
#endif
            };
            Response.Cookies.Append(Constants.SESSION_COOKIE_NAME, sessionJwt, cookieOptions);

            return Ok(new UserView(user));
        }
    }
}

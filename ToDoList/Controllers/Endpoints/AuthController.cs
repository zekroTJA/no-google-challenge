using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Database;
using ToDoList.Filters;
using ToDoList.Models;
using ToDoList.Models.Requests;
using ToDoList.Models.Responses;
using ToDoList.Modules;

namespace ToDoList.Controllers.Endpoints
{
    /// <summary>
    /// Endpoint controller for user registration and login.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IContext db;
        private readonly IPasswordHasher hasher;
        private readonly IAuthorization auth;
        private readonly bool useSecureCookies;

        public AuthController(
            IContext _db, 
            IPasswordHasher _hasher, 
            IAuthorization _auth,
            ILogger<AuthController> _logger,
            IConfiguration _config)
        {
            db = _db;
            hasher = _hasher;
            auth = _auth;
            useSecureCookies = _config.GetValue("Authorization:UseSecureCookies", true);

            if (!useSecureCookies)
                _logger.LogWarning("Secure cookies are disabled! Please do not disable secure cookies in production!");
        }

        // -------------------------------------------------------------------------------
        // --- POST /api/auth/register

        [HttpPost("[action]")]
        [RateLimit(3, 60)]
        public async Task<ActionResult<UserView>> Register([FromBody] AuthCredentials creds)
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

            return Created($"/api/users/{user.Id}", new UserView(user));
        }

        // -------------------------------------------------------------------------------
        // --- POST /api/auth/login

        [HttpPost("[action]")]
        [RateLimit(3, 60)]
        public async Task<ActionResult<UserView>> Login([FromBody] AuthCredentials creds)
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
                Secure = useSecureCookies,
#endif
            };
            Response.Cookies.Append(Constants.SESSION_COOKIE_NAME, sessionJwt, cookieOptions);

            return Ok(new UserView(user));
        }

        // -------------------------------------------------------------------------------
        // --- POST /api/auth/logout

        [HttpPost("[action]")]
        public IActionResult Logout()
        {
            var cookieOptions = new CookieOptions()
            {
                HttpOnly = true,
                MaxAge = TimeSpan.Zero,
#if RELEASE
                Secure = true,
#endif
            };
            Response.Cookies.Append(Constants.SESSION_COOKIE_NAME, "", cookieOptions);

            return NoContent();
        }
    }
}

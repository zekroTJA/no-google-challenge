using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Database;
using ToDoList.Models;
using ToDoList.Models.Requests;
using ToDoList.Models.Responses;
using ToDoList.Modules;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly Context db;
        private readonly IPasswordHasher hasher;

        public AuthController(Context _db, IPasswordHasher _hasher)
        {
            db = _db;
            hasher = _hasher;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(AuthRegister register)
        {
            // all login names are lowercase
            register.LoginName = register.LoginName.ToLower();

            if (await db.Users.AnyAsync(u => u.LoginName == register.LoginName))
                return BadRequest("login name already exists");

            var user = new User()
            {
                LoginName = register.LoginName,
                PasswordHash = hasher.HashFromPassword(register.Password),
            };

            db.Add(user);
            await db.SaveChangesAsync();

            return Created($"/users/{user.Id}", new UserView(user));
        }
    }
}

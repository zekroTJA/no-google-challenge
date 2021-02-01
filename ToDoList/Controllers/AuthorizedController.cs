using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using ToDoList.Modules;

namespace ToDoList.Controllers
{
    public class AuthorizedController : ControllerBase
    {
        public AuthClaims AuthClaims { get; set; }
        public User AuthorizedUser { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using ToDoList.Modules;

namespace ToDoList.Controllers
{
    /// <summary>
    /// Conroller base which contains an instance of the <see cref="AuthClaims"/>
    /// and <see cref="User"/> instance of the currently authorized user.
    /// These properties are set by assigned request filter attributes.
    /// </summary>
    public class AuthorizedControllerBase : ControllerBase
    {
        /// <summary>
        /// The auth claims of the currently authenticated user.
        /// </summary>
        public AuthClaims AuthClaims { get; set; }

        /// <summary>
        /// The instance of the currently authenticated user
        /// fetched from the database on authentication.
        /// </summary>
        public User AuthorizedUser { get; set; }
    }
}

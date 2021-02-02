using System.Text.Json.Serialization;

namespace ToDoList.Models
{
    /// <summary>
    /// Database model of a user.
    /// </summary>
    public class User : Entity
    {
        public string LoginName { get; set; }

        [JsonIgnore]
        public string PasswordHash { get; set; }
    }
}

using System.Text.Json.Serialization;

namespace ToDoList.Models
{
    public class User : Entity
    {
        public string LoginName { get; set; }

        [JsonIgnore]
        public string PasswordHash { get; set; }
    }
}

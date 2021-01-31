using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ToDoList.Models.Requests
{
    public class AuthRegister
    {
        [JsonPropertyName("login_name")]
        [Required]
        public string LoginName { get; set; }

        [JsonPropertyName("password")]
        [Required]
        public string Password { get; set; }
    }
}

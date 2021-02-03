using System.Text.Json.Serialization;

namespace ToDoList.Models.Requests
{
    public class PasswordReset
    {
        [JsonPropertyName("current_password")]
        public string CurrentPassword { get; set; }

        [JsonPropertyName("new_password")]
        public string NewPassword { get; set; }
    }
}

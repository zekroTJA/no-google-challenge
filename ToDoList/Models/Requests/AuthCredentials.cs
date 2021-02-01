using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ToDoList.Models.Requests
{
    public class AuthCredentials
    {
        [JsonPropertyName("login_name")]
        [Required]
        public string LoginName 
        { 
            get => loginName; 
            set => loginName = value.ToLower(); 
        }

        [JsonPropertyName("password")]
        [Required]
        public string Password { get; set; }

        private string loginName;
    }
}

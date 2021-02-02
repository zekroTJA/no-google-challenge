using System.Text.Json.Serialization;

namespace ToDoList.Models.Responses
{
    /// <summary>
    /// Response view model for a user.
    /// </summary>
    public class UserView : Entity
    {
        [JsonPropertyName("login_name")]
        public string LoginName { get; set; }

        public UserView() : base()
        { }

        public UserView(User user) : base(user)
        {
            LoginName = user.LoginName;
        }
    }
}

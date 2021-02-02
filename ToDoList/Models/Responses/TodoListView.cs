using System.Text.Json.Serialization;

namespace ToDoList.Models.Responses
{
    /// <summary>
    /// Response view model for a todo list.
    /// </summary>
    public class TodoListView : Entity
    {
        [JsonPropertyName("owner")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public UserView? Owner { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        public TodoListView() : base()
        { }

        public TodoListView(TodoList list, bool displayUser = true) : base(list)
        {
            Owner = displayUser ? new UserView(list.Owner) : null;
            Name = list.Name;
        }
    }
}

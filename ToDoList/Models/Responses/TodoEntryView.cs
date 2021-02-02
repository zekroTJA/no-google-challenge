using System.Text.Json.Serialization;

namespace ToDoList.Models.Responses
{
    /// <summary>
    /// Response view model of a todo list entry.
    /// </summary>
    public class TodoEntryView : Entity
    {
        [JsonPropertyName("contained_in")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public TodoListView? ContainedIn { get; set; }

        [JsonPropertyName("content")]
        public string Content { get; set; }

        [JsonPropertyName("checked")]
        public bool Checked { get; set; }

        public TodoEntryView() : base()
        { }

        public TodoEntryView(
            TodoEntry entry, 
            bool displayContainedList = true,
            bool displayContainedListOwner = true) : base(entry)
        {
            ContainedIn = displayContainedList
                ? new TodoListView(entry.ContainedIn, displayContainedListOwner)
                : null;
            Content = entry.Content;
            Checked = entry.Checked;
        }
    }
}

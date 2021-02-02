using System.Text.Json.Serialization;

namespace ToDoList.Models.Requests
{
    /// <summary>
    /// Request model to create or update a todo list entry.
    /// </summary>
    public class ListEntryCreate
    {
        [JsonPropertyName("content")]
        public string Content { get; set; }

        [JsonPropertyName("checked")]
        public bool? Checked { get; set; }
    }
}

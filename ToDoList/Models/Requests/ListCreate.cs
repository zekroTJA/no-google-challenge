using System.Text.Json.Serialization;

namespace ToDoList.Models.Requests
{
    /// <summary>
    /// Request model to create or update a todo list.
    /// </summary>
    public class ListCreate
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}

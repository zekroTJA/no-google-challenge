using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ToDoList.Models.Requests
{
    public class ListEntryCreate
    {
        [JsonPropertyName("content")]
        public string Content { get; set; }

        [JsonPropertyName("checked")]
        public bool? Checked { get; set; }
    }
}

namespace ToDoList.Models
{
    /// <summary>
    /// Database model of a todo list entry.
    /// </summary>
    public class TodoEntry : Entity
    {
        public TodoList ContainedIn { get; set; }
        public string Content { get; set; }
        public bool Checked { get; set; }
    }
}

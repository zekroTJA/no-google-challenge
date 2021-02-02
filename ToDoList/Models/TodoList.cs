namespace ToDoList.Models
{
    /// <summary>
    /// Database model of a todo list.
    /// </summary>
    public class TodoList : Entity
    {
        public User Owner { get; set; }
        public string Name { get; set; }
    }
}

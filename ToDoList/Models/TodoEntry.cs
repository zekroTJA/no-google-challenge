namespace ToDoList.Models
{
    public class TodoEntry : Entity
    {
        public TodoList ContainedIn { get; set; }
        public string Content { get; set; }
        public bool Checked { get; set; }
    }
}

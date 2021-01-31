namespace ToDoList.Models
{
    public class TodoList : Entity
    {
        public User Owner { get; set; }
        public string Name { get; set; }
    }
}

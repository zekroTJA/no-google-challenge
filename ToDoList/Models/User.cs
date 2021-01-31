namespace ToDoList.Models
{
    public class User : Entity
    {
        public string LoginName { get; set; }
        public string PasswordHash { get; set; }
    }
}

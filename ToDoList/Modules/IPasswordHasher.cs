namespace ToDoList.Modules
{
    public interface IPasswordHasher
    {
        string HashFromPassword(string password);
        bool CompareHashAndPassword(string hash, string password);
    }
}

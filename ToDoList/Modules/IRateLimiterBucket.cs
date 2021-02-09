namespace ToDoList.Modules
{
    public interface IRateLimiterBucket
    {
        IRateLimiter Get(string ident);
    }
}
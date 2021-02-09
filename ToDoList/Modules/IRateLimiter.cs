using System;

namespace ToDoList.Modules
{
    public interface IRateLimiter
    {
        int Burst { get; }
        DateTime LastAccess { get; }
        TimeSpan Recover { get; }
        int Tokens { get; }

        bool TryReserve();
    }
}
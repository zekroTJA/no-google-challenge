using System;

namespace ToDoList
{
    public static class Constants
    {
        public static readonly TimeSpan SESSION_EXPIRATION = TimeSpan.FromDays(7);

        public const string SESSION_COOKIE_NAME = "todolist.session";
    }
}

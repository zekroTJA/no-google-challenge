using Microsoft.EntityFrameworkCore;
using ToDoList.Models;

namespace ToDoList.Database
{
    public class Context : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<TodoList> Lists { get; set; }
        public DbSet<TodoEntry> Entries { get; set; }

        public Context(DbContextOptions options) : base(options)
        { }
    }
}

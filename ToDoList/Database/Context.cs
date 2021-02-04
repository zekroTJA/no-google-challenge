using Microsoft.EntityFrameworkCore;
using ToDoList.Models;

namespace ToDoList.Database
{
    /// <summary>
    /// Database context used to access the database
    /// sets via the entity framework bindings.
    /// </summary>
    public class Context : DbContext, IContext
    {
        /// <summary>
        /// Users database collection/table.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Todo Lists database collection/table.
        /// </summary>
        public DbSet<TodoList> Lists { get; set; }

        /// <summary>
        /// Todo List Entries database collection/table.
        /// </summary>
        public DbSet<TodoEntry> Entries { get; set; }

        public Context(DbContextOptions options) : base(options)
        { }
    }
}

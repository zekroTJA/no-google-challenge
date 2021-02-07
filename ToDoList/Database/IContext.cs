using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using ToDoList.Models;

namespace ToDoList.Database
{
    public interface IContext : IDisposable
    {
        DbSet<TodoEntry> Entries { get; set; }
        DbSet<TodoList> Lists { get; set; }
        DbSet<User> Users { get; set; }

        DatabaseFacade Database { get; }

        EntityEntry Add([NotNull] object entity);
        EntityEntry Remove([NotNull] object entity);
        void RemoveRange([NotNullAttribute] IEnumerable<object> entities);
        DbSet<TEntity> Set<TEntity>([NotNull] string name) where TEntity : class;
        EntityEntry<TEntity> Update<TEntity>([NotNull] TEntity entity) where TEntity : class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
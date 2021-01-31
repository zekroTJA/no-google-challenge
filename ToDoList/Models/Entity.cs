using System;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    public class Entity
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime Created { get; set; }

        public Entity()
        {
            Id = Guid.NewGuid();
            Created = DateTime.Now;
        }
    }
}

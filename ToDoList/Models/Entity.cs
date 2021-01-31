using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ToDoList.Models
{
    public class Entity
    {
        [Key]
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("created")]
        public DateTime Created { get; set; }

        public Entity()
        {
            Id = Guid.NewGuid();
            Created = DateTime.Now;
        }

        public Entity(Entity entity)
        {
            Id = entity.Id;
            Created = entity.Created;
        }
    }
}

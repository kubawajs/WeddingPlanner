using System;
using System.ComponentModel.DataAnnotations;

namespace WeddingPlanner.Core.Domain.Abstractions
{
    public class BaseModel
    {
        [Key]
        public int Id { get; set; }

        public User CreatedBy { get; }
        public User UpdatedBy { get; set; }

        public DateTime CreatedAt { get; }
        public DateTime UpdatedAt { get; set; }

        public BaseModel()
        {
            var now = DateTime.Now;
            CreatedAt = now;
            UpdatedAt = now;
        }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using WeddingPlanner.Core.Domain.Abstractions;

namespace WeddingPlanner.Core.Domain
{
    public class Guest : BaseModel
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        public bool IsChild { get; set; }
        public bool HasPair { get; set; }
        public int Age { get; set; }
    }
}

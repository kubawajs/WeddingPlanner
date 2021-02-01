using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingPlanner.Core.Domain.Abstractions
{
    public class BaseModel
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("CreatedByUsername")]
        public User CreatedBy { get; set; }
        public string CreatedByUsername { get; set; }

        [ForeignKey("UpdatedByUsername")]
        public User UpdatedBy { get; set; }
        public string UpdatedByUsername { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }

        public BaseModel()
        {
            var now = DateTime.Now;
            CreatedOn = now;
            UpdatedOn = now;
        }
    }
}
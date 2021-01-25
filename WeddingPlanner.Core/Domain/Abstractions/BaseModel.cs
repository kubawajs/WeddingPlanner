using System.ComponentModel.DataAnnotations;

namespace WeddingPlanner.Core.Domain.Abstractions
{
    public class BaseModel
    {
        [Key]
        public int Id { get; set; }
    }
}
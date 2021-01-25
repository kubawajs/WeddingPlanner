using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WeddingPlanner.Core.Domain.Abstractions;

namespace WeddingPlanner.Core.Domain
{
    public class CostDescription : BaseModel
    {
        [Required]
        [MaxLength(100)]
        public string Label { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public decimal Price { get; set; }
        public CostType Type { get; set; }
    }
}
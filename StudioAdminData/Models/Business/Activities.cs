using StudioData.Models.Abstract;
using System.ComponentModel.DataAnnotations;

namespace StudioData.Models.Business
{
    public class Activities : BaseEntity
    {
        [Required]
        public int Quantity { get; set; } = 0;
        [Required]
        public Roles Roles { get; set; } = 0;
        [Required]
        public decimal Value { get; set; } = 0.00M;

    }
}

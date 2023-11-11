using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StudioData.Models.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudioData.Models.Business
{
    public class AvailableDay : BaseEntity
    {
        [Required]
        public DateTime Date { get; set; }
    }
}

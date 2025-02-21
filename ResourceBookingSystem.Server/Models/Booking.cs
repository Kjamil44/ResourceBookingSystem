using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ResourceBookingSystem.Server.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ResourceId { get; set; } // Foreign key

        [ForeignKey("ResourceId")]
        public Resource Resource { get; set; } // Navigation property

        [Required]
        public DateTime DateFrom { get; set; }

        [Required]
        public DateTime DateTo { get; set; }

        [Required]
        public int BookedQuantity { get; set; }
    }
}

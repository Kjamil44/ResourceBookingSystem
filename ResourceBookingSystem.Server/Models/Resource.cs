using System.ComponentModel.DataAnnotations;

namespace ResourceBookingSystem.Server.Models
{
    public class Resource
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Quantity { get; set; }

        public List<Booking> Bookings { get; set; } = new List<Booking>();
    }
}

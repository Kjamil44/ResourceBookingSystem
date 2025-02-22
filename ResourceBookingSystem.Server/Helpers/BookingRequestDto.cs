namespace ResourceBookingSystem.Server.Helpers
{
    public class BookingRequestDto
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int BookedQuantity { get; set; }
    }
}

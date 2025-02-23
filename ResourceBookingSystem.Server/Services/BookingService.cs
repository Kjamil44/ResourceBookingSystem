using ResourceBookingSystem.Server.Helpers;
using ResourceBookingSystem.Server.Models;
using ResourceBookingSystem.Server.Repository;
using ResourceBookingSystem.Server.Services.Email;

namespace ResourceBookingSystem.Server.Services
{
    public class BookingService : IBookingService
    {
        private readonly IResourceBookingRepository _resourceBookingRepository;
        private readonly IEmailService _emailService;

        public BookingService(IResourceBookingRepository resourceBookingRepository, IEmailService emailService)
        {
            _resourceBookingRepository = resourceBookingRepository;
            _emailService = emailService;
        }

        public async Task<IEnumerable<Resource>> ListAllResources()
        {
            return await _resourceBookingRepository.GetResources();
        }

        public async Task BookResource(int resourceId, BookingRequestDto bookingRequest)
        {
            var newBooking = new Booking
            {
                ResourceId = resourceId,
                DateFrom = bookingRequest.DateFrom,
                DateTo = bookingRequest.DateTo,
                BookedQuantity = bookingRequest.BookedQuantity
            };

            var bookingId = await _resourceBookingRepository.AddBooking(newBooking);
            _emailService.SendEmail(bookingId);
        }

        public async Task<bool> IsBookingAvailable(int resourceId, DateTime dateFrom, DateTime dateTo, int requestedQuantity)
        {
            var existingBookings = await _resourceBookingRepository.GetBookingsForResource(resourceId, dateFrom, dateTo);

            int totalBooked = existingBookings.Sum(b => b.BookedQuantity);

            int totalAvailable = await _resourceBookingRepository.GetResourceTotalQuantity(resourceId);

            return (totalAvailable - totalBooked) >= requestedQuantity;
        }
    }
}

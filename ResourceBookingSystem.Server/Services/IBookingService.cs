using ResourceBookingSystem.Server.Helpers;
using ResourceBookingSystem.Server.Models;

namespace ResourceBookingSystem.Server.Services
{
    public interface IBookingService
    {
        public Task<IEnumerable<Resource>> ListAllResources();
        public Task BookResource(int resourceId, BookingRequestDto bookingRequest);
        public Task<bool> IsBookingAvailable(int resourceId, DateTime dateFrom, DateTime dateTo, int bookedQuantity);
    }
}

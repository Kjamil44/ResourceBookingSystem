using ResourceBookingSystem.Server.Models;

namespace ResourceBookingSystem.Server.Repository
{
    public interface IResourceBookingRepository
    {
        public Task<IEnumerable<Resource>> GetResources();
        public Task<IEnumerable<Booking>> GetBookingsForResource(int resourceId, DateTime dateFrom, DateTime dateTo);
        public Task<int> GetResourceTotalQuantity(int resourceId);
        public Task<int> AddBooking(Booking booking);
    }
}

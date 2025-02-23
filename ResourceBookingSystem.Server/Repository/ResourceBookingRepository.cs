using Microsoft.EntityFrameworkCore;
using ResourceBookingSystem.Server.Models;

namespace ResourceBookingSystem.Server.Repository
{
    public class ResourceBookingRepository : IResourceBookingRepository
    {
        private readonly ResourceBookingContext _context;

        public ResourceBookingRepository()
        {
            _context = new ResourceBookingContext();
        }

        public async Task<IEnumerable<Resource>> GetResources()
        {
            var resources = await _context.Resources
                .OrderBy(x => x.Id)
                .ToListAsync();

            return resources;
        }

        public async Task<int> AddBooking(Booking booking)
        {
            _context.Add(booking);
            await _context.SaveChangesAsync();

            return booking.Id;
        }

        public async Task<IEnumerable<Booking>> GetBookingsForResource(int resourceId, DateTime dateFrom, DateTime dateTo)
        {
            var bookingsForResource = await _context.Bookings
                .Where(b => b.ResourceId == resourceId &&
                       b.DateFrom < dateTo &&
                       b.DateTo > dateFrom)
                .ToListAsync();

            return bookingsForResource;
        }

        public async Task<int> GetResourceTotalQuantity(int resourceId)
        {
            var resource = await _context.Resources.FirstOrDefaultAsync(x => x.Id == resourceId);
            if (resource == null)
            {
                throw new Exception("Resource does not exist");
            }

            return resource.Quantity;
        }
    }
}

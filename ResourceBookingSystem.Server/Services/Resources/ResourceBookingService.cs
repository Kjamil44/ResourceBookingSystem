using Microsoft.EntityFrameworkCore;
using ResourceBookingSystem.Server.Helpers;
using ResourceBookingSystem.Server.Models;

namespace ResourceBookingSystem.Server.Services.Resources
{
    public class ResourceBookingService : IResourceBookingService
    {
        private readonly ResourceBookingContext _context;

        public ResourceBookingService()
        {
            _context = new ResourceBookingContext();
        }

        public async Task<IEnumerable<Models.Resource>> ListAllResources()
        {
            var resources = await _context.Resources
                .OrderBy(x => x.Id)
                .ToListAsync();

            return resources;
        }

        public async Task CreateBookingOnResource(int resourceId, BookingRequestDto bookingRequest)
        {
            var resource = await _context.Resources
                .FirstOrDefaultAsync(x => x.Id == resourceId);

            if (resource == null)
                throw new Exception("No Resource Available.");

            var booking = new Booking
            {
                Resource = resource,
                DateFrom = bookingRequest.DateFrom,
                DateTo = bookingRequest.DateTo,
                BookedQuantity = bookingRequest.BookedQuantity,
            };

            //Checks for Date

            _context.Add(booking);
            await _context.SaveChangesAsync();

        }
    }
}

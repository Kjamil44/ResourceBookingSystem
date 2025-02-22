using ResourceBookingSystem.Server.Helpers;

namespace ResourceBookingSystem.Server.Services.Resources
{
    public interface IResourceBookingService
    {
        public Task<IEnumerable<Models.Resource>> ListAllResources();
        public Task CreateBookingOnResource(int resourceId, BookingRequestDto bookingRequest);
    }
}

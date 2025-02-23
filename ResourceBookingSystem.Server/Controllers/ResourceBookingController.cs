using Microsoft.AspNetCore.Mvc;
using ResourceBookingSystem.Server.Helpers;
using ResourceBookingSystem.Server.Services;

namespace ResourceBookingSystem.Server.Controllers
{
    [ApiController]
    [Route("api/resources")]
    public class ResourceBookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public ResourceBookingController(IBookingService bookingService) => _bookingService = bookingService;

        [HttpGet]
        public async Task<IActionResult> GetAllResources()
        {
            var resources = await _bookingService.ListAllResources();
            return Ok(resources);
        }

        [HttpPost("{resourceId}/bookings")]
        public async Task<IActionResult> BookResource([FromRoute] int resourceId, [FromBody] BookingRequestDto bookingRequest)
        {
            var (IsValid, Message) = await _bookingService.ValidateBookingRequest(resourceId, bookingRequest);
            if (!IsValid)
            {
                return BadRequest(new { message = Message });
            }

            await _bookingService.BookResource(resourceId, bookingRequest);

            return Ok(new { message = "Booking successful!" });
        }
    }
}

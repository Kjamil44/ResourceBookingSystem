using Microsoft.AspNetCore.Mvc;
using ResourceBookingSystem.Server.Helpers;
using ResourceBookingSystem.Server.Services;

namespace ResourceBookingSystem.Server.Controllers
{
    [ApiController]
    [Route("api/resources")]
    public class ResourceBookingController : ControllerBase
    {
        private readonly ILogger<ResourceBookingController> _logger;
        private readonly IBookingService _bookingService;

        public ResourceBookingController(ILogger<ResourceBookingController> logger, IBookingService bookingService)
        {
            _logger = logger;
            _bookingService = bookingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllResources()
        {
            var resources = await _bookingService.ListAllResources();
            return Ok(resources);
        }

        [HttpPost("{resourceId}/bookings")]
        public async Task<IActionResult> BookResource([FromRoute] int resourceId, [FromBody] BookingRequestDto bookingRequest)
        {
            if (bookingRequest.DateFrom > bookingRequest.DateTo)
            {
                return BadRequest(new { message = "The start date cannot be later than the end date. Please select a valid date range." });
            }

            var isAvailable = await _bookingService.IsBookingAvailable(resourceId, bookingRequest.DateFrom, bookingRequest.DateTo, bookingRequest.BookedQuantity);
            if (!isAvailable)
            {
                return BadRequest(new { message = "Resource not available for the selected time range." });
            }

            await _bookingService.BookResource(resourceId, bookingRequest);
            return Ok(new { message = "Booking successful!" });
        }
    }
}

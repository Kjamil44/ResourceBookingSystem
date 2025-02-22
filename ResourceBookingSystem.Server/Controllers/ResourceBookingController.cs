using Microsoft.AspNetCore.Mvc;
using ResourceBookingSystem.Server.Helpers;
using ResourceBookingSystem.Server.Models;
using ResourceBookingSystem.Server.Services.Resources;
using System.ComponentModel.Design;

namespace ResourceBookingSystem.Server.Controllers
{
    [ApiController]
    [Route("api/resources")]
    public class ResourceBookingController : ControllerBase
    {
        private readonly ILogger<ResourceBookingController> _logger;
        private readonly IResourceBookingService _resourceBookingService;

        public ResourceBookingController(ILogger<ResourceBookingController> logger, IResourceBookingService resourceBookingService)
        {
            _logger = logger;
            _resourceBookingService = resourceBookingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllResources()
        {
            var resources = await _resourceBookingService.ListAllResources();
            return Ok(resources);
        }


        [HttpPost("{resourceId}/bookings")]
        public async Task<IActionResult> BookAResource([FromRoute] int resourceId, [FromBody] BookingRequestDto bookingRequest)
        {
            var resources = await _resourceBookingService.ListAll();
            return Ok(resources);
        }
    }
}

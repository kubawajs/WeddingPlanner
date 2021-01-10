using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using WeddingPlanner.Infrastructure.Dto;
using WeddingPlanner.Infrastructure.Models;
using WeddingPlanner.Infrastructure.Services.Abstractions;

namespace WeddingPlanner.Api.Controllers
{
    [ApiController]
    [Route("api/guests")]
    public class GuestsController : Controller
    {
        private readonly ILogger<GuestsController> _logger;
        private readonly IGuestService _guestService;

        public GuestsController(ILogger<GuestsController> logger, IGuestService guestService)
        {
            _logger = logger;
            _guestService = guestService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(GuestListResponse), 200)]
        [ProducesResponseType(typeof(GuestListResponse), 400)]
        public async Task<IActionResult> Index([FromQuery] int? age = null)
        {
            try
            {
                GuestListResponse response;
                if(age == null)
                {
                    response = await _guestService.GetGuestsAsync();
                }
                else if(age.HasValue && age.Value >= 0)
                {
                    response = await _guestService.GetGuestsByAgeAsync(age.Value);
                }
                else
                {
                    _logger.LogInformation($"Cannot retrieve guest list - wrong ageParam value: {age}");
                    return BadRequest();
                }

                _logger.LogInformation("Guests list successfully retrieved.");
                return Ok(response);
            }
            catch(Exception ex)
            {
                _logger.LogError($"An error occured during retrieving guests list: {ex.Message}", ex);
                return BadRequest();
            }
        }

        [HttpPost("create")]
        [ProducesResponseType(typeof(GuestDto), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] GuestDto guest)
        {
            if(!ModelState.IsValid)
            {
                _logger.LogWarning("Guest model is not valid.");
                return BadRequest();
            }

            try
            {
                await _guestService.CreateGuestAsync(guest);
                _logger.LogInformation($"Guest successfully created: {guest.Id}");
                return Ok(guest);
            }
            catch(Exception ex)
            {
                _logger.LogError($"An error occured during guest creation: {ex.Message}", ex);
                return BadRequest();
            }
        }
    }
}

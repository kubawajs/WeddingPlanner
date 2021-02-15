using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using WeddingPlanner.Api.Models;
using WeddingPlanner.Api.Services.Abstractions;
using WeddingPlanner.Infrastructure.Dto;

namespace WeddingPlanner.Api.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
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
            GuestListResponse response;
            if (age == null)
            {
                response = await _guestService.GetGuestsAsync();
            }
            else
            {
                response = await _guestService.GetGuestsByAgeAsync(age.Value);
            }
            
            if(!response.Result)
            {
                _logger.LogInformation($"An error occured during retrieving guests list.");
                return BadRequest(response);
            }

            _logger.LogInformation("Guests list successfully retrieved.");
            return new OkObjectResult(response);
        }

        [HttpPost("create")]
        [ProducesResponseType(typeof(GuestResponse), 200)]
        [ProducesResponseType(typeof(GuestResponse), 400)]
        public async Task<IActionResult> Create([FromBody] GuestDto model)
        {
            if(!ModelState.IsValid)
            {
                _logger.LogWarning("Guest model is not valid.");
                return BadRequest();
            }

            var response = await _guestService.CreateAsync(model);
            if(!response.Result)
            {
                _logger.LogError($"An error occured during guest creation.");
                return BadRequest(response);
            }

            _logger.LogInformation($"Guest successfully created: {response.Item.Id}");
            return new OkObjectResult(response);
        }
    }
}
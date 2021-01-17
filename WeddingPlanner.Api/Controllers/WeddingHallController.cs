using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using WeddingPlanner.Infrastructure.Dto;
using WeddingPlanner.Infrastructure.Models;
using WeddingPlanner.Infrastructure.Services.Abstractions;

namespace WeddingPlanner.Api.Controllers
{
    [ApiController]
    [Route("api/weddinghall")]
    public class WeddingHallController : Controller
    {
        private readonly ILogger<WeddingHallController> _logger;
        private readonly IWeddingHallService _weddingHallService;

        public WeddingHallController(ILogger<WeddingHallController> logger, IWeddingHallService weddingHallService)
        {
            _logger = logger;
            _weddingHallService = weddingHallService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(WeddingHallSummaryResponse), 200)]
        [ProducesResponseType(typeof(WeddingHallSummaryResponse), 400)]
        public async Task<IActionResult> Index(int id)
        {
            var response = await _weddingHallService.GetAsync(id);
            if (!response.Result)
            {
                _logger.LogError($"An error occured during retrieving Wedding hall summary.");
                return BadRequest(response);
            }

            _logger.LogInformation("Wedding hall summary successfully retrieved.");
            return new OkObjectResult(response);
        }

        [HttpPost("create")]
        [ProducesResponseType(typeof(WeddingHallSummaryResponse), 200)]
        [ProducesResponseType(typeof(WeddingHallSummaryResponse), 400)]
        public async Task<IActionResult> Create([FromBody] WeddingHallDto model)
        {
            if(!ModelState.IsValid)
            {
                _logger.LogWarning("WeddingHallSummary model is not valid.");
                return BadRequest();
            }

            var response = await _weddingHallService.CreateAsync(model);
            if(!response.Result)
            {
                _logger.LogError($"An error occured during WeddingHall creation.");
                return BadRequest(response);
            }

            _logger.LogInformation($"Wedding hall summary successfully created: {model.Id}");
            return new OkObjectResult(response);
        }

        [HttpPut("edit")]
        [ProducesResponseType(typeof(WeddingHallSummaryResponse), 200)]
        [ProducesResponseType(typeof(WeddingHallSummaryResponse), 400)]
        public async Task<IActionResult> Edit([FromBody] WeddingHallDto model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("WeddingHall model is not valid.");
                return BadRequest();
            }

            var response = await _weddingHallService.UpdateAsync(model);
            if (!response.Result)
            {
                _logger.LogError($"An error occured during WeddingHall creation.");
                return BadRequest(response);
            }

            _logger.LogInformation($"Wedding hall summary successfully created: {model.Id}");
            return new OkObjectResult(response);
        }
    }
}

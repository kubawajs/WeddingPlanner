using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using WeddingPlanner.Api.Services.Abstractions;
using WeddingPlanner.Infrastructure.Dto;
using WeddingPlanner.Infrastructure.Models;

namespace WeddingPlanner.Api.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    [Route("api/outfits")]
    public class OutfitsController : Controller
    {
        private readonly ILogger<OutfitsController> _logger;
        private readonly IBaseService<OutfitDto, OutfitResponse<OutfitDto>> _outfitService;

        public OutfitsController(
            ILogger<OutfitsController> logger,
            IBaseService<OutfitDto, OutfitResponse<OutfitDto>> outfitService)
        {
            _logger = logger;
            _outfitService = outfitService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(OutfitResponse<OutfitDto>), 200)]
        [ProducesResponseType(typeof(OutfitResponse<OutfitDto>), 400)]
        public async Task<IActionResult> Index([FromQuery] int id)
        {
            var response = await _outfitService.GetAsync(id);
            if (!response.Result)
            {
                _logger.LogError($"An error occured during retrieving outfit list.");
                return BadRequest(response);
            }

            _logger.LogInformation("Outfit successfully retrieved.");
            return new OkObjectResult(response);
        }

        [HttpPost("create")]
        [ProducesResponseType(typeof(OutfitResponse<OutfitDto>), 200)]
        [ProducesResponseType(typeof(OutfitResponse<OutfitDto>), 400)]
        public async Task<IActionResult> Create([FromBody] OutfitDto model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Outfit model is not valid.");
                return BadRequest();
            }

            var response = await _outfitService.CreateAsync(model);
            if (!response.Result)
            {
                _logger.LogError($"An error occured during outfit creation.");
                return BadRequest(response);
            }

            _logger.LogInformation($"Outfit successfully created: {response.Item.Id}");
            return new OkObjectResult(response);
        }

        [HttpPut("edit/{id}")]
        [ProducesResponseType(typeof(OutfitResponse<OutfitDto>), 200)]
        [ProducesResponseType(typeof(OutfitResponse<OutfitDto>), 400)]
        public async Task<IActionResult> Edit([FromBody] OutfitDto model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Woman outfit model is not valid.");
                return BadRequest();
            }

            var response = await _outfitService.UpdateAsync(model);
            if (!response.Result)
            {
                _logger.LogError($"An error occured during outfit edit.");
                return BadRequest(response);
            }

            _logger.LogInformation($"Outfit successfully edited: {response.Item.Id}");
            return new OkObjectResult(response);
        }
    }
}

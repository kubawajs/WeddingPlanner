using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using WeddingPlanner.Core.Domain.Abstractions;
using WeddingPlanner.Infrastructure.Dto;
using WeddingPlanner.Infrastructure.Dto.Abstractions;
using WeddingPlanner.Infrastructure.Models;
using WeddingPlanner.Infrastructure.Services.Abstractions;

namespace WeddingPlanner.Api.Controllers
{
    [ApiController]
    [Route("api/outfits")]
    public class OutfitsController : Controller
    {
        private readonly ILogger<OutfitsController> _logger;
        private readonly IOutfitService<ManOutfitDto> _manOutfitService;
        private readonly IOutfitService<WomanOutfitDto> _womanOutfitService;

        public OutfitsController(
            ILogger<OutfitsController> logger,
            IOutfitService<ManOutfitDto> manOutfitService,
            IOutfitService<WomanOutfitDto> womanOutfitService)
        {
            _logger = logger;
            _manOutfitService = manOutfitService;
            _womanOutfitService = womanOutfitService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(OutfitResponse<BaseOutfitDto>), 200)]
        [ProducesResponseType(typeof(OutfitResponse<BaseOutfitDto>), 400)]
        public async Task<IActionResult> Index([FromQuery] string type, int id)
        {
            var parseResult = Enum.TryParse(type, out OutfitType outfitType);
            if (!parseResult)
            {
                _logger.LogError($"Invalid parameter type: {type}");
                return BadRequest($"Invalid parameter type: {type}");
            }

            if(outfitType == OutfitType.Man)
            {
                var response = await _manOutfitService.GetOutfitAsync(id);
                if (!response.Result)
                {
                    _logger.LogError($"An error occured during retrieving men outfit list.");
                    return BadRequest(response);
                }

                _logger.LogInformation("Man outfit successfully retrieved.");
                return new OkObjectResult(response);
            }
            else if (outfitType == OutfitType.Woman)
            {
                var response = await _womanOutfitService.GetOutfitAsync(id);
                if (!response.Result)
                {
                    _logger.LogError($"An error occured during retrieving Women outfit list.");
                    return BadRequest(response);
                }

                _logger.LogInformation("Woman outfit successfully retrieved.");
                return new OkObjectResult(response);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("create/woman")]
        [ProducesResponseType(typeof(OutfitResponse<WomanOutfitDto>), 200)]
        [ProducesResponseType(typeof(OutfitResponse<WomanOutfitDto>), 400)]
        public async Task<IActionResult> Create([FromBody] WomanOutfitDto model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Woman outfit model is not valid.");
                return BadRequest();
            }

            var response = await _womanOutfitService.CreateOutfitAsync(model);
            if (!response.Result)
            {
                _logger.LogError($"An error occured during woman outfit creation.");
                return BadRequest(response);
            }

            _logger.LogInformation($"Woman outfit successfully created: {response.Item.Id}");
            return new OkObjectResult(response);
        }

        [HttpPut("edit/woman")]
        [ProducesResponseType(typeof(OutfitResponse<WomanOutfitDto>), 200)]
        [ProducesResponseType(typeof(OutfitResponse<WomanOutfitDto>), 400)]
        public async Task<IActionResult> Edit([FromBody] WomanOutfitDto model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Woman outfit model is not valid.");
                return BadRequest();
            }

            var response = await _womanOutfitService.UpdateOutfitAsync(model);
            if (!response.Result)
            {
                _logger.LogError($"An error occured during woman outfit edit.");
                return BadRequest(response);
            }

            _logger.LogInformation($"Woman outfit successfully edited: {response.Item.Id}");
            return new OkObjectResult(response);
        }

        [HttpPost("create/man")]
        [ProducesResponseType(typeof(OutfitResponse<ManOutfitDto>), 200)]
        [ProducesResponseType(typeof(OutfitResponse<ManOutfitDto>), 400)]
        public async Task<IActionResult> Create([FromBody] ManOutfitDto model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Man outfit model is not valid.");
                return BadRequest();
            }

            var response = await _manOutfitService.CreateOutfitAsync(model);
            if (!response.Result)
            {
                _logger.LogError($"An error occured during man outfit creation.");
                return BadRequest(response);
            }

            _logger.LogInformation($"Man outfit successfully created: {response.Item.Id}");
            return new OkObjectResult(response);
        }

        [HttpPut("edit/man")]
        [ProducesResponseType(typeof(OutfitResponse<ManOutfitDto>), 200)]
        [ProducesResponseType(typeof(OutfitResponse<ManOutfitDto>), 400)]
        public async Task<IActionResult> Edit([FromBody] ManOutfitDto model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Man outfit model is not valid.");
                return BadRequest();
            }

            var response = await _manOutfitService.UpdateOutfitAsync(model);
            if (!response.Result)
            {
                _logger.LogError($"An error occured during man outfit edit.");
                return BadRequest(response);
            }

            _logger.LogInformation($"Man outfit successfully edited: {response.Item.Id}");
            return new OkObjectResult(response);
        }
    }
}

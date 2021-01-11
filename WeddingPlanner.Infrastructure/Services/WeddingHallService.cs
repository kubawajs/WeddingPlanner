using AutoMapper;
using System;
using System.Threading.Tasks;
using WeddingPlanner.Infrastructure.Dto;
using WeddingPlanner.Infrastructure.Models;
using WeddingPlanner.Infrastructure.Models.Abstractions;
using WeddingPlanner.Infrastructure.Repository.Abstractions;
using WeddingPlanner.Infrastructure.Services.Abstractions;

namespace WeddingPlanner.Infrastructure.Services
{
    public class WeddingHallService : IWeddingHallService
    {
        private readonly IWeddingHallRespository _weddingHallRepository;
        private readonly IGuestRepository _guestRepository;
        private readonly IMapper _mapper;

        public WeddingHallService(IWeddingHallRespository weddingHallRespository, IGuestRepository guestRepository, IMapper mapper)
        {
            _weddingHallRepository = weddingHallRespository;
            _guestRepository = guestRepository;
            _mapper = mapper;
        }

        public async Task<WeddingHallSummaryResponse> CreateWeddingHall(WeddingHallDto model)
        {
            try
            {
                var weddingHall = _mapper.Map<WeddingHall>(model);
                await _weddingHallRepository.CreateWeddingHallAsync(weddingHall);
                return await CreateWeddingHallSummarySuccessResponse(weddingHall, "Wedding hall summary successfully created.");
            }
            catch (Exception ex)
            {
                return new WeddingHallSummaryResponse(
                    BaseApiResponse.CreateErrorResponse(
                        $"An error occured during wedding hall summary creation: {ex.Message}"));
            }
        }

        public async Task<WeddingHallSummaryResponse> GetWeddingHallSummary(int? id = null)
        {
            if(id == null)
            {
                return new WeddingHallSummaryResponse(BaseApiResponse.CreateErrorResponse("Id param not specified."));
            }

            try
            {
                var weddingHall = await _weddingHallRepository.GetWeddingHallAsync(id.Value);
                if (weddingHall == null)
                {
                    return new WeddingHallSummaryResponse(BaseApiResponse.CreateErrorResponse($"Specified wedding hall was not found. Id: {id.Value}"));
                }

                return await CreateWeddingHallSummarySuccessResponse(weddingHall, "Wedding hall summary successfully retrieved.");
            }
            catch(Exception ex)
            {
                return new WeddingHallSummaryResponse(
                    BaseApiResponse.CreateErrorResponse(
                        $"An error occured during wedding hall summary creation: {ex.Message}"));
            }
        }

        public async Task<WeddingHallSummaryResponse> UpdateWeddingHall(WeddingHallDto model)
        {
            try
            {
                var weddingHall = _mapper.Map<WeddingHall>(model);
                await _weddingHallRepository.UpdateWeddingHallAsync(weddingHall);
                return await CreateWeddingHallSummarySuccessResponse(weddingHall, "Wedding hall summary successfully updated.");
            }
            catch(Exception ex)
            {
                return new WeddingHallSummaryResponse(
                    BaseApiResponse.CreateErrorResponse(
                        $"An error occured during wedding hall summary update: {ex.Message}"));
            }
        }

        private async Task<WeddingHallSummaryResponse> CreateWeddingHallSummarySuccessResponse(WeddingHall weddingHall, string message)
        {
            var adultGuestsCount = await _guestRepository.GetAllGuestsCountAsync(weddingHall.ChildAgeTo);
            var childGuestsCount = await _guestRepository.GetChildGuestsCountAsync(weddingHall.ChildAgeFrom, weddingHall.ChildAgeTo);
            return new WeddingHallSummaryResponse(BaseApiResponse.CreateSuccessResponse(message))
            {
                AdultGuestsCount = adultGuestsCount,
                ChildGuestsCount = childGuestsCount,
                Summary = _mapper.Map<WeddingHallDto>(weddingHall)
            };
        }
    }
}

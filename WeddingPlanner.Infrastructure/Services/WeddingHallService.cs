using AutoMapper;
using System;
using System.Threading.Tasks;
using WeddingPlanner.Core.Domain;
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
                await _weddingHallRepository.CreateAsync(weddingHall);
                return await CreateWeddingHallSummarySuccessResponse(weddingHall, "Wedding hall summary successfully created.");
            }
            catch (Exception ex)
            {
                return new WeddingHallSummaryResponse(
                    BaseApiResponse<WeddingHallDto>.CreateErrorResponse(
                        $"An error occured during wedding hall summary creation: {ex.Message}"));
            }
        }

        public async Task<WeddingHallSummaryResponse> GetWeddingHallSummary(int? id = null)
        {
            if(id == null)
            {
                return new WeddingHallSummaryResponse(
                    BaseApiResponse<WeddingHallDto>.CreateErrorResponse("Id param not specified."));
            }

            try
            {
                var weddingHall = await _weddingHallRepository.GetAsync(id.Value);
                if (weddingHall == null)
                {
                    return new WeddingHallSummaryResponse(
                        BaseApiResponse<WeddingHallDto>.CreateErrorResponse($"Specified wedding hall was not found. Id: {id.Value}"));
                }

                return await CreateWeddingHallSummarySuccessResponse(weddingHall, "Wedding hall summary successfully retrieved.");
            }
            catch(Exception ex)
            {
                return new WeddingHallSummaryResponse(
                    BaseApiResponse<WeddingHallDto>.CreateErrorResponse(
                        $"An error occured during wedding hall summary creation: {ex.Message}"));
            }
        }

        public async Task<WeddingHallSummaryResponse> UpdateWeddingHall(WeddingHallDto model)
        {
            try
            {
                var weddingHall = _mapper.Map<WeddingHall>(model);
                await _weddingHallRepository.UpdateAsync(weddingHall);
                return await CreateWeddingHallSummarySuccessResponse(weddingHall, "Wedding hall summary successfully updated.");
            }
            catch(Exception ex)
            {
                return new WeddingHallSummaryResponse(
                    BaseApiResponse<WeddingHallDto>.CreateErrorResponse(
                        $"An error occured during wedding hall summary update: {ex.Message}"));
            }
        }

        private async Task<WeddingHallSummaryResponse> CreateWeddingHallSummarySuccessResponse(WeddingHall weddingHall, string message)
        {
            var adultGuestsCount = await _guestRepository.GetAllGuestsCountAsync(weddingHall.ChildAgeTo);
            var childGuestsCount = await _guestRepository.GetChildGuestsCountAsync(weddingHall.ChildAgeFrom, weddingHall.ChildAgeTo);
            var item = _mapper.Map<WeddingHallDto>(weddingHall);

            return new WeddingHallSummaryResponse(
                BaseApiResponse<WeddingHallDto>.CreateSuccessResponse(message, item))
            {
                AdultGuestsCount = adultGuestsCount,
                ChildGuestsCount = childGuestsCount
            };
        }
    }
}

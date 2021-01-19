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
    public class WeddingHallService : BaseService<WeddingHallDto, WeddingHall, IWeddingHallRespository, WeddingHallSummaryResponse>, IWeddingHallService
    {
        private readonly IGuestRepository _guestRepository;

        public WeddingHallService(
            IWeddingHallRespository weddingHallRespository,
            IGuestRepository guestRepository,
            IMapper mapper) : base(weddingHallRespository, mapper)
        {
            _guestRepository = guestRepository;
        }

        public async override Task<WeddingHallSummaryResponse> CreateAsync(WeddingHallDto model)
        {
            try
            {
                var weddingHall = Mapper.Map<WeddingHall>(model);
                await Repository.CreateAsync(weddingHall);
                return await CreateWeddingHallSummaryResponseAsync(weddingHall, "Wedding hall summary successfully created.");
            }
            catch (Exception ex)
            {
                return CreateErrorResponse($"An error occured during wedding hall summary creation: {ex.Message}");
            }
        }

        public async override Task<WeddingHallSummaryResponse> GetAsync(int id)
        {
            try
            {
                var weddingHall = await Repository.GetAsync(id);
                if (weddingHall == null)
                {
                    return new WeddingHallSummaryResponse(
                        BaseApiResponse<WeddingHallDto>.CreateErrorResponse($"Specified wedding hall was not found. Id: {id}"));
                }

                return await CreateWeddingHallSummaryResponseAsync(weddingHall, "Wedding hall summary successfully retrieved.");
            }
            catch(Exception ex)
            {
                return CreateErrorResponse($"An error occured during wedding hall summary creation: {ex.Message}");
            }
        }

        public async override Task<WeddingHallSummaryResponse> UpdateAsync(WeddingHallDto model)
        {
            try
            {
                var weddingHall = Mapper.Map<WeddingHall>(model);
                await Repository.UpdateAsync(weddingHall);
                return await CreateWeddingHallSummaryResponseAsync(weddingHall, "Wedding hall summary successfully updated.");
            }
            catch(Exception ex)
            {
                return CreateErrorResponse($"An error occured during wedding hall summary update: {ex.Message}");
            }
        }

        protected override WeddingHallSummaryResponse CreateErrorResponse(string message) 
            => new WeddingHallSummaryResponse(BaseApiResponse<WeddingHallDto>.CreateErrorResponse(message));

        protected override WeddingHallSummaryResponse CreateSuccessResponse(string message, WeddingHallDto item)
            => new WeddingHallSummaryResponse(BaseApiResponse<WeddingHallDto>.CreateSuccessResponse(message, item));

        private async Task<WeddingHallSummaryResponse> CreateWeddingHallSummaryResponseAsync(WeddingHall weddingHall, string message)
        {
            var item = Mapper.Map<WeddingHallDto>(weddingHall);
            var response = CreateSuccessResponse(message, item);
            response.AdultGuestsCount = await _guestRepository.GetAllGuestsCountAsync(weddingHall.ChildAgeTo);
            response.ChildGuestsCount = await _guestRepository.GetChildGuestsCountAsync(weddingHall.ChildAgeFrom, weddingHall.ChildAgeTo);

            return response;
        }
    }
}

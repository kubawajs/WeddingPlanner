using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeddingPlanner.Api.Services.Abstractions;
using WeddingPlanner.Core.Domain;
using WeddingPlanner.Infrastructure.Dto;
using WeddingPlanner.Infrastructure.Models;
using WeddingPlanner.Infrastructure.Models.Abstractions;
using WeddingPlanner.Infrastructure.Repository.Abstractions;

namespace WeddingPlanner.Api.Services
{
    public class GuestService : BaseService<GuestDto, Guest, IGuestRepository, GuestResponse>, IGuestService
    {
        public GuestService(
            IMapper mapper,
            IGuestRepository repository)
            : base(repository, mapper)
        { }

        // TODO: refactor
        public async Task<GuestListResponse> GetGuestsAsync()
        {
            try
            {
                var guests = await Repository.GetAllAsync();
                if (guests == null)
                {
                    return new GuestListResponse(BaseApiResponse<GuestListDto>.CreateErrorResponse("Cannot retrieve guests list."));
                }

                return CreateGuestListSuccessResponse(guests);
            }
            catch (Exception ex)
            {
                return new GuestListResponse(BaseApiResponse<GuestListDto>.CreateErrorResponse(
                    $"An error occured during guest list retrieve: {ex.Message}"));
            }
        }

        public async Task<GuestListResponse> GetGuestsByAgeAsync(int age)
        {
            try
            {
                var guests = await Repository.GetGuestsByAgeAsync(age);
                if (guests == null)
                {
                    return new GuestListResponse(BaseApiResponse<GuestListDto>.CreateErrorResponse($"Cannot retrieve guests list for given age {age}."));
                }

                var response = CreateGuestListSuccessResponse(guests);
                response.AgeParam = age;
                return response;
            }
            catch (Exception ex)
            {
                return new GuestListResponse(BaseApiResponse<GuestListDto>.CreateErrorResponse(
                    $"An error occured during guest list retrieve: {ex.Message}"));
            }
        }

        protected override GuestResponse CreateErrorResponse(string message)
            => new GuestResponse(BaseApiResponse<GuestDto>.CreateErrorResponse(message));

        protected override GuestResponse CreateSuccessResponse(string message, GuestDto item)
            => new GuestResponse(BaseApiResponse<GuestDto>.CreateSuccessResponse(message, item));

        private GuestListResponse CreateGuestListSuccessResponse(IEnumerable<Guest> guests)
        {
            var guestDtos = Mapper.Map<IEnumerable<GuestDto>>(guests);
            var guestList = new GuestListDto
            {
                Guests = guestDtos
            };

            return new GuestListResponse(
                BaseApiResponse<GuestListDto>.CreateSuccessResponse("Guests list successfully retrieved.", guestList))
            {
                Count = guestDtos.ToList().Count
            };
        }
    }
}

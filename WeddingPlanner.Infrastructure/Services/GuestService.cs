using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeddingPlanner.Core.Domain;
using WeddingPlanner.Infrastructure.Dto;
using WeddingPlanner.Infrastructure.Models;
using WeddingPlanner.Infrastructure.Models.Abstractions;
using WeddingPlanner.Infrastructure.Repository.Abstractions;
using WeddingPlanner.Infrastructure.Services.Abstractions;

namespace WeddingPlanner.Infrastructure.Services
{
    public class GuestService : IGuestService
    {
        private readonly IMapper _mapper;
        private readonly IGuestRepository _guestRepository;

        public GuestService(IMapper mapper, IGuestRepository guestRepository)
        {
            _mapper = mapper;
            _guestRepository = guestRepository;
        }

        public async Task<GuestResponse> CreateGuestAsync(GuestDto guestDto)
        {
            try
            {
                var guest = _mapper.Map<Guest>(guestDto);
                if (guest == null)
                {
                    throw new Exception("Guest cannot be null.");
                }

                await _guestRepository.CreateAsync(guest);
                var response = new GuestResponse(
                    BaseApiResponse<GuestDto>.CreateSuccessResponse("Guest successfully created", guestDto))
                {
                    Item = guestDto
                };
                return response;
            }
            catch (Exception ex)
            {
                return new GuestResponse(BaseApiResponse<GuestDto>.CreateErrorResponse(
                    $"An error occured during guest creation: {ex.Message}"));
            }
        }

        public async Task<GuestListResponse> GetGuestsAsync()
        {
            try
            {
                var guests = await _guestRepository.GetAllAsync();
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
                var guests = await _guestRepository.GetGuestsByAgeAsync(age);
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

        private GuestListResponse CreateGuestListSuccessResponse(IEnumerable<Guest> guests)
        {
            var guestDtos = _mapper.Map<IEnumerable<GuestDto>>(guests);
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

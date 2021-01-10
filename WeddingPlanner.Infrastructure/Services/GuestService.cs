using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task CreateGuestAsync(GuestDto guestDto)
        {
            var guest = _mapper.Map<Guest>(guestDto);
            if(guest != null)
            {
                await _guestRepository.CreateGuestAsync(guest);
            }
        }

        public async Task<GuestListResponse> GetGuestsAsync()
        {
            var guests = await _guestRepository.GetGuestsAsync();
            if (guests == null)
            {
                return new GuestListResponse(BaseApiResponse.CreateErrorResponse("Cannot retrieve guests list."));
            }

            return CreateSuccessResponse(guests);
        }

        public async Task<GuestListResponse> GetGuestsByAgeAsync(int age)
        {
            var guests = await _guestRepository.GetGuestsByAge(age);
            if (guests == null)
            {
                return new GuestListResponse(BaseApiResponse.CreateErrorResponse($"Cannot retrieve guests list for given age {age}."));
            }

            var response = CreateSuccessResponse(guests);
            response.AgeParam = age;
            
            return response;
        }

        private GuestListResponse CreateSuccessResponse(IEnumerable<Guest> guests)
        {
            var guestDtos = _mapper.Map<IEnumerable<GuestDto>>(guests);
            var response = new GuestListResponse(BaseApiResponse.CreateSuccessResponse("Guests list successfully retrieved."))
            {
                Items = guestDtos,
                Count = guestDtos.ToList().Count
            };
            return response;
        }
    }
}

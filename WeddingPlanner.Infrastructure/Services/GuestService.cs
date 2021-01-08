using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeddingPlanner.Infrastructure.Dto;
using WeddingPlanner.Infrastructure.Models;
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

        public async Task<IEnumerable<GuestDto>> GetGuestsAsync()
        {
            var guests = await _guestRepository.GetGuestsAsync();
            var guestDtos = _mapper.Map<IEnumerable<GuestDto>>(guests);
            return guestDtos;
        }
    }
}

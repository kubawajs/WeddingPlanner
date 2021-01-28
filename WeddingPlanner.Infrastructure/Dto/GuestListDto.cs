using System;
using System.Collections.Generic;
using System.Text;
using WeddingPlanner.Infrastructure.Dto.Abstractions;

namespace WeddingPlanner.Infrastructure.Dto
{
    public class GuestListDto : BaseDto
    {
        public IEnumerable<GuestDto> Guests { get; set; }
    }
}

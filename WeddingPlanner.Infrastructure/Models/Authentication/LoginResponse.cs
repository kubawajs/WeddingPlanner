﻿using System;
using WeddingPlanner.Infrastructure.Dto;
using WeddingPlanner.Infrastructure.Models.Abstractions;

namespace WeddingPlanner.Infrastructure.Models.Authentication
{
    public class LoginResponse : BaseApiResponse<UserDto>
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }

        public LoginResponse(BaseApiResponse<UserDto> response)
        {
            Message = response.Message;
            Status = response.Status;
            Result = response.Result;
            Item = response.Item;
        }
    }
}

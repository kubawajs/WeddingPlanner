using System;
using System.Collections.Generic;
using System.Text;

namespace WeddingPlanner.Infrastructure
{
    public static class Constansts
    {
        public struct Authentication
        {
            public const string JwtIssuerConfig = "Jwt:Issuer";
            public const string JwtAudienceConfig = "Jwt:Audience";
            public const string JwtSecretKeyConfig = "Jwt:SecretKey";
        }
    }
}

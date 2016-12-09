using System;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

namespace JWT.Common.Middlewares.TokenProvider
{
    public class TokenProviderOptions
    {
        public string Path { get; set; } = "/auth/token";
        public string RefreshTokenPath { get; set; } = "/auth/refresh_token";

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public TimeSpan Expiration { get; set; } = TimeSpan.FromMinutes(10);

        public SigningCredentials SigningCredentials { get; set; }
        public IConfigurationRoot config { get; set; }
    }
}

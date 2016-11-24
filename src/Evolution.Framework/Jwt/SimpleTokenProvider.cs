﻿using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Http.Authentication;

namespace Evolution.Framework.Jwt
{
    public class SimpleTokenProvider
    {
        public class CustomJwtDataFormat : ISecureDataFormat<AuthenticationTicket>
        {
            private readonly string algorithm;
            private readonly TokenValidationParameters validationParameters;

            public CustomJwtDataFormat(string algorithm, TokenValidationParameters validationParameters)
            {
                this.algorithm = algorithm;
                this.validationParameters = validationParameters;
            }

            public AuthenticationTicket Unprotect(string protectedText)
                => Unprotect(protectedText, null);

            public AuthenticationTicket Unprotect(string protectedText, string purpose)
            {
                //AuthenticationTicket authTicket = protectedText.ToObject<AuthenticationTicket>();

                var handler = new JwtSecurityTokenHandler();
                ClaimsPrincipal principal = null;
                SecurityToken validToken = null;

                try
                {
                    principal = handler.ValidateToken(protectedText, this.validationParameters, out validToken);

                    var validJwt = validToken as JwtSecurityToken;

                    if (validJwt == null)
                    {
                        throw new ArgumentException("Invalid JWT");
                    }

                    if (!validJwt.Header.Alg.Equals(algorithm, StringComparison.Ordinal))
                    {
                        throw new ArgumentException($"Algorithm must be '{algorithm}'");
                    }

                    // Additional custom validation of JWT claims here (if any)
                }
                catch (SecurityTokenValidationException)
                {
                    return null;
                }
                catch (ArgumentException)
                {
                    return null;
                }

                // Validation passed. Return a valid AuthenticationTicket:
                return new AuthenticationTicket(principal, new AuthenticationProperties(), "CookieAuth");
            }

            //// This ISecureDataFormat implementation is decode-only
            public string Protect(AuthenticationTicket data)
            {
                return string.Empty;
            }

            public string Protect(AuthenticationTicket data, string purpose)
            {
                return string.Empty;
            }
        }
    }
}

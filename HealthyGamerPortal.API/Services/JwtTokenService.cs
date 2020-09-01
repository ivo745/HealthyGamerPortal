using HealthyGamerPortal.API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace HealthyGamerPortal.API.Services
{
    /// <summary>
    /// Implementation of the <see cref="ITokenService"/> using JWT tokens for authentication.
    /// </summary>
    public class JwtTokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Create a new instance of the <see cref="JwtTokenService"/> with an instance of <see cref="IConfiguration"/> and <see cref="IHttpContextAccessor"/>.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="httpContextAccessor">An instance of the <see cref="IHttpContextAccessor"/> interface, for accessing the current user on the HttpContext.</param>
        public JwtTokenService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <inheritdoc />
        public string GenerateToken(string apiToken)
        {
            var signingKey = Convert.FromBase64String(_configuration["JWT:JWTSigningSecret"]);
            int.TryParse(_configuration["JWT:JWTLifetimeInMinutes"], out var tokenLifetime);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration["JWT:JWTTokenIssuer"],
                Audience = null, //Audience cannot be checked because there could be many different audiences depending on who installs the mobile app.
                IssuedAt = DateTime.UtcNow,
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(tokenLifetime),
                Subject = new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimConstants.ExternalTokenClaimName, apiToken)
                }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(signingKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            return tokenHandler.WriteToken(jwtToken);
        }

        /// <inheritdoc />
        public string GetApiToken()
        {
            var currentUser = _httpContextAccessor.HttpContext.User;
            if (currentUser == null || !currentUser.HasClaim(c => c.Type == ClaimConstants.ExternalTokenClaimName))
                return string.Empty;

            return currentUser.Claims.First(c => c.Type == ClaimConstants.ExternalTokenClaimName).Value;
        }
    }
}
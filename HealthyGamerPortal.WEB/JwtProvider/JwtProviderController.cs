using HealthyGamerPortal.Common.ViewModels.Login;
using HealthyGamerPortal.WEB.Controllers.Base;
using HealthyGamerPortal.WEB.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Refit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Principal;
using System.Threading.Tasks;

namespace HealthyGamerPortal.WEB.JwtProvider
{
    /// <summary>
    /// Token provider and authentication functionality for middleware to perform basic sign in.
    /// </summary>
    /// <remarks>
    /// Gets user dbo object from api and inserts identity claims from api response in to jwt
    /// jwt is handed over to api to be inserted in the api auth token
    /// httpcontext user identity is filled in manually with the claims from the token
    /// </remarks>
    public class JwtProviderController : BaseController
    {
        protected JwtProviderOptions _jwtOptions;
        protected HttpContext _httpContext;

        /// <summary>
        /// Sends form data encrypted to api to perform existance and password check.
        /// </summary>
        public async Task<bool> Authenticate(EncryptedBasicLoginModel model)
        {
            var api = RestService.For<IHealthyGamerPortalUserApi>(new HttpClient(new Helpers.AnonymousHttpClientHandler()) { BaseAddress = new Uri(BaseUrl) });
            var response = await api.Authenticate(model);
            if (response.Result != null)
            {
                await PopulateUserIdentity(response.Result);
                return true;
            }

            return false;
        }

        private async Task PopulateUserIdentity(BasicAuthenticationResult response)
        {
            var customClaims = await GetUserClaims(response.Name); // has to be retrieved from BasicAuthenticationResult in future
            var webToken = await GenerateWebToken(customClaims);
            var userIdentity = await GetIdentity(response.Name);

            await PerformSignIn(new GenericPrincipal(userIdentity, response.Roles), webToken.ToString());
        }

        private async Task<SecurityTokenDescriptor> GenerateWebToken(Dictionary<string, object> customClaims)
        {
            // Create the JWT security token and encode it.
            var jwt = new SecurityTokenDescriptor()
            {
                Issuer = _jwtOptions.Issuer,
                Audience = _jwtOptions.Audience,
                Claims = customClaims,
                NotBefore = _jwtOptions.NotBefore,
                Expires = _jwtOptions.Expiration,
                SigningCredentials = _jwtOptions.SigningCredentials
            };

            return jwt;
        }

        private async Task PerformSignIn(GenericPrincipal user, string webToken)
        {
            Dictionary<string, string> items = new Dictionary<string, string>
            {
                { "AuthScheme", CookieAuthenticationDefaults.AuthenticationScheme },
            };

            AuthenticationProperties props = new AuthenticationProperties(items)
            {
                // web authentication ticket life time, after expire new log in required
                ExpiresUtc = DateTime.UtcNow.AddHours(8),
                IsPersistent = false,
                AllowRefresh = false,
            };

            List<AuthenticationToken> authenticationTokens = new List<AuthenticationToken>
            {
                new AuthenticationToken { Name = "access_token", Value = webToken },
            };

            AuthenticationTokenExtensions.StoreTokens(props, authenticationTokens);

            await AuthenticationHttpContextExtensions.SignInAsync(_httpContext,
                CookieAuthenticationDefaults.AuthenticationScheme, user, props);
        }

        /// <summary>
        /// Retrieves user claims from database to use in JWT and httpcontext authentication.
        /// </summary>
        private async Task<Dictionary<string, object>> GetUserClaims(string username) // frombody?
        {
            var api = RestService.For<IHealthyGamerPortalUserApi>(new HttpClient(new Helpers.AnonymousHttpClientHandler()) { BaseAddress = new Uri(BaseUrl) });

            //Get claims from db using api
            //For now local

            string uniqueId = await _jwtOptions.JtiGenerator();

            var claims = new Dictionary<string, object>
            {
                { "Sub", username },
                { "Jti", uniqueId },
                { "Iat", ToUnixEpochDate(_jwtOptions.IssuedAt).ToString() }
            };

            return claims;
        }

        /// <summary>
        /// Retrieves user Identity based on claims to use as authentication user.
        /// </summary>
        private async Task<GenericIdentity> GetIdentity(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return null;
            }

            return new GenericIdentity(username, "Token");
        }

        /// <summary>
        /// Converts data time to standard JWT expire time in Unix epoch time format.
        /// </summary>
        private long ToUnixEpochDate(DateTime date)
        {
            return (long)Math.Round((date.ToUniversalTime() -
                                          new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                                         .TotalSeconds);
        }
    }
}
using HealthyGamerPortal.API.Interfaces;
using HealthyGamerPortal.Common.Models;
using HealthyGamerPortal.Common.ViewModels.Api.Authentication;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Refit;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace HealthyGamerPortal.API.Services
{
    /// <summary>
    /// Class that implements the <see cref="IAuthentication"/> interface for use with the HealthyGamerPortal API.
    /// </summary>
    public class AuthenticationService : IAuthentication
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Create a new instance of <see cref="AuthenticationService"/> with an instance of <see cref="IConfiguration"/>.
        /// </summary>
        /// <param name="configuration"></param>
        public AuthenticationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <inheritdoc />
        public async Task<ApiLoginResult> LoginAsync(ApiLoginModel model)
        {
            if (string.IsNullOrEmpty(model.Token))
                throw new ArgumentException("Authentication token missing.");

            var authResult = await Authenticate(new ApiLoginModel
            {
                Username = model.Username,
                Token = model.Token
            });

            string responseContent = await authResult.Content.ReadAsStringAsync();
            if (authResult.StatusCode == HttpStatusCode.BadRequest)
                return new ApiLoginResult { HasError = true };

            return JsonConvert.DeserializeObject<ApiLoginResult>(responseContent);
        }

        /// <summary>
        /// Validates the login credentials received from WEB, which are required for authorization within API.
        /// </summary>
        /// <returns>Returns IEnumerable CompanyBuildingViewModel.</returns>
        /// <response code="200">The status was successfully retrieved from the API.</response>
        [Post("/api/authentication/login")] //Refit does not support using constants. See: https://github.com/reactiveui/refit/issues/263
        public async Task<HttpResponseMessage> Authenticate(ApiLoginModel loginData)
        {
            if (!string.IsNullOrEmpty(loginData.Token))
            {
                ApiLoginResult result = new ApiLoginResult { Token = loginData.Token, Expires = 0, HasError = false, Username = loginData.Username };
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(result))
                };
                return response;
            }
            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }
    }
}
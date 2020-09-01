using AutoMapper;
using HealthyGamerPortal.API.Interfaces;
using HealthyGamerPortal.Business.Interfaces;
using HealthyGamerPortal.Common.Enums;
using HealthyGamerPortal.Common.Models;
using HealthyGamerPortal.Common.ViewModels.Api;
using HealthyGamerPortal.Common.ViewModels.Api.Authentication;
using HealthyGamerPortal.Common.ViewModels.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HealthyGamerPortal.API.Endpoints.Account
{
    /// <summary>
    /// Controller class that takes care of Customer functionality.
    /// </summary>
    public class AccountApiController : BaseApiController
    {
        private readonly IAuthentication _authenticationService;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;

        /// <summary>
        /// Create a new instance of the <see cref="AccountApiController"/> with the needed dependencies.
        /// </summary>
        /// <param name="mapper">An instance of the <see cref="IMapper"/> interface, for automatically mapping different objects to each other.</param>
        /// <param name="authenticationService"></param>
        /// <param name="tokenService"></param>
        /// <param name="accountService"></param>
        public AccountApiController(IMapper mapper, IAuthentication authenticationService, ITokenService tokenService, IAccountService accountService)
        {
            _mapper = mapper;
            _authenticationService = authenticationService;
            _tokenService = tokenService;
            _accountService = accountService;
        }

        /// <summary>
        /// Gets a <see cref="ApiResponseModel{AuthenticationModel}"/>
        /// </summary>
        /// <param name="model"></param>
        /// <returns><see cref="ApiResponseModel{AuthenticationModel}"/></returns>
        /// <response code="200">The status was successfully retrieved from the API.</response>
        [HttpPost]
        [Route("Account/Login")]
        [ProducesResponseType(typeof(ApiResponseModel<AuthenticationModel>), 200)]
        [AllowAnonymous]
        public async Task<IActionResult> Login(ApiLoginModel model)
        {
            ApiLoginResult result = await _authenticationService.LoginAsync(model);
            if (string.IsNullOrEmpty(result.Token))
                return Unauthorized(); //The external API returns a bad request instead of unauthorized when the user credentials are wrong, hence we have to use this ugly check.

            result.Token = _tokenService.GenerateToken(result.Token);
            return Ok(GenerateSuccessfulResponse(result));
        }

        /// <summary>
        /// Retrieves the AccountType based on the value of the password field connected to the provided email address.
        /// </summary>
        /// <param name="encryptedMessage"></param>
        /// <returns><see cref="ApiResponseModel{AuthenticationModel}"/></returns>
        /// <response code="200">The status was successfully retrieved from the API.</response>
        [HttpGet]
        [Route("Account/IsBasicAccount")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiResponseModel<AccountType>), 200)]
        public async Task<IActionResult> IsBasicAccount([FromQuery]EncryptedMessage encryptedMessage)
        {
            var result = await _accountService.IsBasicAccount(encryptedMessage);
            return Ok(GenerateSuccessfulResponse(result));
        }
    }
}

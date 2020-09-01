using AutoMapper;
using HealthyGamerPortal.Business.Interfaces;
using HealthyGamerPortal.Common.ViewModels.Api;
using HealthyGamerPortal.Common.ViewModels.Login;
using HealthyGamerPortal.Common.ViewModels.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthyGamerPortal.API.Endpoints.User
{
    /// <summary>
    /// Controller class that takes care of ApplicationUser functionality.
    /// </summary>
    public class ApplicationUserApiController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IApplicationUserService _applicationUserService;

        /// <summary>
        /// Create a new instance of the <see cref="ApplicationUserApiController"/> with the needed dependencies.
        /// </summary>
        /// <param name="mapper">An instance of the <see cref="IMapper"/> interface, for automatically mapping different objects to each other.</param>
        /// <param name="applicationUserService"></param>
        public ApplicationUserApiController(IMapper mapper, IApplicationUserService applicationUserService)
        {
            _mapper = mapper;
            _applicationUserService = applicationUserService;
        }

        /// <summary>
        /// Gets a IEnumerable of <see cref="ApiResponseModel{ApplicationUserViewModel}"/>.
        /// </summary>
        /// <returns>Returns IEnumerable <see cref="ApiResponseModel{ApplicationUserViewModel}"/></returns>
        /// <response code="200">The status was successfully retrieved from the API.</response>
        [HttpGet]
        [Route("ApplicationUser/GetManyUsers")]
        [ProducesResponseType(typeof(ApiResponseModel<IEnumerable<ApplicationUserViewModel>>), 200)]
        [Authorize]
        public async Task<IActionResult> GetManyUsers()
        {
            var result = await _applicationUserService.GetManyApplicationUser();
            return Ok(GenerateSuccessfulResponse(result));
        }

        /// <summary>
        /// Gets a <see cref="ApiResponseModel{ApplicationUserViewModel}"/>.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns><see cref="ApiResponseModel{ApplicationUserViewModel}"/></returns>
        /// <response code="200">The status was successfully retrieved from the API.</response>
        [HttpGet]
        [Route("ApplicationUser/GetSingleApplicationUserById")]
        [ProducesResponseType(typeof(ApiResponseModel<ApplicationUserViewModel>), 200)]
        [Authorize]
        public async Task<IActionResult> GetSingleApplicationUserById(Guid Id)
        {
            var result = await _applicationUserService.GetSingleApplicationUserById(Id);
            return Ok(GenerateSuccessfulResponse(result));
        }

        /// <summary>
        /// Gets a <see cref="ApiResponseModel{ApplicationUserViewModel}"/>.
        /// </summary>
        /// <param name="name"></param>
        /// <returns><see cref="ApiResponseModel{ApplicationUserViewModel}"/></returns>
        /// <response code="200">The status was successfully retrieved from the API.</response>
        [HttpGet]
        [Route("ApplicationUser/GetSingleApplicationUserByName")]
        [ProducesResponseType(typeof(ApiResponseModel<ApplicationUserViewModel>), 200)]
        [Authorize]
        public async Task<IActionResult> GetSingleApplicationUserByName(string name)
        {
            var result = await _applicationUserService.GetSingleApplicationUserByName(name);
            return Ok(GenerateSuccessfulResponse(result));
        }

        /// <summary>
        /// Gets a <see cref="ApiResponseModel{ApplicationUserViewModel}"/>.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns><see cref="ApiResponseModel{EditApplicationUserViewModel}"/></returns>
        /// <response code="200">The status was successfully retrieved from the API.</response>
        [HttpGet]
        [Route("ApplicationUser/GetSingleEditApplicationUserById")]
        [ProducesResponseType(typeof(ApiResponseModel<EditApplicationUserViewModel>), 200)]
        [Authorize]
        public async Task<IActionResult> GetSingleEditApplicationUserById(Guid Id)
        {
            var result = await _applicationUserService.GetSingleApplicationUserById(Id);
            return Ok(GenerateSuccessfulResponse(_mapper.Map<EditApplicationUserViewModel>(result)));
        }

        /// <summary>
        /// Gets a <see cref="ApiResponseModel{ApplicationUserViewModel}"/>.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns><see cref="ApiResponseModel{DeleteApplicationUserViewModel}"/></returns>
        /// <response code="200">The status was successfully retrieved from the API.</response>
        [HttpGet]
        [Route("ApplicationUser/GetSingleDeleteApplicationUserById")]
        [ProducesResponseType(typeof(ApiResponseModel<DeleteApplicationUserViewModel>), 200)]
        [Authorize]
        public async Task<IActionResult> GetSingleDeleteApplicationUserById(Guid Id)
        {
            var result = await _applicationUserService.GetSingleApplicationUserById(Id);
            return Ok(GenerateSuccessfulResponse(_mapper.Map<DeleteApplicationUserViewModel>(result)));
        }

        /// <summary>
        /// Gets a <see cref="ApiResponseModel{ApplicationUserViewModel}"/>.
        /// </summary>
        /// <param name="name"></param>
        /// <returns><see cref="ApiResponseModel{DeleteApplicationUserViewModel}"/></returns>
        /// <response code="200">The status was successfully retrieved from the API.</response>
        [HttpGet]
        [Route("ApplicationUser/GetSingleDeleteApplicationUserByName")]
        [ProducesResponseType(typeof(ApiResponseModel<DeleteApplicationUserViewModel>), 200)]
        [Authorize]
        public async Task<IActionResult> GetSingleDeleteApplicationUserByName(string name)
        {
            var result = await _applicationUserService.GetSingleApplicationUserByName(name);
            return Ok(GenerateSuccessfulResponse(_mapper.Map<DeleteApplicationUserViewModel>(result)));
        }

        /// <summary>
        /// Create a <see cref="CreateApplicationUserViewModel"/>.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Returns Bool <see cref="ApiResponseModel{Boolean}"/></returns>
        /// <response code="200">The status was successfully retrieved from the API.</response>
        [HttpPost]
        [Route("ApplicationUser/CreateSingleApplicationUser")]
        [ProducesResponseType(typeof(bool), 200)]
        [Authorize]
        public async Task<IActionResult> CreateSingleApplicationUser([FromBody] CreateApplicationUserViewModel model)
        {
            var result = await _applicationUserService.CreateSingleApplicationUser(model);
            return Ok(GenerateSuccessfulResponse(result));
        }

        /// <summary>
        /// Edit a <see cref="EditApplicationUserViewModel"/>.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Returns Bool <see cref="ApiResponseModel{Boolean}"/></returns>
        /// <response code="200">The status was successfully retrieved from the API.</response>
        [HttpPost]
        [Route("ApplicationUser/EditSingleApplicationUser")]
        [ProducesResponseType(typeof(bool), 200)]
        [Authorize]
        public async Task<IActionResult> EditSingleApplicationUser([FromBody] EditApplicationUserViewModel model)
        {
            var result = await _applicationUserService.EditSingleApplicationUser(model);
            return Ok(GenerateSuccessfulResponse(result));
        }

        /// <summary>
        /// Delete a <see cref="DeleteApplicationUserViewModel"/>.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Returns Bool <see cref="ApiResponseModel{Boolean}"/></returns>
        /// <response code="200">The status was successfully retrieved from the API.</response>
        [HttpPost]
        [Route("ApplicationUser/DeleteSingleApplicationUser")]
        [ProducesResponseType(typeof(bool), 200)]
        [Authorize]
        public async Task<IActionResult> DeleteSingleApplicationUser([FromBody] DeleteApplicationUserViewModel model)
        {
            var result = await _applicationUserService.DeleteSingleApplicationUser(model);
            return Ok(GenerateSuccessfulResponse(result));
        }

        /// <summary>
        /// Authenticate user by <see cref="EncryptedBasicLoginModel"/>.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Returns Bool <see cref="ApiResponseModel{Boolean}"/></returns>
        /// <response code="200">The status was successfully retrieved from the API.</response>
        [HttpPost]
        [Route("ApplicationUser/Authenticate")]
        [ProducesResponseType(typeof(ApiResponseModel<BasicAuthenticationResult>), 200)]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate(EncryptedBasicLoginModel model)
        {
            var result = await _applicationUserService.Authenticate(model);
            return Ok(GenerateSuccessfulResponse(result));
        }
    }
}
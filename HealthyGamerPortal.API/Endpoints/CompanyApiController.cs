using AutoMapper;
using HealthyGamerPortal.API.Interfaces;
using HealthyGamerPortal.Business.Interfaces;
using HealthyGamerPortal.Common.ViewModels.Api;
using HealthyGamerPortal.Common.ViewModels.Company;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HealthyGamerPortal.API.Endpoints.Company
{
    /// <summary>
    /// Controller class that takes care of Customer functionality.
    /// </summary>
    public class CompanyApiController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly ICompanyService _companyService;
        private readonly ITokenService _tokenService;

        /// <summary>
        /// Create a new instance of the <see cref="CompanyApiController"/> with the needed dependencies.
        /// </summary>
        /// <param name="mapper">An instance of the <see cref="IMapper"/> interface, for automatically mapping different objects to each other.</param>
        /// <param name="companyService"></param>
        /// <param name="tokenService"></param>
        public CompanyApiController(IMapper mapper, ICompanyService companyService, ITokenService tokenService)
        {
            _mapper = mapper;
            _companyService = companyService;
            _tokenService = tokenService;
        }

        /// <summary>
        /// ToDo:AUTH + TOKEN (DEV)
        /// </summary>
        /// <returns>Returns IEnumerable CompanyBuildingViewModel.</returns>
        /// <response code="200">The status was successfully retrieved from the API.</response>
        [HttpGet]
        [Route("Company/GetManyCompanyBuilding")]
        [ProducesResponseType(typeof(ApiResponseModel<CompanyBuildingViewModel>), 200)]
        [Authorize]
        public async Task<IActionResult> GetManyCompanyBuilding()
        {
            var result = await _companyService.GetManyCompanyBuilding();
            return Ok(GenerateSuccessfulResponse(result));
        }
    }
}
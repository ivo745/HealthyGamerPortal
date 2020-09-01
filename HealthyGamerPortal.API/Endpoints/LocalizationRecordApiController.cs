using AutoMapper;
using HealthyGamerPortal.Business.Interfaces;
using HealthyGamerPortal.Common.ViewModels.Api;
using HealthyGamerPortal.Common.ViewModels.LocalizationRecords;
using HealthyGamerPortal.Models;
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
    public class LocalizationRecordApiController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly ILocalizationRecordService _LocalizationRecordsService;

        /// <summary>
        /// Create a new instance of the <see cref="LocalizationRecordApiController"/> with the needed dependencies.
        /// </summary>
        /// <param name="mapper">An instance of the <see cref="IMapper"/> interface, for automatically mapping different objects to each other.</param>
        /// <param name="LocalizationRecordsService"></param>
        public LocalizationRecordApiController(IMapper mapper, ILocalizationRecordService LocalizationRecordsService)
        {
            _mapper = mapper;
            _LocalizationRecordsService = LocalizationRecordsService;
        }

        /// <summary>
        /// Gets a List of <see cref="ApiResponseModel{LocalizationRecordViewModel}"/>
        /// </summary>
        /// <returns>Returns List <see cref="ApiResponseModel{LocalizationRecordViewModel}"/></returns>
        /// <response code="200">The status was successfully retrieved from the API.</response>
        [HttpGet]
        [Route("LocalizationRecord/GetManyLocalizationRecords")]
        [ProducesResponseType(typeof(ApiResponseModel<List<LocalizationRecordViewModel>>), 200)]
        [AllowAnonymous]
        public async Task<IActionResult> GetManyLocalizationRecords()
        {
            var result = await _LocalizationRecordsService.GetManyLocalizationRecords();
            return Ok(GenerateSuccessfulResponse(result));
        }

        /// <summary>
        /// Gets a single <see cref="ApiResponseModel{LocalizationRecordViewModel}"/>
        /// </summary>
        /// <param name="Id"></param>
        /// <returns><see cref="ApiResponseModel{LocalizationRecordViewModel}"/></returns>
        /// <response code="200">The status was successfully retrieved from the API.</response>
        [HttpGet]
        [Route("LocalizationRecord/GetSingleLocalizationRecordById")]
        [ProducesResponseType(typeof(ApiResponseModel<LocalizationRecord>), 200)]
        [Authorize]
        public async Task<IActionResult> GetSingleLocalizationRecordById(Guid Id)
        {
            var result = await _LocalizationRecordsService.GetSingleLocalizationRecordById(Id);
            return Ok(GenerateSuccessfulResponse(result));
        }

        /// <summary>
        /// edits a single <see cref="ApiResponseModel{LocalizationRecordViewModel}"/>
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Returns Bool <see cref="ApiResponseModel{Boolean}"/></returns>
        /// <response code="200">The status was successfully retrieved from the API.</response>
        [HttpPost]
        [Route("LocalizationRecord/EditSingleLocalizationRecord")]
        [ProducesResponseType(typeof(bool), 200)]
        [Authorize]
        public async Task<IActionResult> EditSingleLocalizationRecord([FromBody] LocalizationRecordViewModel model)
        {
            var result = await _LocalizationRecordsService.EditSingleLocalizationRecord(model);
            return Ok(GenerateSuccessfulResponse(result));
        }
    }
}

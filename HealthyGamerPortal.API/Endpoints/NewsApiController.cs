using AutoMapper;
using HealthyGamerPortal.Business.Interfaces;
using HealthyGamerPortal.Common.ViewModels.Api;
using HealthyGamerPortal.Common.ViewModels.News;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HealthyGamerPortal.API.Endpoints.User
{
    /// <summary>
    /// Controller class that takes care of Customer functionality.
    /// </summary>
    public class NewsApiController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly INewsService _INewsService;

        /// <summary>
        /// Create a new instance of the <see cref="NewsApiController"/> with the needed dependencies.
        /// </summary>
        /// <param name="mapper">An instance of the <see cref="IMapper"/> interface, for automatically mapping different objects to each other.</param>
        /// <param name="NewsService"></param>
        public NewsApiController(IMapper mapper, INewsService NewsService)
        {
            _mapper = mapper;
            _INewsService = NewsService;
        }

        /// <summary>
        /// Gets a IEnumerable of <see cref="ApiResponseModel{NewsItemViewModel}"/> with short messages
        /// </summary>
        /// <returns>Returns IEnumerable <see cref="ApiResponseModel{NewsItemViewModel}"/></returns>
        /// <response code="200">The status was successfully retrieved from the API.</response>
        [HttpGet]
        [Route("NewsManagement/GetManyShortNews")]
        [ProducesResponseType(typeof(ApiResponseModel<NewsItemViewModel>), 200)]
        [Authorize]
        public async Task<IActionResult> GetManyShortNews()
        {
            var result = await _INewsService.GetManyShortNews();
            return Ok(GenerateSuccessfulResponse(result));
        }

        /// <summary>
        /// Gets a IEnumerable of <see cref="ApiResponseModel{NewsItemViewModel}"/>
        /// </summary>
        /// <returns>Returns IEnumerable <see cref="ApiResponseModel{NewsItemViewModel}"/></returns>
        /// <response code="200">The status was successfully retrieved from the API.</response>
        [HttpGet]
        [Route("NewsManagement/GetManyNews")]
        [ProducesResponseType(typeof(ApiResponseModel<NewsItemViewModel>), 200)]
        [Authorize]
        public async Task<IActionResult> GetManyNews()
        {
            var result = await _INewsService.GetManyNews();
            return Ok(GenerateSuccessfulResponse(result));
        }

        /// <summary>
        /// Gets a <see cref="ApiResponseModel{NewsItemViewModel}"/>
        /// </summary>
        /// <param name="Id"></param>
        /// <returns> <see cref="ApiResponseModel{NewsItemViewModel}"/></returns>
        /// <response code="200">The status was successfully retrieved from the API.</response>
        [HttpGet]
        [Route("NewsManagement/GetSingleNewsById")]
        [ProducesResponseType(typeof(ApiResponseModel<NewsItemViewModel>), 200)]
        [Authorize]
        public async Task<IActionResult> GetSingleNewsById(Guid Id)
        {
            var result = await _INewsService.GetSingleNewsById(Id);
            return Ok(GenerateSuccessfulResponse(result));
        }

        /// <summary>
        /// Gets a <see cref="ApiResponseModel{NewsItemViewModel}"/>
        /// </summary>
        /// <param name="Id"></param>
        /// <returns> <see cref="ApiResponseModel{DeleteNewsItemViewModel}"/></returns>
        /// <response code="200">The status was successfully retrieved from the API.</response>
        [HttpGet]
        [Route("NewsManagement/GetSingleDeleteNewsById")]
        [ProducesResponseType(typeof(ApiResponseModel<DeleteNewsItemViewModel>), 200)]
        [Authorize]
        public async Task<IActionResult> GetSingleDeleteNewsById(Guid Id)
        {
            var result = await _INewsService.GetSingleNewsById(Id);
            return Ok(GenerateSuccessfulResponse(_mapper.Map<DeleteNewsItemViewModel>(result)));
        }

        /// <summary>
        /// Gets a <see cref="ApiResponseModel{NewsItemViewModel}"/>
        /// </summary>
        /// <param name="Id"></param>
        /// <returns> <see cref="ApiResponseModel{EditNewsItemViewModel}"/></returns>
        /// <response code="200">The status was successfully retrieved from the API.</response>
        [HttpGet]
        [Route("NewsManagement/GetSingleEditNewsById")]
        [ProducesResponseType(typeof(ApiResponseModel<EditNewsItemViewModel>), 200)]
        public async Task<IActionResult> GetSingleEditNewsById(Guid Id)
        {
            var result = await _INewsService.GetSingleNewsById(Id);
            return Ok(GenerateSuccessfulResponse(_mapper.Map<EditNewsItemViewModel>(result)));
        }

        /// <summary>
        /// Creates a of <see cref="CreateNewsItemViewModel"/>
        /// </summary>
        /// <param name="model"></param>
        /// <returns><see cref="Boolean"/></returns>
        /// <response code="200">The status was successfully retrieved from the API.</response>
        [HttpPost]
        [Route("NewsManagement/CreateSingleNews")]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> CreateSingleNews([FromBody] CreateNewsItemViewModel model)
        {
            var result = await _INewsService.CreateSingleNews(model);
            return Ok(GenerateSuccessfulResponse(result));
        }

        /// <summary>
        /// Edit a <see cref="EditNewsItemViewModel"/>
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Returns Bool <see cref="ApiResponseModel{Boolean}"/></returns>
        /// <response code="200">The status was successfully retrieved from the API.</response>
        [HttpPost]
        [Route("NewsManagement/EditSingleNews")]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> EditSingleNews([FromBody] EditNewsItemViewModel model)
        {
            var result = await _INewsService.EditSingleNews(model);
            return Ok(GenerateSuccessfulResponse(result));
        }

        /// <summary>
        /// Delete a <see cref="DeleteNewsItemViewModel"/>
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Returns Bool <see cref="ApiResponseModel{Boolean}"/></returns>
        /// <response code="200">The status was successfully retrieved from the API.</response>
        [HttpPost]
        [Route("NewsManagement/DeleteSingleNews")]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> DeleteSingleNews([FromBody] DeleteNewsItemViewModel model)
        {
            var result = await _INewsService.DeleteSingleNews(model);
            return Ok(GenerateSuccessfulResponse(result));
        }
    }
}

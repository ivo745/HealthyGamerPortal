using HealthyGamerPortal.Common.ViewModels.Api;
using HealthyGamerPortal.Common.ViewModels.News;
using Refit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthyGamerPortal.WEB.Interfaces
{
    [Headers("Accept: application/json")]
    public interface IHealthyGamerPortalNewsApi
    {
        /// <summary>
        /// Gets a IEnumerable of <see cref="ApiResponseModel{NewsItemViewModel}"/>
        /// </summary>
        /// <returns>Returns IEnumerable <see cref="ApiResponseModel{NewsItemViewModel}"/></returns>
        [Get("/api/v1/NewsApi/NewsManagement/GetManyShortNews")]
        Task<ApiResponseModel<IEnumerable<NewsItemViewModel>>> GetManyShortNews();

        /// <summary>
        /// Gets a IEnumerable of <see cref="ApiResponseModel{NewsItemViewModel}"/>
        /// </summary>
        /// <returns>Returns IEnumerable <see cref="ApiResponseModel{NewsItemViewModel}"/></returns>
        [Get("/api/v1/NewsApi/NewsManagement/GetManyNews")]
        Task<ApiResponseModel<IEnumerable<NewsItemViewModel>>> GetManyNews();

        /// <summary>
        /// Gets a <see cref="ApiResponseModel{NewsItemViewModel}"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns><see cref="ApiResponseModel{NewsItemViewModel}"/></returns>
        [Get("/api/v1/NewsApi/NewsManagement/GetSingleNewsById")]
        Task<ApiResponseModel<NewsItemViewModel>> GetSingleNewsById(Guid Id);

        /// <summary>
        /// Gets a <see cref="ApiResponseModel{DeleteNewsItemViewModel}"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns><see cref="ApiResponseModel{DeleteNewsItemViewModel}"/></returns>
        [Get("/api/v1/NewsApi/NewsManagement/GetSingleDeleteNewsById")]
        Task<ApiResponseModel<DeleteNewsItemViewModel>> GetSingleDeleteNewsById(Guid Id);

        /// <summary>
        /// Gets a <see cref="ApiResponseModel{EditNewsItemViewModel}"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns><see cref="ApiResponseModel{EditNewsItemViewModel}"/></returns>
        [Get("/api/v1/NewsApi/NewsManagement/GetSingleEditNewsById")]
        Task<ApiResponseModel<EditNewsItemViewModel>> GetSingleEditNewsById(Guid Id);

        /// <summary>
        /// Gets a <see cref="ApiResponseModel{bool}"/>
        /// </summary>
        /// <param name="model"></param>
        /// <returns><see cref="ApiResponseModel{bool}"/></returns>
        [Post("/api/v1/NewsApi/NewsManagement/CreateSingleNews")]
        Task<ApiResponseModel<bool>> CreateSingleNews(CreateNewsItemViewModel model);

        /// <summary>
        /// Gets a <see cref="ApiResponseModel{bool}"/>
        /// </summary>
        /// <param name="model"></param>
        /// <returns><see cref="ApiResponseModel{bool}"/></returns>
        [Post("/api/v1/NewsApi/NewsManagement/EditSingleNews")]
        Task<ApiResponseModel<bool>> EditSingleNews(EditNewsItemViewModel model);

        /// <summary>
        /// Gets a <see cref="ApiResponseModel{bool}"/>
        /// </summary>
        /// <param name="model"></param>
        /// <returns><see cref="ApiResponseModel{bool}"/></returns>
        [Post("/api/v1/NewsApi/NewsManagement/DeleteSingleNews")]
        Task<ApiResponseModel<bool>> DeleteSingleNews(DeleteNewsItemViewModel model);
    }
}
using HealthyGamerPortal.Common.ViewModels.Api;
using HealthyGamerPortal.Common.ViewModels.LocalizationRecords;
using Refit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthyGamerPortal.WEB.Interfaces
{
    [Headers("Accept: application/json")]
    public interface IHealthyGamerLocalizationRecordsApi
    {
        /// <summary>
        /// Gets a List <see cref="ApiResponseModel{LocalizationRecordViewModel}"/>
        /// </summary>
        /// <returns>Returns List <see cref="ApiResponseModel{LocalizationRecordViewModel}"/></returns>
        [Get("/api/v1/LocalizationRecordApi/LocalizationRecord/GetManyLocalizationRecords")]
        Task<ApiResponseModel<List<LocalizationRecordViewModel>>> GetManyLocalizationRecords();

        /// <summary>
        /// Gets a single <see cref="ApiResponseModel{LocalizationRecordViewModel}"/>
        /// </summary>
        /// <param name="Id"></param>
        /// <returns><see cref="ApiResponseModel{LocalizationRecordViewModel}"/></returns>
        [Get("/api/v1/LocalizationRecordApi/LocalizationRecord/GetSingleLocalizationRecordById")]
        Task<ApiResponseModel<LocalizationRecordViewModel>> GetSingleLocalizationRecordById(Guid Id);

        /// <summary>
        /// edits a single <see cref="ApiResponseModel{bool}"/>
        /// </summary>
        /// <param name="model"></param>
        /// <returns><see cref="ApiResponseModel{bool}"/></returns>
        [Post("/api/v1/LocalizationRecordApi/LocalizationRecord/EditSingleLocalizationRecord")]
        Task<ApiResponseModel<bool>> EditSingleLocalizationRecord(LocalizationRecordViewModel model);
    }
}
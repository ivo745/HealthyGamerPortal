using HealthyGamerPortal.Common.ViewModels.LocalizationRecords;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthyGamerPortal.Business.Interfaces
{
    public interface ILocalizationRecordService
    {

        /// <summary>
        /// Gets <see cref="List{LocalizationRecordViewModel}"/>
        /// </summary>
        /// <returns>Returns <see cref="List{LocalizationRecordViewModel}"/></returns>
        Task<List<LocalizationRecordViewModel>> GetManyLocalizationRecords();

        /// <summary>
        /// Gets a single <see cref="LocalizationRecordViewModel"/>
        /// </summary>
        /// <param name="Id"></param>
        /// <returns><see cref="LocalizationRecordViewModel"/></returns>
        Task<LocalizationRecordViewModel> GetSingleLocalizationRecordById(Guid id);

        /// <summary>
        /// Edits a Single Localization Records in Database from <see cref="LocalizationRecordViewModel"/>
        /// </summary>
        /// <param name="model"></param>
        /// <returns><see cref="bool"/></returns>
        Task<bool> EditSingleLocalizationRecord(LocalizationRecordViewModel model);
    }
}

using AutoMapper;
using HealthyGamerPortal.Business.Interfaces;
using HealthyGamerPortal.Common.ViewModels.LocalizationRecords;
using HealthyGamerPortal.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthyGamerPortal.Business.Services
{
    public class LocalizationRecordService : ILocalizationRecordService
    {
        private readonly IMapper _mapper;
        private readonly HealthyGamerPortalDbContext _healthyGamerPortalDbContext;

        /// <summary>
        /// Create a new instance of the <see cref="ApplicationUserService"/> with the needed dependencies.
        /// </summary>
        /// <param name="mapper">An instance of the <see cref="IMapper"/> interface, for automatically mapping different objects to each other.</param>
        /// <param name="healthyGamerPortalDbContext"></param>
        public LocalizationRecordService(IMapper mapper, HealthyGamerPortalDbContext healthyGamerPortalDbContext)
        {
            _mapper = mapper;
            _healthyGamerPortalDbContext = healthyGamerPortalDbContext;
        }

        /// <summary>
        /// Gets <see cref="IEnumerable{LocalizationRecords}"/>
        /// </summary>
        /// <returns>Returns <see cref="IEnumerable{LocalizationRecords}"/></returns>
        public async Task<List<LocalizationRecordViewModel>> GetManyLocalizationRecords()
        {
            return _mapper.Map<List<LocalizationRecordViewModel>>(await _healthyGamerPortalDbContext.LocalizationRecord.ToListAsync());
        }

        /// <summary>
        /// Gets a single <see cref="LocalizationRecordViewModel"/>
        /// </summary>
        /// <param name="LocalizationRecordsId"></param>
        /// <returns><see cref="LocalizationRecordViewModel"/></returns>
        public async Task<LocalizationRecordViewModel> GetSingleLocalizationRecordById(Guid LocalizationRecordsId)
        {
            return _mapper.Map<LocalizationRecordViewModel>(await _healthyGamerPortalDbContext.LocalizationRecord.FirstAsync(I => I.Id == LocalizationRecordsId));
        }

        /// <summary>
        /// Edits a Single LocalizationRecords <see cref="LocalizationRecordViewModel"/>
        /// </summary>
        /// <param name="model"></param>
        /// <returns><see cref="bool"/></returns>
        public async Task<bool> EditSingleLocalizationRecord(LocalizationRecordViewModel model)
        {
            var existingLocalizationRecords = await _healthyGamerPortalDbContext.LocalizationRecord.AsNoTracking().FirstOrDefaultAsync(i => i.Id == model.Id);
            if (existingLocalizationRecords != null)
            {
                existingLocalizationRecords.Text = model.Text;
                _healthyGamerPortalDbContext.Update(existingLocalizationRecords);
                await _healthyGamerPortalDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}

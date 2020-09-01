using HealthyGamerPortal.Common.ViewModels.Api;
using HealthyGamerPortal.Common.ViewModels.Company;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthyGamerPortal.WEB.Interfaces
{
    [Headers("Accept: application/json")]
    public interface IHealthyGamerPortalCompanyApi
    {
        /// <summary>
        /// Gets a IEnumerable of <see cref="ApiResponseModel{NewsItemViewModel}"/>
        /// </summary>
        /// <returns>Returns IEnumerable <see cref="ApiResponseModel{NewsItemViewModel}"/></returns>
        [Get("/api/v1/CompanyApi/Company/GetManyCompanyBuilding")]
        Task<ApiResponseModel<IEnumerable<CompanyBuildingViewModel>>> GetManyCompanyBuilding();
    }
}
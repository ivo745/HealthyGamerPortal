using HealthyGamerPortal.Common.ViewModels.Company;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthyGamerPortal.Business.Interfaces
{

    public interface ICompanyService
    {
        /// <summary>
        /// Gets a IEnumerable of <see cref="ApiResponseModel{CompanyBuildingViewModel}"/>
        /// From database
        /// </summary>
        /// <returns>Returns IEnumerable <see cref="ApiResponseModel{CompanyBuildingViewModel}"/></returns>
        Task<IEnumerable<CompanyBuildingViewModel>> GetManyCompanyBuilding();
    }
}

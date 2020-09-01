using AutoMapper;
using HealthyGamerPortal.Business.Interfaces;
using HealthyGamerPortal.Common.ViewModels.Company;
using HealthyGamerPortal.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthyGamerPortal.Business.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IMapper _mapper;
        private readonly HealthyGamerPortalDbContext _healthyGamerPortalDbContext;

        /// <summary>
        /// Create a new instance of the <see cref="CompanyService"/> with the needed dependencies.
        /// </summary>
        /// <param name="mapper">An instance of the <see cref="IMapper"/> interface, for automatically mapping different objects to each other.</param>
        /// <param name="healthyGamerPortalDbContext"></param>
        public CompanyService(IMapper mapper, HealthyGamerPortalDbContext healthyGamerPortalDbContext)
        {
            _mapper = mapper;
            _healthyGamerPortalDbContext = healthyGamerPortalDbContext;
        }

        /// <summary>
        /// Gets a IEnumerable of <see cref="ApiResponseModel{CompanyBuildingViewModel}"/>
        /// From database
        /// </summary>
        /// <returns>Returns IEnumerable <see cref="ApiResponseModel{CompanyBuildingViewModel}"/></returns>
        public async Task<IEnumerable<CompanyBuildingViewModel>> GetManyCompanyBuilding()
        {
            var manyCompanyBuidling = new List<CompanyBuildingViewModel>();

            return manyCompanyBuidling;
        }
    }
}

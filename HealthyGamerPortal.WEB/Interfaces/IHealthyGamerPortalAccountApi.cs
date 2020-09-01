using HealthyGamerPortal.Common.Enums;
using HealthyGamerPortal.Common.Models;
using HealthyGamerPortal.Common.ViewModels.Api;
using HealthyGamerPortal.Common.ViewModels.Api.Authentication;
using HealthyGamerPortal.Common.ViewModels.Login;
using Microsoft.AspNetCore.Mvc;
using Refit;
using System.Threading.Tasks;

namespace HealthyGamerPortal.WEB.Interfaces
{
    [Headers("Accept: application/json")]
    public interface IHealthyGamerPortalAccountApi
    {
        /// <summary>
        /// Gets a IEnumerable <see cref="ApiResponseModel{AuthenticationModel}"/>
        /// </summary>
        /// <returns>Returns IEnumerable <see cref="ApiResponseModel{AuthenticationModel}"/></returns>
        [Get("/api/v1/AccountApi/Account/IsBasicAccount")]
        Task<ApiResponseModel<AccountType>> IsBasicAccount(EncryptedMessage encryptedMessage);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Post("/api/v1/AccountApi/Account/Login")]
        Task<ApiResponseModel<ApiLoginResult>> Login([FromBody]ApiLoginModel model);
    }
}
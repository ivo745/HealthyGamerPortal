using HealthyGamerPortal.Common.ViewModels.Api;
using HealthyGamerPortal.Common.ViewModels.Login;
using HealthyGamerPortal.Common.ViewModels.Users;
using Microsoft.AspNetCore.Mvc;
using Refit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthyGamerPortal.WEB.Interfaces
{
    [Headers("Accept: application/json")]
    public interface IHealthyGamerPortalUserApi
    {
        /// <summary>
        /// Gets a IEnumerable <see cref="ApiResponseModel{ApplicationUserViewModel}"/>
        /// </summary>
        /// <returns>Returns IEnumerable <see cref="ApiResponseModel{ApplicationUserViewModel}"/></returns>
        [Get("/api/v1/ApplicationUserApi/ApplicationUser/GetManyUsers")]
        Task<ApiResponseModel<IEnumerable<ApplicationUserViewModel>>> GetManyUsers();

        /// <summary>
        /// Gets a <see cref="ApiResponseModel{ApplicationUserViewModel}"/>
        /// </summary>
        /// <param name="Id"></param>
        /// <returns><see cref="ApiResponseModel{ApplicationUserViewModel}"/></returns>
        [Get("/api/v1/ApplicationUserApi/ApplicationUser/GetSingleApplicationUserById")]
        Task<ApiResponseModel<ApplicationUserViewModel>> GetSingleApplicationUserById(Guid id);

        /// <summary>
        /// Gets a <see cref="ApiResponseModel{ApplicationUserViewModel}"/>
        /// </summary>
        /// <param name="Id"></param>
        /// <returns><see cref="ApiResponseModel{ApplicationUserViewModel}"/></returns>
        [Get("/api/v1/ApplicationUserApi/ApplicationUser/GetSingleApplicationUserByName")]
        Task<ApiResponseModel<ApplicationUserViewModel>> GetSingleApplicationUserByName(string name);

        /// <summary>
        /// Gets a <see cref="ApiResponseModel{EditApplicationUserViewModel}"/>
        /// </summary>
        /// <param name="Id"></param>
        /// <returns><see cref="ApiResponseModel{EditApplicationUserViewModel}"/></returns>
        [Get("/api/v1/ApplicationUserApi/ApplicationUser/GetSingleEditApplicationUserById")]
        Task<ApiResponseModel<EditApplicationUserViewModel>> GetSingleEditApplicationUserById(Guid id);

        /// <summary>
        /// Gets a <see cref="ApiResponseModel{DeleteApplicationUserViewModel}"/>
        /// </summary>
        /// <param name="Id"></param>
        /// <returns><see cref="ApiResponseModel{DeleteApplicationUserViewModel}"/></returns>
        [Get("/api/v1/ApplicationUserApi/ApplicationUser/GetSingleDeleteApplicationUserById")]
        Task<ApiResponseModel<DeleteApplicationUserViewModel>> GetSingleDeleteApplicationUserById(Guid id);

        /// <summary>
        /// Gets a <see cref="ApiResponseModel{bool}"/>
        /// </summary>
        /// <param name="model"></param>
        /// <returns><see cref="ApiResponseModel{bool}"/></returns>
        [Post("/api/v1/ApplicationUserApi/ApplicationUser/CreateSingleApplicationUser")]
        Task<ApiResponseModel<bool>> CreateSingleApplicationUser(CreateApplicationUserViewModel model);

        /// <summary>
        /// Gets a <see cref="ApiResponseModel{bool}"/>
        /// </summary>
        /// <param name="model"></param>
        /// <returns><see cref="ApiResponseModel{bool}"/></returns>
        [Post("/api/v1/ApplicationUserApi/ApplicationUser/EditSingleApplicationUser")]
        Task<ApiResponseModel<bool>> EditSingleApplicationUser(EditApplicationUserViewModel model);

        /// <summary>
        /// Gets a <see cref="ApiResponseModel{bool}"/>
        /// </summary>
        /// <param name="model"></param>
        /// <returns><see cref="ApiResponseModel{bool}"/></returns>
        [Post("/api/v1/ApplicationUserApi/ApplicationUser/DeleteSingleApplicationUser")]
        Task<ApiResponseModel<bool>> DeleteSingleApplicationUser(DeleteApplicationUserViewModel model);

        /// <summary>
        /// Gets a LoginModel of <see cref="LoginModel{LoginModel}"/>
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>Returns LoginModel <see cref="ApiResponseModel{LoginModel}"/></returns>
        [Post("/api/v1/ApplicationUserApi/ApplicationUser/Authenticate")]
        Task<ApiResponseModel<BasicAuthenticationResult>> Authenticate([FromBody] EncryptedBasicLoginModel model);
    }
}
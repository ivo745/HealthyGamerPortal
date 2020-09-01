using HealthyGamerPortal.Common.ViewModels.Login;
using HealthyGamerPortal.Common.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthyGamerPortal.Business.Interfaces
{
    public interface IApplicationUserService
    {

        /// <summary>
        /// Gets <see cref="IEnumerable{ApplicationUserViewModel}"/>
        /// </summary>
        /// <returns>Returns <see cref="IEnumerable{ApplicationUserViewModel}"/></returns>
        Task<IEnumerable<ApplicationUserViewModel>> GetManyApplicationUser();

        /// <summary>
        /// Gets a single <see cref="ApplicationUserViewModel"/>
        /// </summary>
        /// <param name="ApplicationUserId"></param>
        /// <returns><see cref="ApplicationUserViewModel"/></returns>
        Task<ApplicationUserViewModel> GetSingleApplicationUserById(Guid id);

        /// <summary>
        /// Gets a single <see cref="ApplicationUserViewModel"/>
        /// </summary>
        /// <param name="ApplicationUserName"></param>
        /// <returns><see cref="ApplicationUserViewModel"/></returns>
        Task<ApplicationUserViewModel> GetSingleApplicationUserByName(string name);

        /// <summary>
        /// Creates a Single ApplicationUser in the Database from <see cref="CreateApplicationUserViewModel"/>
        /// </summary>
        /// <param name="model"></param>
        /// <returns><see cref="bool"/></returns>
        Task<bool> CreateSingleApplicationUser(CreateApplicationUserViewModel model);

        /// <summary>
        /// Edits a Single ApplicationUser in Database from <see cref="EditApplicationUserViewModel"/>
        /// </summary>
        /// <param name="model"></param>
        /// <returns><see cref="bool"/></returns>
        Task<bool> EditSingleApplicationUser(EditApplicationUserViewModel model);

        /// <summary>
        /// Deletes Single ApplicationUser From Database by <see cref="DeleteApplicationUserViewModel"/>
        /// </summary>
        /// <param name="model"></param>
        /// <returns><see cref="bool"/></returns>
        Task<bool> DeleteSingleApplicationUser(DeleteApplicationUserViewModel model);

        /// <summary>
        /// Deletes Single ApplicationUser From Database by <see cref="DeleteApplicationUserViewModel"/>
        /// </summary>
        /// <param name="model"></param>
        /// <returns><see cref="bool"/></returns>
        Task<BasicAuthenticationResult> Authenticate(EncryptedBasicLoginModel model);
    }
}

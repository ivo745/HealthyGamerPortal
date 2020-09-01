using HealthyGamerPortal.Common.Models;
using HealthyGamerPortal.Common.ViewModels.Api.Authentication;
using System.Threading.Tasks;

namespace HealthyGamerPortal.API.Interfaces
{
    /// <summary>
    /// Interface that describes authentication actions that can be taken on an API.
    /// </summary>
    public interface IAuthentication
    {
        /// <summary>
        /// Login a user using the provided <paramref name="model"/>.
        /// </summary>
        /// <returns>The login result returned by the API.</returns>
        /// <param name="model">The LoginModel for the user.</param>
        Task<ApiLoginResult> LoginAsync(ApiLoginModel model);
    }
}
using HealthyGamerPortal.Common.Enums;
using HealthyGamerPortal.Common.ViewModels.Login;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HealthyGamerPortal.Business.Interfaces
{
    public interface IAccountService
    {
        /// <summary>
        /// Gets the <see cref="AccountType"/> based on the presence of 'Azure' in the password field.
        /// </summary>
        /// <returns>Returns <see cref="AccountType"/></returns>
        Task<AccountType> IsBasicAccount([FromQuery]EncryptedMessage encryptedMessage);
    }
}

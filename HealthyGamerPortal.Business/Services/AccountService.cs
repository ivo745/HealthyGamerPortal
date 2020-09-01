using AutoMapper;
using HealthyGamerPortal.Business.Interfaces;
using HealthyGamerPortal.Common.Cryptography;
using HealthyGamerPortal.Common.Enums;
using HealthyGamerPortal.Common.ViewModels.Login;
using HealthyGamerPortal.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HealthyGamerPortal.Business.Services
{
    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly HealthyGamerPortalDbContext _healthyGamerPortalDbContext;

        /// <summary>
        /// Create a new instance of the <see cref="AccountService"/> with the needed dependencies.
        /// </summary>
        /// <param name="mapper">An instance of the <see cref="IMapper"/> interface, for automatically mapping different objects to each other.</param>
        /// <param name="healthyGamerPortalDbContext"></param>
        public AccountService(IMapper mapper, HealthyGamerPortalDbContext healthyGamerPortalDbContext)
        {
            _mapper = mapper;
            _healthyGamerPortalDbContext = healthyGamerPortalDbContext;
        }

        /// <summary>
        /// Retrieves account type from ApplicationUser based on matching username.
        /// </summary>
        /// <returns><see cref="AccountType"/></returns>
        public async Task<AccountType> IsBasicAccount(EncryptedMessage encryptedMessage)
        {
            // check password == Azure 
            var result = await _healthyGamerPortalDbContext.ApplicationUsers.FirstOrDefaultAsync(I => I.Email == Rfc7905.DecryptText(encryptedMessage.Length, encryptedMessage.Text) && I.Password == "Discord");
            if (result != null)
                return AccountType.Discord;
            return AccountType.Basic;
        }
    }
}

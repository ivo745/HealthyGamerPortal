using AutoMapper;
using HealthyGamerPortal.Business.Interfaces;
using HealthyGamerPortal.Common.Cryptography;
using HealthyGamerPortal.Common.ViewModels.Login;
using HealthyGamerPortal.Common.ViewModels.Users;
using HealthyGamerPortal.Data;
using HealthyGamerPortal.Models;
using Microsoft.EntityFrameworkCore;
using NSec.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthyGamerPortal.Business.Services
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly IMapper _mapper;
        private readonly HealthyGamerPortalDbContext _healthyGamerPortalDbContext;

        /// <summary>
        /// Create a new instance of the <see cref="ApplicationUserService"/> with the needed dependencies.
        /// </summary>
        /// <param name="mapper">An instance of the <see cref="IMapper"/> interface, for automatically mapping different objects to each other.</param>
        /// <param name="healthyGamerPortalDbContext"></param>
        public ApplicationUserService(IMapper mapper, HealthyGamerPortalDbContext healthyGamerPortalDbContext)
        {
            _mapper = mapper;
            _healthyGamerPortalDbContext = healthyGamerPortalDbContext;
        }

        /// <summary>
        /// Gets <see cref="IEnumerable{ApplicationUserViewModel}"/>
        /// </summary>
        /// <returns>Returns <see cref="IEnumerable{ApplicationUserViewModel}"/></returns>
        public async Task<IEnumerable<ApplicationUserViewModel>> GetManyApplicationUser()
        {
            var applicationUsersList = await _healthyGamerPortalDbContext.ApplicationUsers.ToListAsync();
            var applicationUserViewsList = new List<ApplicationUserViewModel>();
            foreach (var applicationUser in applicationUsersList)
            {
                applicationUserViewsList.Add(_mapper.Map<ApplicationUserViewModel>(applicationUser));
            }
            return applicationUserViewsList;
        }

        /// <summary>
        /// Gets a single <see cref="ApplicationUserViewModel"/>
        /// </summary>
        /// <param name="ApplicationUserId"></param>
        /// <returns><see cref="ApplicationUserViewModel"/></returns>
        public async Task<ApplicationUserViewModel> GetSingleApplicationUserById(Guid ApplicationUserId)
        {
            return _mapper.Map<ApplicationUserViewModel>(await _healthyGamerPortalDbContext.ApplicationUsers.FirstAsync(I => I.Id == ApplicationUserId));
        }

        /// <summary>
        /// Gets a single <see cref="ApplicationUserViewModel"/>
        /// </summary>
        /// <param name="ApplicationUserName"></param>
        /// <returns><see cref="ApplicationUserViewModel"/></returns>
        public async Task<ApplicationUserViewModel> GetSingleApplicationUserByName(string ApplicationUserName)
        {
            return _mapper.Map<ApplicationUserViewModel>(await _healthyGamerPortalDbContext.ApplicationUsers.FirstAsync(I => I.Email == ApplicationUserName));
        }

        /// <summary>
        /// Creates a Single <see cref="ApplicationUser"/> in the Database from <see cref="CreateApplicationUserViewModel"/>
        /// </summary>
        /// <param name="model"></param>
        /// <returns><see cref="bool"/></returns>
        public async Task<bool> CreateSingleApplicationUser(CreateApplicationUserViewModel model)
        {
            if (!_healthyGamerPortalDbContext.ApplicationUsers.Any(i => i.Email == model.Email))
            {
                ApplicationUser applicationUser = _mapper.Map<ApplicationUser>(model);
                CreatePasswordHash(Encoding.UTF8.GetBytes("NotDiscord"), out string salt, out string hashedPassword);

                applicationUser.Password = hashedPassword;
                applicationUser.Salt = salt;
                _healthyGamerPortalDbContext.ApplicationUsers.Add(applicationUser);
                await _healthyGamerPortalDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Edits a Single <see cref="ApplicationUser"/> in Database from <see cref="EditApplicationUserViewModel"/>
        /// </summary>
        /// <param name="model"></param>
        /// <returns><see cref="bool"/></returns>
        public async Task<bool> EditSingleApplicationUser(EditApplicationUserViewModel model)
        {
            var existingApplicationUser = await _healthyGamerPortalDbContext.ApplicationUsers.AsNoTracking().FirstOrDefaultAsync(i => i.Id == model.Id);
            if (existingApplicationUser != null)
            {
                ApplicationUser applicationUser = _mapper.Map<ApplicationUser>(model);
                applicationUser.Password = existingApplicationUser.Password;
                _healthyGamerPortalDbContext.Update(applicationUser);
                await _healthyGamerPortalDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Deletes Single <see cref="ApplicationUser"/> From Database by <see cref="DeleteApplicationUserViewModel"/>
        /// </summary>
        /// <param name="model"></param>
        /// <returns><see cref="bool"/></returns>
        public async Task<bool> DeleteSingleApplicationUser(DeleteApplicationUserViewModel model)
        {
            if (_healthyGamerPortalDbContext.ApplicationUsers.AsNoTracking().First(i => i.Id == model.Id) != null)
            {
                ApplicationUser DeleteApplicationUser = _mapper.Map<ApplicationUser>(model);
                _healthyGamerPortalDbContext.Remove(DeleteApplicationUser);
                await _healthyGamerPortalDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Outputs hash and salt as <see cref="string"/> based on user password in <see cref="HMACSHA512"/> format to store in DB.
        /// </summary>
        private void CreatePasswordHash(ReadOnlySpan<byte> data, out string salt, out string hashedPassword)
        {
            var algorithm = MacAlgorithm.HmacSha512;
            var keyCreationParameters = new KeyCreationParameters()
            {
                ExportPolicy = KeyExportPolicies.AllowPlaintextArchiving
            };

            using var key = Key.Create(algorithm, keyCreationParameters);
            salt = Convert.ToBase64String(key.Export(KeyBlobFormat.NSecSymmetricKey));
            hashedPassword = Convert.ToBase64String(algorithm.Mac(key, data));
        }

        /// <summary>
        /// Compares stored hash as <see cref="T:byte[]"/> with salt based on sequence equality in <see cref="HMACSHA512"/> format.
        /// </summary>
        private bool VerifyPasswordHash(ReadOnlySpan<byte> blob, ReadOnlySpan<byte> data, ReadOnlySpan<byte> mac)
        {
            var algorithm = MacAlgorithm.HmacSha512;
            using (var key = Key.Import(MacAlgorithm.HmacSha512, blob, KeyBlobFormat.NSecSymmetricKey))
            {
                if (algorithm.Verify(key, data, mac))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Retrieves <see cref="ApplicationUser"/> from DB and perform password check for basic login.
        /// </summary>
        /// <returns><see cref="BasicAuthenticationResult"/></returns>
        public async Task<BasicAuthenticationResult> Authenticate(EncryptedBasicLoginModel model)
        {
            var user = await _healthyGamerPortalDbContext.ApplicationUsers.FirstOrDefaultAsync(
                X => X.Email == Rfc7905.DecryptText(model.Email.Length, model.Email.Text));

            // check if user exists
            if (user == null)
                return null;

            // check if password is correct
            if (!VerifyPasswordHash(Convert.FromBase64String(user.Salt), Encoding.UTF8.GetBytes(Rfc7905.DecryptText(model.Password.Length, model.Password.Text)),
                Convert.FromBase64String(user.Password)))
                return null;

            //Retrieve roles from DB
            BasicAuthenticationResult result = new BasicAuthenticationResult { Name = user.Email, Roles = new string[] { "Sad", "NotSad" } };

            // authentication successful
            return result;
        }
    }
}

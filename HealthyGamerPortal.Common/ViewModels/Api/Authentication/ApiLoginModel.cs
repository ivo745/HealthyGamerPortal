
using HealthyGamerPortal.Common.ViewModels.Api.Base;

namespace HealthyGamerPortal.Common.ViewModels.Api.Authentication
{
    public class ApiLoginModel : BaseViewModel
    {
        /// <summary>
        /// The username of the user that wants to login.
        /// As an encrypted Base64 string.
        /// </summary>
        /// <example>Yn1z8yUwOso5BYJCp0HevE+uZpJhy/kTMdxaA+g0tvUMA/Y59eTqy75h0c+ARDUpjtlCD2rO3+vh8EHdyhZ5b1TrPsJF6ASQBaIQPR95Yp9R/dwGnicG41fSpMwI4WcH5mEtGB2rOSD7TVgsR0tCDwRyDP/zNHZHPiPPH/c4vz/KLWk2MzmOzNpKNWyrjqU1iJQDDNby2keC18WX8ldrVEK4GvTchs+PNtyXf2dDAXr+/f620zOitSqft69SjmnwqF0ul1AJuKz6NUzVS7ZEiSAcKRYRcXzgFisjNMHLhAX/szV6yBsDI2w/YqRZhhMeUBxBphYo8bZ0eZkEI9IlEQ==</example>
        public string Username { get; set; }

        public string Token { get; set; }
    }
}

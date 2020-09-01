
namespace HealthyGamerPortal.Common.Models
{
    /// <summary>
    /// Class that holds the results of a login action.
    /// </summary>
    public class ApiLoginResult
    {
        /// <summary>
        /// The authentication token that can be used to access this API.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// The time in miliseconds from Epoch when <see cref="Token"/> expires.
        /// </summary>
        public long Expires { get; set; }

        /// <summary>
        /// The username that has been logged in.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Indicates if the login action has failed due to an error.
        /// </summary>
        public bool HasError { get; set; }
    }
}
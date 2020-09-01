using HealthyGamerPortal.Common.Enums;
using Newtonsoft.Json;

namespace HealthyGamerPortal.Common.ViewModels.Api.Authentication
{
    /// <summary>
    /// Data transfer object that holds authentication data.
    /// </summary>
    public class AuthenticationModel
    {
        /// <summary>
        /// The authentication token that can be used to access this API.
        /// </summary>
        /// <example>eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJtY3MtdG9rZW4iOiJ5VVQ3YWNLQ2hveUtQeklGNFFLVnZsc3FmejlFdklMb3I3ck96WDAxNUpSSjd6Nm0xZWUzSFR5aUZXODFmT3ZWWjEiLCJuYmYiOjE1NjA5MzAzODUsImV4cCI6MTU2MDkzMDY4NSwiaWF0IjoxNTYwOTMwMzg1LCJpc3MiOiJsb2NhbGhvc3Q6NTAwMSJ9.6Aqc08Ji9_Q8BKTnGR5tPbU5c2zdO_88djxHG82S4Do</example>
        [JsonProperty("token")]
        public string AuthenticationToken { get; set; }

        /// <summary>
        /// The time in miliseconds from Epoch when <see cref="AuthenticationToken"/> expires.
        /// </summary>
        /// <example>1337</example>
        [JsonProperty("expires")]
        public long ExpiresIn { get; set; }

        /// <summary>
        /// The username that has been logged in.
        /// </summary>
        /// <example>test@gmail.com</example>
        [JsonProperty("username")]
        public string Username { get; set; }

        /// <summary>
        /// The username that has been logged in.
        /// </summary>
        /// <example>test@gmail.com</example>
        [JsonProperty("accounttype")]
        public AccountType AccountType { get; set; }
    }
}

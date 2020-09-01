using Microsoft.AspNetCore.Authorization;

namespace HealthyGamerPortal.API.Interfaces
{
    /// <summary>
    /// Interface that defines actions for handling authentication tokens.
    /// With use of an external API token that will be wrapped in our own authentication token.
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Create a new authentication token wrapping an existing token from the external API.
        /// </summary>
        /// <param name="apiToken">The existing token from the API.</param>
        /// <returns>A string representation of the authentication token.</returns>
        string GenerateToken(string apiToken);

        /// <summary>
        /// Extract the external API token that was wrapped into our own authentication token.
        /// This method takes the token from the current user that is calling an endpoint that is decorated with the <see cref="AuthorizeAttribute"/>
        /// </summary>
        /// <returns>The original external API token that was wrapped into our own authentication token, if any.</returns>
        string GetApiToken();
    }
}
namespace HealthyGamerPortal.API
{
    /// <summary>
    /// Constants class that defines the keys using for the environment variables.
    /// </summary>
    public static class EnvironmentConstants
    {
        /// <summary>
        /// Defines the key that holds the secret key for signing and encrypting the JWT tokens.
        /// This key is also used to validate tokens sent to this API.
        /// </summary>
        public const string JwtSigningSecret = "JWT_SIGNING_SECRET";

        /// <summary>
        /// Defines the key that holds the information about how long a JWT token should be valid for defined in minutes.
        /// </summary>
        public const string JwtTokenLifetimeInMinutes = "JWT_TOKEN_LIFETIME_MINUTES";

        /// <summary>
        /// Defines the key that holds the name of the server that issues the JWT tokens.
        /// The issuer is also checked when validating tokens sent to this API.
        /// </summary>
        public const string JwtTokenValidIssuer = "JWT_TOKEN_ISSUER";

        /// <summary>
        /// Defines the key that holds the base URL pointing to the external API.
        /// </summary>
        public const string ExternalApiBaseUrl = "EXTERNAL_API_URL";
    }

    /// <summary>
    /// Constants class that defines the keys used for the configuration file.
    /// </summary>
    public static class ConfigurationConstants
    {
        /// <summary>
        /// Defines the key that holds the secret key for signing and encrypting the JWT tokens.
        /// This key is also used to validate tokens sent to this API.
        /// </summary>
        public const string JwtSigningSecret = "JWT:JWTSigningSecret";

        /// <summary>
        /// Defines the key that holds the information about how long a JWT token should be valid for defined in minutes.
        /// </summary>
        public const string JwtTokenLifetimeInMinutes = "JWT:JWTLifetimeInMinutes";

        /// <summary>
        /// Defines the key that holds the name of the server that issues the JWT tokens.
        /// The issuer is also checked when validating tokens sent to this API.
        /// </summary>
        public const string JwtTokenValidIssuer = "JWT:JWTTokenIssuer";

        /// <summary>
        /// Defines the key that holds the base URL pointing to the external API.
        /// </summary>
        public const string ExternalApiBaseUrl = "Urls:ExternalApiBaseUrl";
    }

    /// <summary>
    /// Constants class that defines constants for custom claims in the authentication token.
    /// </summary>
    public static class ClaimConstants
    {
        /// <summary>
        /// Defines the name of the custom claim that defines the token of the external API.
        /// </summary>
        public const string ExternalTokenClaimName = "mcs-token";
    }
}
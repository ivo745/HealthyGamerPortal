using HealthyGamerPortal.Common.Cryptography;
using HealthyGamerPortal.Common.ViewModels.Login;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Text;
using System.Threading.Tasks;

namespace HealthyGamerPortal.WEB.JwtProvider
{
    /// <summary>
    /// Basic authentication endpoint.
    /// </summary>
    public class JwtProviderMiddleware : JwtProviderController
    {
        private readonly RequestDelegate _next;
        private readonly JwtProviderOptions _options;
        private readonly IAntiforgery _antiforgery;

        public JwtProviderMiddleware(RequestDelegate next, IOptions<JwtProviderOptions> options, IAntiforgery antiforgery)
        {
            _next = next;
            _options = options.Value;
            _jwtOptions = options.Value;
            _antiforgery = antiforgery;
        }

        /// <summary>
        /// Scout every request for basic login path and initiate authentication.
        /// </summary>
        public async Task InvokeAsync(HttpContext httpContext)
        {
            HttpRequest request = httpContext.Request;
            HttpResponse response = httpContext.Response;

            // If the request path doesn't match, skip
            if (request.Path.Equals(_options.Path, StringComparison.Ordinal))
            {
                // Slow connection can cause collision between concurring tasks, delay until finished
                while (!_antiforgery.ValidateRequestAsync(httpContext).IsCompleted)
                {
                    //wait a bit please :D
                }

                // Request must be POST with matching antiforgery token
                if (!request.Method.Equals("POST") || !_antiforgery.ValidateRequestAsync(httpContext).IsCompletedSuccessfully)
                {
                    response.StatusCode = 400;
                }

                if (!httpContext.User.Identity.IsAuthenticated)
                {
                    EncryptedBasicLoginModel model = new EncryptedBasicLoginModel
                    {
                        Email = new EncryptedMessage()
                        {
                            Text = Rfc7905.EncryptText(request.Form["Email"]),
                            Length = Encoding.UTF8.GetBytes(request.Form["Email"]).Length
                        },
                        Password = new EncryptedMessage()
                        {
                            Text = Rfc7905.EncryptText(request.Form["Password"]),
                            Length = Encoding.UTF8.GetBytes(request.Form["Password"]).Length
                        }
                    };

                    _httpContext = httpContext;
                    if (await Authenticate(model))
                    {
                        response.Redirect("/");
                    }
                    else
                    {
                        response.Redirect("/Account/Oauth");
                    }

                    return;
                }
            }

            await _next(httpContext);
        }
    }

    /// <summary>
    /// Extension method used to add the middleware to the HTTP request pipeline.
    /// </summary>
    public static class TokenProviderMiddlewareExtensions
    {
        public static IApplicationBuilder UseTokenProviderMiddleware(this IApplicationBuilder builder, JwtProviderOptions options)
        {
            return builder.UseMiddleware<JwtProviderMiddleware>(Options.Create(options));
        }
    }
}
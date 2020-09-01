using Microsoft.AspNetCore.Authentication;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace HealthyGamerPortal.WEB.Helpers
{
    public class AuthenticatedHttpClientHandler : DelegatingHandler
    {
        public AuthenticatedHttpClientHandler(HttpMessageHandler innerHandler = null)
            : base(innerHandler ?? new HttpClientHandler()) { }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var result = AuthenticationHttpContextExtensions.AuthenticateAsync(System.Web.HttpContextHelper.Current, "Cookies");
            if (result.Result.Succeeded)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("bearer",
                    result.Result.Properties.GetTokenValue("api_token"));
            }
            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }

    public class AnonymousHttpClientHandler : DelegatingHandler
    {
        public AnonymousHttpClientHandler(HttpMessageHandler innerHandler = null)
            : base(innerHandler ?? new HttpClientHandler()) { }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}

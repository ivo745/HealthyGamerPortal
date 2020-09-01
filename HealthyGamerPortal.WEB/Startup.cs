using Discord.OAuth2;
using HealthyGamerPortal.Common.Helpers;
using HealthyGamerPortal.Common.ViewModels.Api.Authentication;
using HealthyGamerPortal.WEB.Interfaces;
using HealthyGamerPortal.WEB.JwtProvider;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NLog.Web;
using Refit;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HealthyGamerPortal.WEB
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private static readonly NLog.Logger _logger = NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();

        private IConfiguration Configuration { get; }

        private void SetDiscordOptions(DiscordOptions options)
        {
            options.SaveTokens = true;
            options.ClientId = Configuration["Discord:ClientId"];
            options.ClientSecret = Configuration["Discord:ClientSecret"];
            options.AppSecret = "6XkegTU_e2AERB9vc7ZxnLxGRxtX_BSd";
            options.CallbackPath = Configuration["Discord:CallbackPath"];
            options.AccessDeniedPath = "/";
            options.SignInScheme = "Cookies";
            options.Scope.Add("identify");
            options.Scope.Add("guilds");
            options.Scope.Add("guilds.join");
            options.Events = new Microsoft.AspNetCore.Authentication.OAuth.OAuthEvents
            {
                OnTicketReceived = async context =>
                {
                }
            };
            options.Validate(DiscordDefaults.AuthenticationScheme);
        }

        [Microsoft.AspNetCore.Authorization.Authorize]
        private async Task<string> GetApiToken(string webToken)
        {
            if (string.IsNullOrEmpty(webToken))
                return null;

            var api = RestService.For<IHealthyGamerPortalAccountApi>(new HttpClient(new Helpers.AnonymousHttpClientHandler()) { BaseAddress = new Uri("https://localhost:5001") });
            var request = await api.Login(new ApiLoginModel
            {
                Token = webToken,
                Username = ""
            });
            return request.Result.Token;
        }

        private async Task<bool> RefreshApiToken(AuthenticationProperties properties)
        {
            string webToken = properties.GetTokenValue("access_token");
            string newApiToken = await GetApiToken(webToken);

            if (!string.IsNullOrEmpty(newApiToken))
            {
                var tokens = properties.GetTokens().Append(new AuthenticationToken()
                {
                    Name = "api_token",
                    Value = newApiToken
                });

                properties.StoreTokens(tokens);
                return true;
            }
            return false;
        }

        private async Task<bool> RefreshWebToken(AuthenticationProperties properties)
        {
            string webToken = properties.GetTokenValue("access_token");

            if (!string.IsNullOrEmpty(webToken))
            {
                if (properties.ExpiresUtc < DateTime.UtcNow)
                {
                    webToken = properties.GetTokenValue("refresh_token");
                    properties.UpdateTokenValue("access_token", webToken);
                    properties.UpdateTokenValue("refresh_token", string.Empty);
                    properties.ExpiresUtc = DateTime.UtcNow.AddHours(8);
                }
                return true;
            }
            return false;
        }

        private void SetCookieOptions(CookieAuthenticationOptions options)
        {
            options.Cookie.Name = "auth-token";
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;

            options.LoginPath = Configuration["Basic:LoginPath"];
            options.AccessDeniedPath = Configuration["Basic:AccessDeniedPath"];
            options.LogoutPath = Configuration["Basic:LogoutPath"];
            options.ClaimsIssuer = Configuration["Basic:ClaimIssuer"];
            options.Events = new CookieAuthenticationEvents
            {
                OnValidatePrincipal = async context =>
                {
                    if (context.Principal.Identity.IsAuthenticated)
                    {
                        var props = context.Properties;
                        // check for out of date web token or missing token
                        if (!await RefreshWebToken(props))
                        {
                            context.RejectPrincipal();

                            await context.HttpContext.SignOutAsync(
                                CookieAuthenticationDefaults.AuthenticationScheme);
                            return;
                        }
                        else
                        {
                            if (props.IssuedUtc < DateTime.UtcNow.AddHours(-2) ||
                                string.IsNullOrEmpty(props.GetTokenValue("api_token")))
                            {
                                if (await RefreshApiToken(props))
                                    context.ShouldRenew = true;
                            }
                        }
                        if (HttpClientHelper.ClientUser().LoginState != Discord.LoginState.LoggedIn)
                        {
                            //await HttpClientHelper.DiscordClientUserLoginAsync(context.Properties.GetTokenValue("access_token"));
                        }
                        await HttpClientHelper.DiscordClientBotLoginAsync();
                    }
                }
            };
            options.Validate();
        }

        private void SetAntiforgeryOptions(AntiforgeryOptions options)
        {
            options.Cookie.Name = "antiforgery-token";
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddRazorPages();

            //this is the Configuration for the localization change things here like the supported culture of the default culture
            services.Configure<RequestLocalizationOptions>(opts =>
            {
                var supportedCultures = new List<CultureInfo>
                {
                    new CultureInfo("de-DE"),
                    new CultureInfo("en-US"),
                };
                opts.DefaultRequestCulture = new RequestCulture("en-US");
                // Formatting numbers, dates, etc.
                opts.SupportedCultures = supportedCultures;
                // UI strings that we have localized.
                opts.SupportedUICultures = supportedCultures;
            });

            services.AddSingleton(typeof(IStringLocalizerFactory), typeof(LocalizerFactory));
            services.AddTransient(typeof(IStringLocalizer<>), typeof(StringLocalizer<>));

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddDiscord(DiscordDefaults.AuthenticationScheme, options => SetDiscordOptions(options))
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options => SetCookieOptions(options));

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential 
                // cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                // requires using Microsoft.AspNetCore.Http;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddAntiforgery(options => SetAntiforgeryOptions(options));

            /*
            services.AddAuthorization(options => {
                options.AddPolicy("Admins", policyBuilder => policyBuilder.RequireClaim("groups", Configuration.GetValue<string>("AzureSecurityGroup:AdminObjectId")));
            });
            services.AddAuthorization(options => {
                options.AddPolicy("Users", policyBuilder => policyBuilder.RequireClaim("groups", Configuration.GetValue<string>("AzureSecurityGroup:UserObjectId")));
            });
            */

            services.AddHsts(options =>
            {
                options.Preload = true;
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(60);
            });

            services.AddHttpsRedirection(options =>
            {
                options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
                options.HttpsPort = 5001;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseExceptionHandler("/Base/Error");
            }
            else
            {
                app.UseExceptionHandler("/Base/Error");
            }

            System.Web.HttpContextHelper.Configure(app.ApplicationServices.GetRequiredService<IHttpContextAccessor>());
            app.UseRequestLocalization(app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>().Value);
            app.UseTokenProviderMiddleware(new JwtProviderOptions
            {
                Audience = Configuration["JwtProviderOptions:Audience"],
                Issuer = Configuration["JwtProviderOptions:Issuer"],
                Path = Configuration["JwtProviderOptions:Path"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Convert.FromBase64String(Configuration["JwtProviderOptions:SigningSecret"])), SecurityAlgorithms.HmacSha256),
            });

            // security headers
            app.UseHsts(hsts => hsts.MaxAge(365).IncludeSubdomains());
            app.UseXContentTypeOptions();
            app.UseReferrerPolicy(opts => opts.NoReferrer());
            app.UseXXssProtection(options => options.EnabledWithBlockMode());
            app.UseXfo(options => options.Deny());
            app.UseCsp(opts => opts
                .BlockAllMixedContent()
                .StyleSources(s => s.Self())
                .StyleSources(s => s.UnsafeInline())
                .FontSources(s => s.Self())
                .FormActions(s => s.Self())
                .FormActions(s => s.CustomSources("https://discordapp.com/oauth2/"))
                .FrameAncestors(s => s.Self())
                .ImageSources(s => s.Self())
                .ImageSources(s => s.CustomSources("https://cdn.discordapp.com/avatars/", "https://cdn.discordapp.com/embed/avatars/"))
            );

            app.UseHttpsRedirection();
            var cachePeriod = env.IsDevelopment() ? "600" : "604800";
            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    // Requires the following import:
                    // using Microsoft.AspNetCore.Http;
                    ctx.Context.Response.Headers.Append("Cache-Control", $"public, max-age={cachePeriod}");
                }
            });
            app.UseCookiePolicy();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
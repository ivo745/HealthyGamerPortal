using AutoMapper;
using HealthyGamerPortal.API.Helpers;
using HealthyGamerPortal.Common.Helpers;
using HealthyGamerPortal.Common.Models;
using HealthyGamerPortal.Common.ViewModels.Api.Authentication;
using HealthyGamerPortal.Common.ViewModels.LocalizationRecords;
using HealthyGamerPortal.Common.ViewModels.News;
using HealthyGamerPortal.Common.ViewModels.Users;
using HealthyGamerPortal.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog.Web;
using System;
using System.IO;
using System.Net.Mime;
using System.Reflection;
using System.Threading.Tasks;

namespace HealthyGamerPortal.API
{
    /// <summary>
    /// The main startup class for the API containing configuration for the API and its dependencies.
    /// </summary>
    public class Startup
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IConfiguration _configuration;
        private readonly string _assemblyFileVersion = VersionHelper.GetFileVersionForType(typeof(Startup));
        private readonly NLog.Logger _logger = NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();
        private static readonly LoggerFactory ConsoleLoggerFactory = new LoggerFactory(new[] { new DebugLoggerProvider() });

        /// <summary>
        /// Create a new instance of the <see cref="Startup"/> class with a specified configuration.
        /// </summary>
        /// <param name="configuration">The configuration that is read from the appSettings.json file.</param>
        /// <param name="env">The current hosting environment information.</param>
        public Startup(IWebHostEnvironment env, IConfiguration configuration)
        {
            _configuration = configuration;
            _hostingEnvironment = env;
        }

        /// <summary>
        /// Passes <see cref="JwtBearerOptions"/> to assign new option parameters.
        /// </summary>
        private void SetJwtBearerOptions(JwtBearerOptions options)
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateLifetime = true,
                ValidateAudience = false, //Audience cannot be checked because there could be many different audiences depending on who installs the mobile app.
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(_configuration["JWT:JWTSigningSecret"])),
                ValidIssuer = _configuration["JWT:JWTTokenIssuer"],
                ClockSkew = TimeSpan.Zero
            };
            options.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    // Received an api token that was expired
                    if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                    {
                        return Task.CompletedTask;
                    }
                    _logger.Error(context.Exception, "Error validating JWT token!");
                    return Task.CompletedTask;
                },
                OnTokenValidated = context =>
                {
                    Console.WriteLine("OnTokenValidated: " + context.SecurityToken);
                    return Task.CompletedTask;
                }
            };
            _logger.Info("Api Jwt authentication started ");
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">The services that are registered to the API.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            _logger.Info("Application starting (Startup.cs) " + _assemblyFileVersion);

            services.AddControllers().ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var result = new BadRequestObjectResult(context.ModelState);
                    result.ContentTypes.Add(MediaTypeNames.Application.Json);
                    result.ContentTypes.Add(MediaTypeNames.Application.Xml);

                    return result;
                };
            });

            //Register the IApiVersionDescriptionProvider to allow automatic detection of the different API versions that are registered.
            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'rev'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            //Using explicit ILogger type here to avoid registering the implementation type to DI rather than the interface.

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                    .RequireAuthenticatedUser().Build());
            });

            services.AddDbContext<Data.HealthyGamerPortalDbContext>(options =>
            {
                string dbConnectionString = _configuration["ConnectionStrings:DatabaseConnection"];
                options.UseSqlServer(dbConnectionString, options => options.EnableRetryOnFailure()).UseLoggerFactory(ConsoleLoggerFactory);
            });

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => SetJwtBearerOptions(options));

            // Add API versioning system with a default version number.
            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
            });

            services.AddCors();

            ServicesConfig.RegisterServices(services);
            ServicesConfig.RegisterCustomSingletonImplementation(services, ConfigureAutoMapper().CreateMapper());

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                // Get an instance of IApiVersionDescriptionProvider to be able to detect which versions of the API exist.
                var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

                foreach (var description in provider.ApiVersionDescriptions)
                    c.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description)); // Add a swagger.json definition for each API version.

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT authorization using the Bearer scheme. \n\rExample: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                // Load the exported XML documentation to enrich the Swagger spec and UI with more detailed information.
                var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentsPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);
                c.IncludeXmlComments(xmlCommentsPath);
            });
        }

        /// <summary>
        /// Adds general API info for the ApiVersionExplorer.
        /// </summary>
        /// <param name="description">The ApiVersionDescription of the API.</param>
        /// <returns>An info object with the relevant info for this API.</returns>
        private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new OpenApiInfo
            {
                Version = $"{VersionHelper.GetFileVersionForType(typeof(Startup))} rev.{description.ApiVersion.ToString()}",
                Title = "HealthyGamerPortal API",
                Description = "A simple example ASP.NET Core Web API",
                TermsOfService = new Uri("https://example.com/terms"),
                Contact = new OpenApiContact
                {
                    Name = "Shayne Boyer",
                    Email = string.Empty,
                    Url = new Uri("https://twitter.com/spboyer"),
                },
                License = new OpenApiLicense
                {
                    Name = "Use under LICX",
                    Url = new Uri("https://example.com/license"),
                }
            };
            if (description.IsDeprecated)
            {
                info.Description += " This API version has been deprecated.";
            }

            return info;
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">The API app builder.</param>
        /// <param name="apiVersionDescriptionProvider"></param>
        public void Configure(IApplicationBuilder app, IApiVersionDescriptionProvider apiVersionDescriptionProvider)
        {
            if (_hostingEnvironment.IsDevelopment())
            {
                app.UseExceptionHandler("/error-local-development");
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    // Load the Swagger spec for each version of the API into the UI.
                    foreach (var apiDescription in apiVersionDescriptionProvider.ApiVersionDescriptions)
                        c.SwaggerEndpoint($"/swagger/{apiDescription.GroupName}/swagger.json"
                            , $"HealthyGamerPortal API {apiDescription.GroupName}");

                    c.RoutePrefix = string.Empty; // Make sure the Swagger UI is displayed during start of the API in development mode.
                });
            }
            else
            {
                app.UseExceptionHandler("/error");

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        /// <summary>
        /// Configures the known mappings for AutoMapper.
        /// This so that AutoMapper know how to map between certain objects we created.
        /// </summary>
        /// <returns>The configuration for AutoMapper to do its thing. This configuration can be used to get an instance of <see cref="IMapper"/> which can then be injected.</returns>
        private MapperConfiguration ConfigureAutoMapper() => new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<StatusResult, StatusModel>();
            cfg.CreateMap<ApiLoginResult, AuthenticationModel>()
                .ForMember(ad => ad.AuthenticationToken, opt => opt.MapFrom(lr => lr.Token))
                .ForMember(ad => ad.ExpiresIn, opt => opt.MapFrom(lr => lr.Expires))
                .ForMember(ad => ad.Username, opt => opt.MapFrom(lr => lr.Username));
            cfg.CreateMap<ApplicationUser, CreateApplicationUserViewModel>();
            cfg.CreateMap<CreateApplicationUserViewModel, ApplicationUser>();
            cfg.CreateMap<EditApplicationUserViewModel, ApplicationUser>();
            cfg.CreateMap<ApplicationUser, ApplicationUserViewModel>();
            cfg.CreateMap<DeleteApplicationUserViewModel, ApplicationUser>();
            cfg.CreateMap<ApplicationUserViewModel, EditApplicationUserViewModel>();
            cfg.CreateMap<ApplicationUserViewModel, DeleteApplicationUserViewModel>();
            cfg.CreateMap<NewsItem, NewsItemViewModel>();
            cfg.CreateMap<CreateNewsItemViewModel, NewsItem>();
            cfg.CreateMap<EditNewsItemViewModel, NewsItem>();
            cfg.CreateMap<NewsItemViewModel, EditNewsItemViewModel>();
            cfg.CreateMap<NewsItemViewModel, DeleteNewsItemViewModel>();
            cfg.CreateMap<LocalizationRecord, LocalizationRecordViewModel>();
            cfg.CreateMap<LocalizationRecordViewModel, LocalizationRecord>();
        });
    }
}

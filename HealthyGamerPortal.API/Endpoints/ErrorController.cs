using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HealthyGamerPortal.API.Endpoints.Error
{
    /// <summary>
    /// Endpoint to provide detailed exception reporting and logging.
    /// </summary>
    [ApiController]
    public class ErrorController : ControllerBase
    {
        /// <summary>
        /// Provide view context for displaying exception information on development level
        /// </summary>
        /// <param name="webHostEnvironment"></param>
        [Route("/error-local-development")]
        [HttpGet]
        public IActionResult ErrorLocalDevelopment([FromServices] IWebHostEnvironment webHostEnvironment)
        {
            if (webHostEnvironment.EnvironmentName != "Development")
            {
                throw new InvalidOperationException(
                    "This shouldn't be invoked in non-development environments.");
            }

            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

            return Problem(
                detail: context.Error.StackTrace,
                title: context.Error.Message);
        }

        /// <summary>
        /// Provide view context for displaying exception information on deployment level
        /// </summary>
        [Route("/error")]
        [HttpGet]
        public IActionResult Error() => Problem();
    }
}
using HealthyGamerPortal.Common.Api;
using HealthyGamerPortal.Common.Extensions;
using HealthyGamerPortal.Common.ViewModels.Api;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HealthyGamerPortal.API.Endpoints
{
    /// <summary>
    /// The base API controller that contains some common attributes, properties and methods.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BaseApiController : ControllerBase
    {
        /// <summary>
        /// Generate an instance of <see cref="ApiResponseModel{T}"/> that indicates a successful operation and optionally includes a result.
        /// </summary>
        /// <typeparam name="T">The type of the result that came out of the sucessful operation.</typeparam>
        /// <param name="result">The actual result that came out of the successful operation.</param>
        /// <returns>An instance of <see cref="ApiResponseModel{T}"/> with the optional result set, and no error message.</returns>
        protected ApiResponseModel<T> GenerateSuccessfulResponse<T>(T result)
        {
            return new ApiResponseModel<T>
            {
                Result = result,
                ErrorMessage = string.Empty
            };
        }

        /// <summary>
        /// Generate an instance of <see cref="ApiUnhandledExceptionModel"/> that indicates a non-successful operation, taking an error code..
        /// </summary>
        /// <param name="ex"></param>
        /// <returns>An instance of <see cref="ApiUnhandledExceptionModel"/> with an error code.</returns>
        protected ApiUnhandledExceptionModel GenerateUnhandledExceptionResponse(Exception ex)
        {
            return new ApiUnhandledExceptionModel
            {
                Exception = ex
            };
        }

        /// <summary>
        /// Generate an instance of <see cref="ApiResponseModel{T}"/> that indicates a non-successful operation, taking an error code..
        /// </summary>
        /// <param name="errorCode">The unique code for this specific error that occurred during the operation.</param>
        /// <returns>An instance of <see cref="ApiResponseModel{T}"/> with an error code.</returns>
        protected ApiResponseModel<short?> GenerateErrorResponse(ErrorCode errorCode)
        {
            return new ApiResponseModel<short?>
            {
                ErrorCode = (int)errorCode,
                ErrorMessage = errorCode.GetDescription(),
                Result = null
            };
        }

        /// <summary>
        /// Generate an instance of <see cref="ApiResponseModel{T}"/> that indicates a non-successful operation, taking an error code and an message of what went wrong.
        /// It also takes format parameters that allows text replacement in the <paramref name="errorMessage"/> using <c>string.Format()</c>.
        /// </summary>
        /// <param name="errorCode">The unique code for this specific error that occurred during the operation.</param>
        /// <param name="errorMessage">An optional message explaining the error that occurred in more detail.</param>
        /// <param name="formatParams">Optional parameters for replacing values using <c>string.Format()</c>.</param>
        /// <returns>An instance of <see cref="ApiResponseModel{T}"/> with an error code and optional error message.</returns>
        protected ApiResponseModel<short?> GenerateErrorResponse(ErrorCode errorCode, string errorMessage, params string[] formatParams)
        {
            return new ApiResponseModel<short?>
            {
                ErrorCode = (int)errorCode,
                ErrorMessage = string.Format(errorMessage, formatParams),
                Result = null
            };
        }
    }
}

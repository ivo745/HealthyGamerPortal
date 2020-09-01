
namespace HealthyGamerPortal.Common.ViewModels.Api
{
    /// <summary>
    /// Class that defines a reponse this API will always return if possible regardless of status code.
    /// </summary>
    /// <typeparam name="T">The type of the <see cref="Result"/> that will be returned by a successful response.</typeparam>
    public class ApiResponseModel<T>
    {
        /// <summary>
        /// A message describing the error that occurred.
        /// This property is only filled if the response was unsuccessful.
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// A unique code for the error that occurred.
        /// </summary>
        /// <value>The unique error code of the error that occurred, or 0 if the response was successful.</value>
        public int ErrorCode { get; set; }

        /// <summary>
        /// The result of the operation that was performed by the request, if any.
        /// </summary>
        public T Result { get; set; }
    }
}

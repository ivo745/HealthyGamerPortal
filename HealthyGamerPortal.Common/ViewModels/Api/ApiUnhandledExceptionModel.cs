using System;

namespace HealthyGamerPortal.Common.ViewModels.Api
{

    public class ApiUnhandledExceptionModel
    {
        /// <summary>
        /// A message describing the error that occurred.
        /// This property is only filled if the response was unsuccessful.
        /// </summary>
        public Exception Exception { get; set; }
    }
}

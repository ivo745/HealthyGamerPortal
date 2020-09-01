
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace HealthyGamerPortal.Common.ViewModels.Api.Authentication
{
    /// <summary>
    /// Data transfer object that describes the validation errors that have occurred with a request thus causing a 400 response.
    /// </summary>
    public class ValidationErrorsModel
    {
        /// <summary>
        /// <see cref="Dictionary{TKey, TValue}"/> holding all the validation errors that have occurred during the request.
        /// </summary>
        public Dictionary<string, IEnumerable<string>> ValidationErrors { get; set; } = new Dictionary<string, IEnumerable<string>>();

        /// <summary>
        /// Create a new instance of <see cref="ValidationErrorsModel"/> with a <see cref="ActionContext"/> containing the modelstate and validation errors.
        /// </summary>
        /// <param name="actionContext">The context that contains the modelstate and all validation errors that have occurred.</param>
        public ValidationErrorsModel(ActionContext actionContext)
        {
            PopulateValidationErrors(actionContext.ModelState);
        }

        /// <summary>
        /// Extract the errors that occurred during validation and put them in the <see cref="ValidationErrors"/> dictionary.
        /// </summary>
        /// <param name="modelStateDictionary">The modelstate dictionary containing all validation data and errors.</param>
        private void PopulateValidationErrors(ModelStateDictionary modelStateDictionary)
        {
            foreach (var keyModelStatePair in modelStateDictionary)
            {
                var key = keyModelStatePair.Key;
                var errors = keyModelStatePair.Value.Errors;
                if (errors != null && errors.Count > 0)
                {
                    var errorMessages = errors.Select(modelError => modelError.ErrorMessage);
                    ValidationErrors.Add(key, errorMessages);
                }
            }
        }
    }
}

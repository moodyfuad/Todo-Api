using Microsoft.AspNetCore.Mvc.Filters;
using Shared.Exceptions;

namespace Presentation.Filters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState
                    .Where(kvp => kvp.Value.Errors.Count > 0)
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                    );

                // throw validation exception so global middleware can handle it
                throw new ValidationException("One or more validation errors occurred.", errors);
            }

            base.OnActionExecuting(context);
        }
    }
}

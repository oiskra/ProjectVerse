using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using projectverseAPI.DTOs;

namespace projectverseAPI.Validators
{
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if(!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values.Where(v => v.Errors.Any())
                    .Select(v => v.Errors)
                    .Select(v =>
                        v.Select(err => err.ErrorMessage))
                    .ToList();
                var keys = context.ModelState.Keys.ToList();
                var errorResult = new Dictionary<string, object>();

                for (int i = 0; i < errors.Count; i++)
                    errorResult.Add(keys[i], errors[i]);

                var responseObj = new ErrorResponseDTO
                {
                    Title = "Bad Request",
                    Status = StatusCodes.Status400BadRequest,
                    Errors = errorResult
                };

                context.Result = new JsonResult(responseObj)
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
        }
    }
}

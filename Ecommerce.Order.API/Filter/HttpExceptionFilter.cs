using Ecommerce.Order.CrossCutting.Exception;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Text.Json;

namespace Ecommerce.Order.API.Filter
{
    public class HttpExceptionFilter : IActionFilter
    {
        private const string ERROR_CONTENT_TYPE = "application/problem+json";
        private readonly ILogger _logger;

        public HttpExceptionFilter(ILogger<HttpExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is BusinessException bex)
            {
                var response = new ValidationProblemDetails(context.ModelState)
                {
                    Status = (int)HttpStatusCode.UnprocessableEntity,
                    Title = bex.Title,
                };

                foreach (var group in bex.Errors.GroupBy(a => a.Name))
                    response.Errors.Add(group.Key, group.Select(a => a.Message).ToArray());

                context.Result = new ObjectResult(response) { StatusCode = response.Status };
                context.HttpContext.Response.ContentType = ERROR_CONTENT_TYPE;
                context.ExceptionHandled = true;
                _logger.LogError(JsonSerializer.Serialize(response));
            }
            else if (context.Exception != null)
            {
                var ex = context.Exception;
                var response = new ValidationProblemDetails(context.ModelState)
                {
                    Status = (int)HttpStatusCode.UnprocessableEntity,
                    Title = ex.Message,
                    Detail = ex.StackTrace,
                };
                AddInnerExceptions(ex, response);
                context.Result = new ObjectResult(response) { StatusCode = response.Status };
                context.HttpContext.Response.ContentType = ERROR_CONTENT_TYPE;
                context.ExceptionHandled = true;
                _logger.LogError(JsonSerializer.Serialize(response));
            }
            else if (context.Result is BadRequestObjectResult)
            {
                var errors = new List<Tuple<string, string>>();
                ProblemDetails response;

                // context.ModelState.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid
                foreach (var ms in context.ModelState)
                    foreach (var error in ms.Value.Errors)
                        errors.Add(new Tuple<string, string>(ms.Key, error.ErrorMessage));

                if (errors.Count == 0)
                {
                    response = new ProblemDetails()
                    {
                        Status = (int)HttpStatusCode.BadRequest,
                        Title = "An unexpected internal Error occured",
                        Detail = ((BadRequestObjectResult)context.Result).Value?.ToString() ?? "Unknown error",
                    };
                }
                else
                {
                    var validationResponse = new ValidationProblemDetails
                    {
                        Status = (int)HttpStatusCode.BadRequest,
                        Title = "An unexpected internal Error occured",
                    };
                    foreach (var group in errors.GroupBy(a => a.Item1))
                        validationResponse.Errors.Add(group.Key, group.Select(a => a.Item2).ToArray());

                    response = validationResponse;
                }

                context.Result = new ObjectResult(response) { StatusCode = response.Status };
                context.HttpContext.Response.ContentType = ERROR_CONTENT_TYPE;
                _logger.LogError(JsonSerializer.Serialize(response));
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

        }

        private void AddInnerExceptions(Exception ex, ValidationProblemDetails problemDetails)
        {
            if (ex.InnerException == null)
                return;

            var errors = new List<string>();
            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
                errors.Add(ex.Message);
            }

            problemDetails.Errors.Clear();

            if (errors.Count == 1)
            {
                problemDetails.Title = "Inner Exceptions";
                problemDetails.Detail = errors.First();
            }
            else
            {
                problemDetails.Errors.Add("Inner Exceptions", errors.ToArray());
            }
        }
    }
}

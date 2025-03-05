using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using minimarket_project_backend.Common.Responses;

namespace minimarket_project_backend.Helpers
{
    public class ErrorResponseHelper
    {

        // Crea la respuesta por fallo en el servidor 

        public IActionResult CreateServerErrorResponse(string message)
        {
            return new ObjectResult(
                new ProblemDetails
                {
                    Type = "https://tools.ietf.org/html/rfc9110#section-15.6.1",
                    Title = "Internal Server Error",
                    Status = StatusCodes.Status500InternalServerError,
                    Detail = $"Ocurrió un error: {message}"
                }
            ){ StatusCode = 500 };
        }

        public IActionResult CreateRequestErrorResponse(ModelStateDictionary modelState)
        {
            var errors = modelState
                .Where(kvp => kvp.Value!.Errors.Any())
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage).ToArray()
                );

            return new BadRequestObjectResult(
                new ProblemDetails
                {
                    Type = "https://tools.ietf.org/html/rfc9110#section-15.5.1",
                    Title = "Validation error",
                    Status = StatusCodes.Status400BadRequest,
                    Detail = "One or more validation errors have occurred.",
                    Extensions = { ["errors"] = errors },
                }
            );
        }

        // Crea una respuesta de error genérica para Bad Request (400)
        public IActionResult CreateBadRequestResponse(string message)
        {
            return new BadRequestObjectResult(
                new ProblemDetails
                {
                    Type = "https://tools.ietf.org/html/rfc9110#section-15.5.1",
                    Title = "Bad Request",
                    Status = StatusCodes.Status400BadRequest,
                    Detail = message,
                }
            );
        }
        
        // Crea una respuesta de error genérica para Conflict (409)
        public IActionResult CreateConflictResponse(string message)
        {
            return new ConflictObjectResult(
                new ProblemDetails
                {
                    Type = "https://tools.ietf.org/html/rfc9110#section-15.5.10",
                    Title = "Conflict",
                    Status = StatusCodes.Status409Conflict,
                    Detail = message,
                }
            );
        }


        // Crea una respuesta de error cuando no se encuentra un registro (404)
        public IActionResult CreateNotFoundErrorResponse<TEntity>(object identifier)
        {
            string identifierMessage = identifier is int
                ? $"with the ID {identifier}."
                : $"with the name '{identifier}'.";

            return new NotFoundObjectResult(
                new ProblemDetails
                {
                    Type = "https://tools.ietf.org/html/rfc9110#section-15.5.5",
                    Title = "Not Found",
                    Status = StatusCodes.Status404NotFound,
                    Detail = $"There is no {typeof(TEntity).Name.ToLower()} {identifierMessage}"
                }
            );
        }


    }
}

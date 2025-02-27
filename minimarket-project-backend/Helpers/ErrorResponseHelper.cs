using Microsoft.AspNetCore.Mvc;
using minimarket_project_backend.Common.Responses;

namespace minimarket_project_backend.Helpers
{
    public class ErrorResponseHelper
    {

        // Crea la respuesta por fallo en el servidor 

        public IActionResult CreateServerErrorResponse(Exception ex)
        {
            return new ObjectResult(
                new ErrorResponse
                {
                    StatusCode = 500,
                    Message = $"Ocurrió un error: {ex.Message}",
                    Error = "Internal Server Error",
                    Success = false
                }
            ){ StatusCode = 500 };
        }

        // Crea una respuesta de error genérica para Bad Request (400)
        public IActionResult CreateBadRequestResponse(string message)
        {
            return new BadRequestObjectResult(new ErrorResponse
            {
                StatusCode = 400,
                Message = message,
                Error = "Bad Request",
                Success = false
            });
        }


        // Crea una respuesta de error cuando no se encuentra un registro (404)
        public IActionResult CreateNotFoundErrorResponse<TEntity>(object identifier)
        {
            string identifierMessage = identifier is int
                ? $"con el ID {identifier}."
                : $"con el nombre '{identifier}'.";

            return new NotFoundObjectResult(new ErrorResponse
            {
                StatusCode = 404,
                Message = $"No existe una {typeof(TEntity).Name.ToLower()} {identifierMessage}",
                Error = "Not Found",
                Success = false
            });
        }


    }
}

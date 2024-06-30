using Microsoft.AspNetCore.Mvc;
using minimarket_project_backend.Models.Responses;

namespace minimarket_project_backend.Validation
{
    public class MethodsHTTPValidator
    {
        public IActionResult ValidatePagination(int page, int limit)
        {
            if (page < 0 || limit < 0)
            {
                return new BadRequestObjectResult(new ApiResponse
                {
                    StatusCode = 400,
                    Message = "El page o el limit no pueden ser menores a cero.",
                    Error = "Bad Request",
                    Success = false,
                });
            }

            return null;
        }        
        
        public IActionResult ValidateSearch(string name, int page, int limit)
        {
            if (name.Length <= 0)
            {
                return new BadRequestObjectResult(new ApiResponse
                {
                    StatusCode = 400,
                    Message = $"El name debe tener una longitud mayor a cero.",
                    Error = "Bad Request",
                    Success = false,
                });
            }

            if(ValidatePagination(page, limit) != null) 
            {
                return ValidatePagination(page, limit);
            }

            return null;
        }        
        
        public IActionResult ValidateId(int id)
        {
            if (id <= 0)
            {
                return new BadRequestObjectResult(new
                {
                    StatusCode = 400,
                    Message = $"El id no puede ser menor o igual a cero.",
                    Error = "Bad Request",
                    Success = false,
                });
            }

            return null;
        }
    }
}

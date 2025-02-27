using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using minimarket_project_backend.Common.Validator;

namespace minimarket_project_backend.Controllers
{
    [Route("categories")]
    [ApiController]
    [EnableCors("ReglasCors")]
    public class CategoriesController : ControllerBase
    {
        private readonly RequestValidator methodsHTTPValidator = new();

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page, [FromQuery] int limit)
        {
            //var validationResult = methodsHTTPValidator.ValidatePagination(page, limit);
            //if (validationResult != null) return validationResult;

            //var response = await _iCategoria.getAll(page, limit);
            //return responseProcessor.ProcessDataResponse(response);
            return Ok(null);
        }
    }
}

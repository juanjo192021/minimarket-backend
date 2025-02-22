using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using minimarket_project_backend.Utilities;
using minimarket_project_backend.Validation;
using tienda_project_backend.Services;

namespace tienda_project_backend.Controllers
{
    [Route("categoria")]
    [ApiController]
    [EnableCors("ReglasCors")]
    public class CategoriaController(ICategoria iCategoria) : ControllerBase
    {
        private readonly ICategoria _iCategoria = iCategoria;
        private readonly MethodsHTTPValidator methodsHTTPValidator = new();

        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> getAll([FromQuery] int page, [FromQuery] int limit)
        {
            //var validationResult = methodsHTTPValidator.ValidatePagination(page, limit);
            //if (validationResult != null) return validationResult;

            //var response = await _iCategoria.getAll(page, limit);
            //return responseProcessor.ProcessDataResponse(response);
            return Ok(null);
        }
    }
}

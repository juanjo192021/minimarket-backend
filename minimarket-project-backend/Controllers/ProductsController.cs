using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using minimarket_project_backend.Common.Validator;
using minimarket_project_backend.Services;

namespace minimarket_project_backend.Controllers
{
    [Route("products")]
    [ApiController]
    [EnableCors("ReglasCors")]
    public class ProductsController(IProductService iProducto) : ControllerBase
    {
        private readonly IProductService _iProducto = iProducto;
        private readonly RequestValidator methodsHTTPValidator = new();
        

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page, [FromQuery] int limit)
        {
            //var validationResult = methodsHTTPValidator.ValidatePagination(page, limit);
            //if (validationResult != null) return validationResult;

            //var response = await _iProducto.getAll(page, limit);
            //return responseProcessor.ProcessDataResponse(response);

            return Ok(null);
        }
    }
}

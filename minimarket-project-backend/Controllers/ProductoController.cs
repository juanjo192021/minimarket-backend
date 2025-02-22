using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using minimarket_project_backend.Models.Responses;
using minimarket_project_backend.Utilities;
using minimarket_project_backend.Validation;
using tienda_project_backend.Dtos.Producto;
using tienda_project_backend.Models;
using tienda_project_backend.Services;

namespace tienda_project_backend.Controllers
{
    [Route("producto")]
    [ApiController]
    [EnableCors("ReglasCors")]
    public class ProductoController(IProducto iProducto) : ControllerBase
    {
        private readonly IProducto _iProducto = iProducto;
        private readonly MethodsHTTPValidator methodsHTTPValidator = new();
        

        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> getAll([FromQuery] int page, [FromQuery] int limit)
        {
            //var validationResult = methodsHTTPValidator.ValidatePagination(page, limit);
            //if (validationResult != null) return validationResult;

            //var response = await _iProducto.getAll(page, limit);
            //return responseProcessor.ProcessDataResponse(response);

            return Ok(null);
        }
    }
}

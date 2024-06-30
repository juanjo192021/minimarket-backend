using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using minimarket_project_backend.Utilities;
using minimarket_project_backend.Validation;
using tienda_project_backend.Dtos.Marca;
using tienda_project_backend.Services;

namespace tienda_project_backend.Controllers
{
    [Route("marca")]
    [ApiController]
    [EnableCors("ReglasCors")]
    public class MarcaController(IMarca iMarca) : ControllerBase
    {
        private readonly IMarca _iMarca = iMarca;
        private readonly MethodsHTTPValidator methodsHTTPValidator = new();
        private readonly ResponseProcessor responseProcessor = new();

        [HttpGet]
        //page={page}&limit={limit}
        [Route("getAll")]
        public async Task<IActionResult> getAll([FromQuery] int page, [FromQuery] int limit)
        {
            var validationResult = methodsHTTPValidator.ValidatePagination(page, limit);
            if (validationResult != null) return validationResult;

            var response = await _iMarca.getAll(page, limit);
            return responseProcessor.ProcessDataResponse(response);
        }

        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> search([FromQuery] string name, [FromQuery] int page, [FromQuery] int limit)
        {
            var validationResult = methodsHTTPValidator.ValidateSearch(name,page, limit);
            if (validationResult != null) return validationResult;

            var response = await _iMarca.search(name, page, limit);
            return responseProcessor.ProcessDataResponse(response);
        }


        [HttpGet]
        [Route("searchById/{id}")]
        public async Task<IActionResult> searchById(int id)
        {
            var validationResult = methodsHTTPValidator.ValidateId(id);
            if (validationResult != null) return validationResult;

            var response = await _iMarca.searchById(id);
            return responseProcessor.ProcessSingleDataResponse(response);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> create([FromBody] CreateMarcaDTO createMarcaDTO)
        {
            var response = await _iMarca.create(createMarcaDTO);
            return responseProcessor.ProcessApiResponse(response);
        }

        [HttpPatch]
        [Route("update")]
        public async Task<IActionResult> update([FromBody] UpdateMarcaDTO updateMarcaDTO)
        {
            var response = await _iMarca.update(updateMarcaDTO);
            return responseProcessor.ProcessApiResponse(response);
        }

        [HttpDelete()]
        [Route("delete/{id}")]
        public async Task<IActionResult> delete(int id)
        {
            var validationResult = methodsHTTPValidator.ValidateId(id);
            if (validationResult != null) return validationResult;

            var response = await _iMarca.delete(id);
            return responseProcessor.ProcessApiResponse(response);
        }
    }
}

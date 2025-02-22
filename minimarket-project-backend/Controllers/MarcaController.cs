using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using minimarket_project_backend.Helpers;
using minimarket_project_backend.Utilities;
using minimarket_project_backend.Validation;
using tienda_project_backend.Dtos.Marca;
using tienda_project_backend.Models;
using tienda_project_backend.Services;
using tienda_project_backend.Services.Implementation;

namespace tienda_project_backend.Controllers
{
    [Route("marca")]
    [ApiController]
    [EnableCors("ReglasCors")]
    public class MarcaController(IMarca _marcaService, IMapper _mapper) : ControllerBase
    {
        private readonly MethodsHTTPValidator methodsHTTPValidator = new();
        private readonly ResponseHelper responseHelper = new(_mapper);
        private readonly ErrorResponseHelper errorResponseHelper = new();

        [HttpGet]
        //page={page}&limit={limit}
        [Route("getAll")]
        public async Task<IActionResult> GetAll([FromQuery] string? name = null, [FromQuery] int page = 1, [FromQuery] int limit = 10)
        {
            
            try
            {
                var validationResult = methodsHTTPValidator.ValidatePagination(page, limit);

                if (!validationResult) return errorResponseHelper.CreateBadRequestResponse("El page o el limit no pueden ser menores a cero.");

                var response = await _marcaService.GetAll(name, page, limit);

                if (!string.IsNullOrEmpty(name) && (response?.Data == null || response.Data.Count == 0))
                    return errorResponseHelper.CreateNotFoundErrorResponse<Marca>(name);

                return Ok(response);

            }
            catch (Exception ex)
            {
                return errorResponseHelper.CreateServerErrorResponse(ex);
            }
        }

        [HttpGet]
        [Route("searchById/{id}")]
        public async Task<IActionResult> SearchById(int id)
        {

            try
            {
                var validationResult = methodsHTTPValidator.ValidateId(id);
                if (!validationResult) return errorResponseHelper.CreateBadRequestResponse("El id no puede ser menor o igual a cero.");

                var response = await _marcaService.SearchById(id);

                if (response == null) return errorResponseHelper.CreateNotFoundErrorResponse<Marca>(id);

                return responseHelper.CreateSuccessResponse(
                    response,
                    200,
                    "Marca obtenida con éxito.");

            }
            catch (Exception ex)
            {
                return errorResponseHelper.CreateServerErrorResponse(ex);
            }
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromForm] CreateMarcaDTO createMarcaDTO)
        {
            try
            {
                var response = await _marcaService.Create(createMarcaDTO);

                if (response == null) return errorResponseHelper.CreateBadRequestResponse("La marca no pudo ser creada.");

                //return responseHelper.CreateSuccessResponse(
                //    response, 
                //    201, 
                //    "Marca creada con éxito.");

                return responseHelper.CreateSuccessResponse(
                    response,
                    201,
                    "Marca creada con éxito.",
                    nameof(SearchById), // Nombre del método que recupera una marca por ID
                    new { id = response.Id } // Parámetros de la ruta para obtener el recurso
                );
            }
            catch (Exception ex)
            {
                return errorResponseHelper.CreateServerErrorResponse(ex);
            }

        }

        [HttpPatch]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] UpdateMarcaDTO updateMarcaDTO)
        {
            try
            {
                var response = await _marcaService.Update(updateMarcaDTO);

                if (response == null) return errorResponseHelper.CreateBadRequestResponse("La marca no pudo ser actualizada.");

                return responseHelper.CreateSuccessResponse(
                        response,
                        201,
                        "Marca acrualizada con éxito.");
            }
            catch (Exception ex)
            {
                return errorResponseHelper.CreateServerErrorResponse(ex);
            }
        }

        [HttpDelete()]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var validationResult = methodsHTTPValidator.ValidateId(id);
                if (!validationResult) return errorResponseHelper.CreateBadRequestResponse("El id no puede ser menor o igual a cero.");

                var response = await _marcaService.Delete(id);

                if (!response) return errorResponseHelper.CreateNotFoundErrorResponse<Marca>(id);

                return responseHelper.CreateSuccessDeleteResponse("Marca eliminada correctamente");
            }
            catch (Exception ex)
            {
                return errorResponseHelper.CreateServerErrorResponse(ex);
            }

        }
    }
}

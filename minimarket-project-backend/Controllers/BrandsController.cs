using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using minimarket_project_backend.Common.Validator;
using minimarket_project_backend.Dtos.Brand;
using minimarket_project_backend.Helpers;
using minimarket_project_backend.Models;
using minimarket_project_backend.Services;

namespace minimarket_project_backend.Controllers
{
    [Route("brands")]
    [EnableCors("ReglasCors")]
    public class BrandsController : ControllerBase
    {
        private readonly RequestValidator methodsHTTPValidator = new();
        private readonly ResponseHelper responseHelper = new ResponseHelper();
        private readonly ErrorResponseHelper errorResponseHelper = new();

        private readonly IBrandService _marcaService;

        public BrandsController(IBrandService marcaService)
        {
            _marcaService = marcaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? name = null, 
            [FromQuery] int page = 1, 
            [FromQuery] int limit = 10)
        {

            try
            {
                var validationResult = methodsHTTPValidator.ValidatePagination(page, limit);

                if (!validationResult) return errorResponseHelper.CreateBadRequestResponse("The page or limit cannot be less than zero.");

                var response = await _marcaService.GetAll(name ?? string.Empty, page, limit);

                if (!string.IsNullOrEmpty(name) && (response?.Data == null || response.Data.Count == 0))
                    return errorResponseHelper.CreateNotFoundErrorResponse<Brand>(name);

                return Ok(response);

            }
            catch (Exception ex)
            {
                return errorResponseHelper.CreateServerErrorResponse(ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> SearchById(int id)
        {

            try
            {
                var validationResult = methodsHTTPValidator.ValidateId(id);

                if (!validationResult) return errorResponseHelper.CreateBadRequestResponse("The id cannot be less than or equal to zero.");

                var response = await _marcaService.SearchById(id);

                if (response == null) return errorResponseHelper.CreateNotFoundErrorResponse<Brand>(id);

                return responseHelper.CreateSuccessResponse(
                    response,
                    StatusCodes.Status200OK,
                    "Brand successfully found.");

            }
            catch (Exception ex)
            {
                return errorResponseHelper.CreateServerErrorResponse(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] BrandRequestDTO brandRequestDTO)
        {
            try
            {
                if (!ModelState.IsValid) return errorResponseHelper.CreateRequestErrorResponse(ModelState);

                var brand = await _marcaService.SearchByName(brandRequestDTO.name);

                if (brand != null) return errorResponseHelper.CreateConflictResponse("The brand already exists.");

                var response = await _marcaService.Create(brandRequestDTO);

                if (response == null) return errorResponseHelper.CreateServerErrorResponse("The brand could not be created.");

                return responseHelper.CreateSuccessResponse(
                    response,
                    201,
                    "Brand successfully created.",
                    nameof(SearchById), // Nombre del método que recupera una marca por ID
                    "Brands", // Nombre del controlador
                    new { id = response.Id } // Parámetros de la ruta para obtener el recurso
                );
            }
            catch (Exception ex)
            {
                return errorResponseHelper.CreateServerErrorResponse(ex.Message);
            }

        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] BrandRequestDTO brandRequestDTO)
        {
            try
            {
                var validationResult = methodsHTTPValidator.ValidateId(id);

                if (!validationResult) return errorResponseHelper.CreateBadRequestResponse("The id cannot be less than or equal to zero.");

                if (!ModelState.IsValid) return errorResponseHelper.CreateRequestErrorResponse(ModelState);

                var brand = await _marcaService.SearchById(id);

                if (brand == null) return errorResponseHelper.CreateNotFoundErrorResponse<Brand>(id);

                var response = await _marcaService.Update(brand, brandRequestDTO);

                if (response == null) return errorResponseHelper.CreateServerErrorResponse("The brand could not be updated.");

                return responseHelper.CreateSuccessResponse(
                        response,
                        200,
                        "Brand successfully updated.");
            }
            catch (Exception ex)
            {
                return errorResponseHelper.CreateServerErrorResponse(ex.Message);
            }
        }

        [HttpDelete()]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var validationResult = methodsHTTPValidator.ValidateId(id);

                if (!validationResult) return errorResponseHelper.CreateBadRequestResponse("The id cannot be less than or equal to zero.");

                var brand = await _marcaService.SearchById(id);

                if (brand == null) return errorResponseHelper.CreateNotFoundErrorResponse<Brand>(id);

                var response = await _marcaService.Deactivated(brand);

                if (!response) return errorResponseHelper.CreateBadRequestResponse("The brand could not be deleted.");

                return responseHelper.CreateSuccessDeleteResponse(brand,"Brand successfully deleted.");
            }
            catch (Exception ex)
            {
                return errorResponseHelper.CreateServerErrorResponse(ex.Message);
            }

        }
    }
}

using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using minimarket_project_backend.Common.Validator;
using minimarket_project_backend.Dtos.Brand;
using minimarket_project_backend.Helpers;
using minimarket_project_backend.Models;
using minimarket_project_backend.Services;

namespace minimarket_project_backend.Controllers
{
    [Route("api/v1/brands")]
    [EnableCors("ReglasCors")]
    public class BrandsController : Controller
    {
        private readonly RequestValidator _requestValidator = new();
        private readonly ResponseHelper _responseHelper = new ();
        private readonly ErrorResponseHelper _errorResponseHelper = new();

        private readonly IBrandService _brandService;

        public BrandsController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? name = null, 
            [FromQuery] int page = 1, 
            [FromQuery] int limit = 10)
        {

            try
            {
                var validationResult = _requestValidator.ValidatePagination(page, limit);

                if (!validationResult) return _errorResponseHelper.CreateBadRequestResponse("The page or limit cannot be less than zero.");

                var response = await _brandService.GetAll(name ?? string.Empty, page, limit);

                if (!string.IsNullOrEmpty(name) && (response?.Data == null || response.Data.Count == 0))
                    return _errorResponseHelper.CreateNotFoundErrorResponse<Brand>(name);

                return Ok(response);

            }
            catch (Exception ex)
            {
                return _errorResponseHelper.CreateServerErrorResponse(ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> SearchById(int id)
        {

            try
            {
                var validationResult = _requestValidator.ValidateId(id);

                if (!validationResult) return _errorResponseHelper.CreateBadRequestResponse("The id cannot be less than or equal to zero.");

                var response = await _brandService.SearchById(id);

                if (response == null) return _errorResponseHelper.CreateNotFoundErrorResponse<Brand>(id);

                return _responseHelper.CreateSuccessResponse(
                    response,
                    StatusCodes.Status200OK,
                    "Brand successfully found.");

            }
            catch (Exception ex)
            {
                return _errorResponseHelper.CreateServerErrorResponse(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] BrandRequestDTO brandRequestDTO)
        {
            try
            {
                if (!ModelState.IsValid) return _errorResponseHelper.CreateRequestErrorResponse(ModelState);

                var brand = await _brandService.SearchByName(brandRequestDTO.name);

                if (brand != null) return _errorResponseHelper.CreateConflictResponse("The brand already exists.");

                var response = await _brandService.Create(brandRequestDTO);

                if (response == null) return _errorResponseHelper.CreateServerErrorResponse("The brand could not be created.");

                return _responseHelper.CreateSuccessResponse(
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
                return _errorResponseHelper.CreateServerErrorResponse(ex.Message);
            }

        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] BrandRequestDTO brandRequestDTO)
        {
            try
            {
                var validationResult = _requestValidator.ValidateId(id);

                if (!validationResult) return _errorResponseHelper.CreateBadRequestResponse("The id cannot be less than or equal to zero.");

                if (!ModelState.IsValid) return _errorResponseHelper.CreateRequestErrorResponse(ModelState);

                var brand = await _brandService.SearchById(id);

                if (brand == null) return _errorResponseHelper.CreateNotFoundErrorResponse<Brand>(id);

                var response = await _brandService.Update(brand, brandRequestDTO);

                if (response == null) return _errorResponseHelper.CreateServerErrorResponse("The brand could not be updated.");

                return _responseHelper.CreateSuccessResponse(
                        response,
                        200,
                        "Brand successfully updated.");
            }
            catch (Exception ex)
            {
                return _errorResponseHelper.CreateServerErrorResponse(ex.Message);
            }
        }

        [HttpDelete()]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var validationResult = _requestValidator.ValidateId(id);

                if (!validationResult) return _errorResponseHelper.CreateBadRequestResponse("The id cannot be less than or equal to zero.");

                var brand = await _brandService.SearchById(id);

                if (brand == null) return _errorResponseHelper.CreateNotFoundErrorResponse<Brand>(id);

                var response = await _brandService.Deactivated(brand);

                if (!response) return _errorResponseHelper.CreateBadRequestResponse("The brand could not be deleted.");

                return _responseHelper.CreateSuccessDeleteResponse(brand,"Brand successfully deleted.");
            }
            catch (Exception ex)
            {
                return _errorResponseHelper.CreateServerErrorResponse(ex.Message);
            }

        }
    }
}

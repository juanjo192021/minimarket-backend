using FluentValidation;
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
    public class BrandsController(IBrandService _marcaService) : ControllerBase
    {
        private readonly RequestValidator methodsHTTPValidator = new();
        private readonly ResponseHelper responseHelper = new ResponseHelper();
        private readonly ErrorResponseHelper errorResponseHelper = new();

        //("{name?}/{page?}/{limit?}")
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? name = null, [FromQuery] int page = 1, [FromQuery] int limit = 10)
        {

            try
            {
                var validationResult = methodsHTTPValidator.ValidatePagination(page, limit);

                if (!validationResult) return errorResponseHelper.CreateBadRequestResponse("El page o el limit no pueden ser menores a cero.");

                var response = await _marcaService.GetAll(name ?? string.Empty, page, limit);

                if (!string.IsNullOrEmpty(name) && (response?.Data == null || response.Data.Count == 0))
                    return errorResponseHelper.CreateNotFoundErrorResponse<Brand>(name);

                return Ok(response);

            }
            catch (Exception ex)
            {
                return errorResponseHelper.CreateServerErrorResponse(ex);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> SearchById(int id)
        {

            try
            {
                var validationResult = methodsHTTPValidator.ValidateId(id);
                if (!validationResult) return errorResponseHelper.CreateBadRequestResponse("El id no puede ser menor o igual a cero.");

                var response = await _marcaService.SearchById(id);

                if (response == null) return errorResponseHelper.CreateNotFoundErrorResponse<Brand>(id);

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
        public async Task<IActionResult> Create(
            [FromForm] BrandRequestDTO brandRequestDTO,
            [FromServices] IValidator<BrandRequestDTO> validator
        )
        {
            try
            {
                // 🔥 Validar manualmente con FluentValidation
                //var validationResult = await validator.ValidateAsync(brandRequestDTO);

                //if (!validationResult.IsValid)
                //{
                //    return BadRequest(new
                //    {
                //        Message = "Errores de validación",
                //        Errors = validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage })
                //    });
                //}

                var validationResult = await validator.ValidateAsync(brandRequestDTO);

                if (!validationResult.IsValid)
                {
                    var problemDetails = new ProblemDetails
                    {
                        Type = "https://tools.ietf.org/html/rfc9110#section-15.5.1",
                        Title = "Error de validación",
                        Status = StatusCodes.Status400BadRequest,
                        Detail = "Uno o más errores de validación han ocurrido."
                    };

                    // Agregamos los errores al diccionario de extensiones
                    problemDetails.Extensions["errors"] = validationResult.Errors
                        .GroupBy(e => e.PropertyName)
                        .ToDictionary(
                            g => g.Key,
                            g => g.Select(e => e.ErrorMessage).ToArray()
                        );

                    return BadRequest(problemDetails);
                }


                var response = await _marcaService.Create(brandRequestDTO);

                if (response == null) return errorResponseHelper.CreateBadRequestResponse("La marca no pudo ser creada.");

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
        [Route("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] BrandRequestDTO brandRequestDTO)
        {
            try
            {
                var brand = await _marcaService.SearchById(id);

                if (brand == null) return errorResponseHelper.CreateNotFoundErrorResponse<Brand>(id);

                var response = await _marcaService.Update(brand, brandRequestDTO);

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
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var validationResult = methodsHTTPValidator.ValidateId(id);
                if (!validationResult) return errorResponseHelper.CreateBadRequestResponse("El id no puede ser menor o igual a cero.");

                var response = await _marcaService.Delete(id);

                if (!response) return errorResponseHelper.CreateNotFoundErrorResponse<Brand>(id);

                return responseHelper.CreateSuccessDeleteResponse("Marca eliminada correctamente");
            }
            catch (Exception ex)
            {
                return errorResponseHelper.CreateServerErrorResponse(ex);
            }

        }
    }
}

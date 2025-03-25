using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using minimarket_project_backend.Common.Validator;
using minimarket_project_backend.Dtos.Category;
using minimarket_project_backend.Helpers;
using minimarket_project_backend.Models;
using minimarket_project_backend.Services;

namespace minimarket_project_backend.Controllers
{
    [Route("api/v1/categories")]
    [EnableCors("ReglasCors")]
    public class CategoriesController : Controller
    {
        private readonly RequestValidator _requestValidator = new();
        private readonly ResponseHelper _responseHelper = new();
        private readonly ErrorResponseHelper _errorResponseHelper = new();

        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> SearchById(int id)
        {

            try
            {
                var validationResult = _requestValidator.ValidateId(id);

                if (!validationResult) return _errorResponseHelper.CreateBadRequestResponse("The id cannot be less than or equal to zero.");

                var response = await _categoryService.SearchById(id);

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
        public async Task<IActionResult> Create([FromForm] CategoryRequestDTO categoryRequestDTO)
        {
            try
            {
                if (!ModelState.IsValid) return _errorResponseHelper.CreateRequestErrorResponse(ModelState);

                var category = await _categoryService.SearchByName(categoryRequestDTO.Name);

                if (category != null) return _errorResponseHelper.CreateConflictResponse("The category already exists.");
                var response = await _categoryService.Create(categoryRequestDTO);

                if (response == null) return _errorResponseHelper.CreateServerErrorResponse("The category could not be created.");

                return _responseHelper.CreateSuccessResponse(
                    response,
                    201,
                    "Category successfully created.",
                    nameof(SearchById),
                    "Categories",
                    new { id = response.Id }
                );
            }
            catch (Exception ex)
            {
                return _errorResponseHelper.CreateServerErrorResponse(ex.Message);
            }

        }
    }
}

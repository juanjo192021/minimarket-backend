using minimarket_project_backend.Common.Responses;
using minimarket_project_backend.Dtos.Category;
using minimarket_project_backend.Models;

namespace minimarket_project_backend.Services
{
    public interface ICategoryService
    {
        Task<PaginationResponse<List<Category>>> GetAll(string name, int page, int limit);
        Task<Category?> SearchById(int id);
        Task<Category?> SearchByName(string name);
        Task<Category?> Create(CategoryRequestDTO categoryRequestDTO);
    }
}

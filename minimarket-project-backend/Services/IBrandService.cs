using minimarket_project_backend.Common.Responses;
using minimarket_project_backend.Models;
using minimarket_project_backend.Dtos.Brand;

namespace minimarket_project_backend.Services
{
    public interface IBrandService
    {
        public Task<PaginationResponse<List<Brand>>> GetAll(string name, int page, int limit);
        public Task<Brand?> SearchById(int id);
        public Task<Brand?> SearchByName(string name);
        public Task<Brand?> Create(BrandRequestDTO brandRequestDTO);
        public Task<Brand?> Update(Brand brand , BrandRequestDTO brandRequestDTO);
        public Task<bool> Deactivated(Brand brand); 
        public Task<bool> Delete(Brand brand);
    }
}

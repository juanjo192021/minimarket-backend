using minimarket_project_backend.Dtos.Categoria;
using minimarket_project_backend.Models.Responses;
using tienda_project_backend.Dtos.Categoria;

namespace tienda_project_backend.Services
{
    public interface ICategoria
    {
        public Task<DataResponse<List<CategoriaDTO>>> getAll(int page, int limit);
        public Task<DataResponse<List<CategoriaDTO>>> search(string name, int page, int limit);
        public Task<DataResponse<CategoriaDTO>> searchById(int id);
        public Task<ApiResponse> create(CreateCategoriaDTO createCategoriaDTO);
        public Task<ApiResponse> update(UpdateCategoriaDTO updateCategoriaDTO);
        public Task<ApiResponse> delete(int id);
    }
}

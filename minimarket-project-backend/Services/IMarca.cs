using minimarket_project_backend.Models.Responses;
using tienda_project_backend.Dtos.Marca;

namespace tienda_project_backend.Services
{
    public interface IMarca
    {
        public Task<DataResponse<List<MarcaDTO>>> getAll(int page, int limit);
        public Task<DataResponse<List<MarcaDTO>>> search(string name, int page, int limit);
        public Task<DataResponse<MarcaDTO>> searchById(int id);
        public Task<ApiResponse> create(CreateMarcaDTO createMarcaDTO);
        public Task<ApiResponse> update(UpdateMarcaDTO updateMarcaDTO);
        public Task<ApiResponse> delete(int id);
    }
}

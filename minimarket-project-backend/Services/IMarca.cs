using minimarket_project_backend.Models.Responses;
using tienda_project_backend.Dtos.Marca;

namespace tienda_project_backend.Services
{
    public interface IMarca
    {
        public Task<PaginationResponse<List<MarcaDTO>>> GetAll(string name, int page, int limit);
        public Task<MarcaDTO?> SearchById(int id);
        public Task<MarcaDTO?> Create(CreateMarcaDTO createMarcaDTO);
        public Task<MarcaDTO?> Update(UpdateMarcaDTO updateMarcaDTO);
        public Task<bool> Delete(int id);
    }
}

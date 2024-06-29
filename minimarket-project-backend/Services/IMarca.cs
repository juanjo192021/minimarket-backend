using tienda_project_backend.Dtos.Marca;
using tienda_project_backend.Models;
using tienda_project_backend.Utilities.Response;

namespace tienda_project_backend.Services
{
    public interface IMarca
    {
        public Task<Response<List<MarcaDTO>>> getAll(int page, int limit);
        public Task<Response<List<MarcaDTO>>> search(string name, int page, int limit);
        public Task<Response<MarcaDTO>> searchById(int id);
        public Task<Response<Marca>> create(CreateMarcaDTO createMarcaDTO);
        public Task<Response<Marca>> update(UpdateMarcaDTO updateMarcaDTO);
        public Task<Response<Marca>> delete(int id);
    }
}

using tienda_project_backend.Dtos.Categoria;
using tienda_project_backend.Models;
using tienda_project_backend.Utilities.Response;
namespace tienda_project_backend.Services
{
    public interface ICategoria
    {
        public Task<Response<List<CategoriaDTO>>> getAll();
    }
}

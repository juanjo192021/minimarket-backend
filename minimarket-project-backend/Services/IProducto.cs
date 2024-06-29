using tienda_project_backend.Dtos.Producto;
using tienda_project_backend.Utilities.Response;

namespace tienda_project_backend.Services
{
    public interface IProducto
    {
        public Task<Response<List<ProductoDTO>>> getAll();
    }
}

using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using tienda_project_backend.Dtos.Producto;
using tienda_project_backend.Services;
using tienda_project_backend.Utilities.Response;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace tienda_project_backend.Controllers
{
    [Route("producto")]
    [ApiController]
    [EnableCors("ReglasCors")]
    public class ProductoController : ControllerBase
    {
        private readonly IProducto _iProducto;
        public ProductoController(IProducto iProducto)
        {
            _iProducto = iProducto;
        }

        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> getAll()
        {
            Response<List<ProductoDTO>> productos = await _iProducto.getAll();

            if (!productos.Success)
            {

                return this.StatusCode(500, new
                {
                    productos.StatusCode,
                    productos.Message,
                    productos.Error,
                    productos.Success
                });
            }

            return this.Ok(new
            {
                productos.StatusCode,
                productos.Message,
                productos.Success,
                productos.Data
            });
        }
    }
}

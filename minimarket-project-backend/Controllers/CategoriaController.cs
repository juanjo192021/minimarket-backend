using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using tienda_project_backend.Dtos.Categoria;
using tienda_project_backend.Models;
using tienda_project_backend.Services;
using tienda_project_backend.Utilities.Response;

namespace tienda_project_backend.Controllers
{
    [Route("categoria")]
    [ApiController]
    [EnableCors("ReglasCors")]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoria _iCategoria;
        public CategoriaController(ICategoria iCategoria)
        {
            _iCategoria = iCategoria;
        }

        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> getAll()
        {
            Response<List<CategoriaDTO>> categorias = await _iCategoria.getAll();

            if (!categorias.Success)
            {

                return this.StatusCode(500, new
                {
                    categorias.StatusCode,
                    categorias.Message,
                    categorias.Error,
                    categorias.Success
                });
            }

            return this.Ok(new 
            {
                categorias.StatusCode,
                categorias.Message,
                categorias.Success,
                categorias.Data
            });
        }
    }
}

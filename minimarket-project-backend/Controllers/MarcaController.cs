using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using tienda_project_backend.Dtos.Marca;
using tienda_project_backend.Models;
using tienda_project_backend.Services;
using tienda_project_backend.Utilities.Response;


namespace tienda_project_backend.Controllers
{
    [Route("marca")]
    [ApiController]
    [EnableCors("ReglasCors")]
    public class MarcaController : ControllerBase
    {
        private readonly IMarca _iMarca;
        public MarcaController(IMarca iMarca)
        {
            _iMarca = iMarca;
        }

        [HttpGet]
        //page={page}&limit={limit}
        [Route("getAll")]
        public async Task<IActionResult> getAll([FromQuery] int page, [FromQuery] int limit)
        {
            if (page < 0 || limit < 0)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = $"El page o el limit no pueden ser menores a cero.",
                    Error = "Bad Request",
                    Success = false,
                });
            }

            Response<List<MarcaDTO>> marcas = await _iMarca.getAll(page, limit);

            if (marcas.Data == null)
            {
                return this.StatusCode(500, new
                {
                    marcas.StatusCode,
                    marcas.Message,
                    marcas.Error,
                    marcas.Success
                });
            }

            if (marcas.Data.Count == 0)
            {
                return this.NotFound(new
                {
                    marcas.StatusCode,
                    marcas.Message,
                    marcas.Error,
                    marcas.Success,
                    marcas.Data
                });
            }

            return this.Ok(new
            {
                marcas.StatusCode,
                marcas.Message,
                marcas.Success,
                marcas.Data
            });
        }

        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> search([FromQuery] string name, [FromQuery] int page, [FromQuery] int limit)
        {
            if (name.Length <= 0)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = $"El name debe tener una longitud mayor a cero.",
                    Error = "Bad Request",
                    Success = false,
                });
            }

            if (page < 0 || limit < 0)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = $"El page o el limit no pueden ser menores a cero.",
                    Error = "Bad Request",
                    Success = false,
                });
            }

            Response<List<MarcaDTO>> marca = await _iMarca.search(name, page, limit);

            if (marca.Data == null)
            {
                return this.StatusCode(500, new
                {
                    marca.StatusCode,
                    marca.Message,
                    marca.Error,
                    marca.Success
                });
            }

            if (marca.Data.Count == 0)
            {
                return this.NotFound( new
                {
                    marca.StatusCode,
                    marca.Message,
                    marca.Error,
                    marca.Success,
                    marca.Data
                });
            }

            return this.Ok(new
            {
                marca.StatusCode,
                marca.Message,
                marca.Success,
                marca.Data
            });
        }


        [HttpGet]
        [Route("searchById/{id}")]
        public async Task<IActionResult> searchById(int id)
        {
            if(id <= 0)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = $"El id no puede ser menor o igual a cero.",
                    Error = "Bad Request",
                    Success = false,
                });
            }


            Response<MarcaDTO> marca = await _iMarca.searchById(id);

            if (!marca.Success)
            {
                return this.StatusCode(500, new
                {
                    marca.StatusCode,
                    marca.Message,
                    marca.Error,
                    marca.Success
                });
            }

            return this.Ok(new
            {
                marca.StatusCode,
                marca.Message,
                marca.Success,
                marca.Data
            });
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> create([FromBody] CreateMarcaDTO createMarcaDTO)
        {
            Response<Marca> marca = await _iMarca.create(createMarcaDTO);

            if (!marca.Success)
            {

                return this.StatusCode(500, new
                {
                    marca.StatusCode,
                    marca.Message,
                    marca.Error,
                    marca.Success
                });
            }

            return this.Ok(new
            {
                marca.StatusCode,
                marca.Message,
                marca.Success,
                marca.Data
            });
        }

        [HttpPatch]
        [Route("update")]
        public async Task<IActionResult> update([FromBody] UpdateMarcaDTO updateMarcaDTO)
        {
            Response<Marca> marca = await _iMarca.update(updateMarcaDTO);

            if (!marca.Success)
            {

                return this.StatusCode(500, new
                {
                    marca.StatusCode,
                    marca.Message,
                    marca.Error,
                    marca.Success
                });
            }

            return this.Ok(new
            {
                marca.StatusCode,
                marca.Message,
                marca.Success,
                marca.Data
            });
        }

        [HttpDelete()]
        [Route("delete/{id}")]
        public async Task<IActionResult> delete(int id)
        {
            Response<Marca> marca = await _iMarca.delete(id);

            if (!marca.Success)
            {

                return this.StatusCode(500, new
                {
                    marca.StatusCode,
                    marca.Message,
                    marca.Error,
                    marca.Success
                });
            }

            return this.Ok(new
            {
                marca.StatusCode,
                marca.Message,
                marca.Success
            });
        }
    }
}

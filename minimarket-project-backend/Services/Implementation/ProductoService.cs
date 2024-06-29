using AutoMapper;
using Microsoft.EntityFrameworkCore;
using tienda_project_backend.Dtos.Producto;
using tienda_project_backend.Models;
using tienda_project_backend.Utilities.Response;

namespace tienda_project_backend.Services.Implementation
{
    public class ProductoService : IProducto
    {
        private readonly DbMinimarketContext _dbcontext;
        private readonly IMapper _mapper;

        public ProductoService(DbMinimarketContext dbcontext, IMapper mapper)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
        }

        public async Task<Response<List<ProductoDTO>>> getAll()
        {
            var response = new Response<List<ProductoDTO>>();

            try
            {
                List<Producto> productos = await (from p in _dbcontext.Producto
                                                  join c in _dbcontext.Categoria on p.IdCategoria equals c.Id
                                                  join m in _dbcontext.Marca on p.IdMarca equals m.Id
                                                  select new Producto
                                                  {
                                                      Id = p.Id,
                                                      Nombre = p.Nombre,
                                                      Descripcion = p.Descripcion,
                                                      RutaImagen = p.RutaImagen,
                                                      Stock = p.Stock,
                                                      Precio = p.Precio,
                                                      Estado = p.Estado,
                                                      FechaCreacion = p.FechaCreacion,
                                                      FechaModificacion = p.FechaModificacion,
                                                      Categoria = new Categoria
                                                      {
                                                          Id = c.Id,
                                                          Nombre = c.Nombre,
                                                          RutaImagen = c.RutaImagen,
                                                          Estado = c.Estado,
                                                          FechaCreacion = c.FechaCreacion,
                                                          FechaModificacion = c.FechaModificacion
                                                      },
                                                      Marca = new Marca
                                                      {
                                                          Id = m.Id,
                                                          Nombre = m.Nombre,
                                                          RutaImagen = m.RutaImagen,
                                                          Estado = m.Estado,
                                                          FechaCreacion = m.FechaCreacion,
                                                          FechaModificacion = m.FechaModificacion
                                                      }
                                                  }).ToListAsync();

                if (productos.Equals(null) || !productos.Any())
                {
                    response.StatusCode = 200;
                    response.Message = "No hay productos disponibles.";
                    response.Success = true;
                    response.Data = [];
                }

                // Mapper para pasar de productos a productos DTO
                List<ProductoDTO> productosDTO = _mapper.Map<List<ProductoDTO>>(productos);

                response.StatusCode = 200;
                response.Message = "Productos obtenidos con éxito.";
                response.Success = true;
                response.Data = productosDTO;
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Message = $"Ocurrió un error: {ex.Message}";
                response.Error = "Internal Server Error";
                response.Success = false;
            }

            return response;
        }
    }
}

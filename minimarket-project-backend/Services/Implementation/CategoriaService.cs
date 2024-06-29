using AutoMapper;
using Microsoft.EntityFrameworkCore;
using tienda_project_backend.Dtos.Categoria;
using tienda_project_backend.Models;
using tienda_project_backend.Utilities.Response;

namespace tienda_project_backend.Services.Implementation
{
    public class CategoriaService : ICategoria
    {
        private readonly DbMinimarketContext _dbcontext;
        private readonly IMapper _mapper;

        public CategoriaService(DbMinimarketContext dbcontext, IMapper mapper)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
        }

        public async Task<Response<List<CategoriaDTO>>> getAll()
        {
            Response<List<CategoriaDTO>> response = new();

            try
            {
                List<Categoria> categorias = await _dbcontext.Categoria.ToListAsync();

                if (categorias.Equals(null) || !categorias.Any())
                {
                    response.StatusCode = 200;
                    response.Message = "No hay categorías disponibles.";
                    response.Success = true;
                    response.Data = [];
                    //response.error = "";
                }

                List<CategoriaDTO> categoriasDTO = _mapper.Map<List<CategoriaDTO>>(categorias);

                response.StatusCode = 200;
                response.Message = "Categorías obtenidas con éxito.";
                response.Success = true;
                response.Data = categoriasDTO;
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

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using tienda_project_backend.Dtos.Marca;
using tienda_project_backend.Models;
using tienda_project_backend.Utilities.Response;

namespace tienda_project_backend.Services.Implementation
{
    public class MarcaService : IMarca
    {
        private readonly DbMinimarketContext _dbcontext;
        private readonly IMapper _mapper;

        public MarcaService(DbMinimarketContext dbcontext, IMapper mapper)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
        }

        private static IQueryable<TEntity> BuildQuery<TEntity>(IQueryable<TEntity> query, int page, int limit) where TEntity : class
        {

            if (page > 0)
            {
                int skipAmount = (page - 1) * limit;
                query = query.Skip(skipAmount);
            }

            if (limit > 0)
            {
                query = query.Take(limit);
            }

            return query;
        }

        private void HandleResponse<TEntity, TDTO>(Response<List<TDTO>> response, List<TEntity> entities)
        {
            if (entities.Count == 0)
            {
                response.StatusCode = 404;
                response.Message = $"No hay {(typeof(TEntity).Name.ToLower())}s disponibles.";
                response.Error = "Not Found";
                response.Success = false;
                response.Data = new List<TDTO>();
            }
            else
            {
                List<TDTO> entitiesDTO = _mapper.Map<List<TDTO>>(entities);

                response.StatusCode = 200;
                response.Message = $"{(typeof(TEntity).Name)}s obtenidas con éxito.";
                response.Success = true;
                response.Data = entitiesDTO;
            }
        }

        private static void HandleException<TEntity>(Response<List<TEntity>> response, Exception ex)
        {
            response.StatusCode = 500;
            response.Message = $"Ocurrió un error: {ex.Message}";
            response.Error = "Internal Server Error";
            response.Success = false;
            response.Data = null;
        }

        public async Task<Response<List<MarcaDTO>>> getAll(int page, int limit)
        {
            Response<List<MarcaDTO>> response = new();

            try
            {
                IQueryable<Marca> query = BuildQuery(_dbcontext.Marca, page, limit);

                List<Marca> marcas = await query.ToListAsync();

                HandleResponse(response, marcas);

            }
            catch (Exception ex)
            {
                HandleException(response, ex);
            }

            return response;
        }

        public async Task<Response<List<MarcaDTO>>> search(string name, int page, int limit)
        {
            Response<List<MarcaDTO>> response = new();

            try
            {
                name = name.ToUpper();
               
                IQueryable<Marca> query = _dbcontext.Marca.Where(m => m.Nombre.Contains(name))  // Ajusta el campo 'Nombre' según tu modelo
                                                          .OrderBy(m => m.Id);

                query = BuildQuery(query, page, limit);

                List<Marca> marcas = await query.ToListAsync();

                HandleResponse(response, marcas);
            }
            catch (Exception ex)
            {
                HandleException(response, ex);
            }

            return response;
        }

        public async Task<Response<MarcaDTO>> searchById(int id)
        {
            Response<MarcaDTO> response = new();

            try
            {
                Marca marca = await _dbcontext.Marca.FindAsync(id);

                if (marca.Equals(null))
                {
                    response.StatusCode = 404;
                    response.Message = $"No existe una marca con el id {id}.";
                    response.Error = "Not Found";
                    response.Success = false;
                    response.Data = new MarcaDTO();
                }

                MarcaDTO marcaDTO = _mapper.Map<MarcaDTO>(marca);

                response.StatusCode = 200;
                response.Message = "Marca obtenida con éxito.";
                response.Success = true;
                response.Data = marcaDTO;
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

        public async Task<Response<Marca>> create(CreateMarcaDTO createMarcaDTO)
        {
            Response<Marca> response = new();

            try
            {
                
                //if (createMarcaDTO.Nombre.Equals(null))
                //{
                //    response.StatusCode = 400;
                //    response.Message = "El campo nombre es requerido";
                //    response.Error = "Bad Request";
                //    response.Success = false;

                //    return response;
                //}

                //if (createMarcaDTO.Estado.Equals(null))
                //{
                //    response.StatusCode = 400;
                //    response.Message = "El campo estado es requerido";
                //    response.Error = "Bad Request";
                //    response.Success = false;

                //    return response;
                //}


                Marca marca = _mapper.Map<Marca>(createMarcaDTO);

                marca.FechaCreacion = DateTime.Now;

                _dbcontext.Marca.Add(marca);
                await _dbcontext.SaveChangesAsync();

                response.StatusCode = 200;
                response.Message = "Marca creada con éxito.";
                response.Success = true;
                response.Data = marca;
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

        public async Task<Response<Marca>> update(UpdateMarcaDTO updateMarcaDTO)
        {
            Response<Marca> response = new();

            try
            {
                Marca marca = await _dbcontext.Marca.FindAsync(updateMarcaDTO.Id);

                if (marca == null)
                {
                    response.StatusCode = 404;
                    response.Message = $"No se encontro a una marca con el id {updateMarcaDTO.Id}";
                    response.Error = "Not Found";
                    response.Success = false;
                }

                marca.Nombre = updateMarcaDTO.Nombre is null ? marca.Nombre : updateMarcaDTO.Nombre;
                marca.RutaImagen = updateMarcaDTO.RutaImagen is null ? marca.RutaImagen : updateMarcaDTO.RutaImagen;
                marca.Estado = updateMarcaDTO.Estado;
                marca.FechaModificacion = DateTime.Now;

                _dbcontext.Marca.Update(marca);
                await _dbcontext.SaveChangesAsync();

                //MarcaDTO responseMarca = _mapper.Map<MarcaDTO>(marca);

                response.StatusCode = 200;
                response.Message = "Marca actualizada con éxito.";
                response.Success = true;
                response.Data = marca;
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

        public async Task<Response<Marca>> delete(int id)
        {
            Response<Marca> response = new();

            try
            {
                Marca marca = await _dbcontext.Marca.FindAsync(id);

                if (marca == null)
                {
                    response.StatusCode = 404;
                    response.Message = $"No se encontro a una marca con el id {id}";
                    response.Error = "Not Found";
                    response.Success = false;
                }

                _dbcontext.Marca.Remove(marca);
                await _dbcontext.SaveChangesAsync();

                response.StatusCode = 200;
                response.Message = $"La marca con el id {marca.Id} fue eliminada con éxito.";
                response.Success = true;
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

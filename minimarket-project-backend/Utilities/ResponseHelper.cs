using AutoMapper;
using minimarket_project_backend.Models.Responses;

namespace minimarket_project_backend.Utilities
{
    public class ResponseHelper(IMapper _mapper)
    {
        public void SetListDataResponse<TEntity, TDTO>(DataResponse<List<TDTO>> response, List<TEntity> entities)
        {
            if (entities.Count == 0)
            {
                response.StatusCode = 404;
                response.Message = $"No hay {(typeof(TEntity).Name.ToLower())}s disponibles.";
                response.Error = "Not Found";
                response.Success = false;
                response.Data = [];
            }
            else
            {
                response.StatusCode = 200;
                response.Message = $"{(typeof(TEntity).Name)}s obtenidas con éxito.";
                response.Success = true;
                response.Data = _mapper.Map<List<TDTO>>(entities);
            }
        }

        public void SetListDataFilterResponse<TEntity>(DataResponse<List<TEntity>> response, List<TEntity> entities)
        {
            if (entities.Count == 0)
            {
                response.StatusCode = 404;
                response.Message = $"No hay {(typeof(TEntity).Name.ToLower())}s disponibles.";
                response.Error = "Not Found";
                response.Success = false;
                response.Data = [];
            }
            else
            {
                response.StatusCode = 200;
                response.Message = $"{(typeof(TEntity).Name)}s obtenidas con éxito.";
                response.Success = true;
                response.Data = entities;
            }
        }

        public void SetDataResponse<TEntity, TDTO>(DataResponse<TDTO> response, TEntity entities, int id)
        {
            TDTO? entitiesDTO = default;

            if (entities == null)
            {
                response.StatusCode = 404;
                response.Message = $"No existe una {typeof(TEntity).Name.ToLower()} con el id {id}.";
                response.Error = "Not Found";
                response.Success = false;
                response.Data = entitiesDTO;
            }
            else
            {

                response.StatusCode = 200;
                response.Message = $"{(typeof(TEntity).Name)} obtenida con éxito.";
                response.Success = true;
                response.Data = _mapper.Map<TDTO>(entities);
            }
        }

        public void SetCreateResponse<TEntity>(ApiResponse response, TEntity entities)
        {
            if (entities != null)
            {
                response.StatusCode = 200;
                response.Message = $"{(typeof(TEntity).Name)} creada con éxito.";
                response.Success = true;
            }
        }

        public  void SetUpdateResponse<TEntity>(ApiResponse response, TEntity entities)
        {
            if (entities != null)
            {
                response.StatusCode = 200;
                response.Message = $"{(typeof(TEntity).Name)} actualizada con éxito.";
                response.Success = true;
            }
        }

        public void SetDeleteResponse<TEntity>(ApiResponse response, TEntity entities)
        {
            if (entities != null)
            {
                response.StatusCode = 200;
                response.Message = $"{(typeof(TEntity).Name)} eliminada con éxito.";
                response.Success = true;
            }
        }

        public void SetNotFoundApiResponse<TEntity>(ApiResponse response, TEntity entities, int id)
        {
            if (entities == null)
            {
                response.StatusCode = 404;
                response.Message = $"No existe una {typeof(TEntity).Name.ToLower()} con el id {id}.";
                response.Error = "Not Found";
                response.Success = false;
            }
        }

        public void SetListErrorResponse<TEntity>(DataResponse<TEntity> response, Exception ex)
        {
            response.StatusCode = 500;
            response.Message = $"Ocurrió un error: {ex.Message}";
            response.Error = "Internal Server Error";
            response.Success = false;
            response.Data = default;
        }

        public void SetStandardErrorResponse(ApiResponse response, Exception ex)
        {
            response.StatusCode = 500;
            response.Message = $"Ocurrió un error: {ex.Message}";
            response.Error = "Internal Server Error";
            response.Success = false;
        }
    }
}

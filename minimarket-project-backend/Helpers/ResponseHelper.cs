using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using minimarket_project_backend.Common.Responses;

namespace minimarket_project_backend.Helpers
{
    public class ResponseHelper
    {
        //private readonly IMapper _mapper = mapper;

        // Mapea la respuesta de la paginación y retorna la respuesta mapeada

        public PaginationResponse<List<TEntity>> CreatePaginationResponse<TEntity>(PaginationResponse<List<TEntity>> entities)
        {
            return new PaginationResponse<List<TEntity>>
            {
                Page = entities.Page,
                PageSize = entities.PageSize,
                TotalRecords = entities.TotalRecords,
                TotalPages = entities.TotalPages,
                HasPreviousPage = entities.HasPreviousPage,
                HasNextPage = entities.HasNextPage,
                //Data = _mapper.Map<List<TDTO>>(entities.Data)
                Data = entities.Data
            };
        }

        // Creación, búsqueda y actualización la respuesta cuando todo salio sasticfactoriamente

        // TEntity entities,int statusCode, string message
        public IActionResult CreateSuccessResponse<TEntity>(TEntity entities, int statusCode, string message, string actionName = null, object routeValues = null)
        {
            // return new OkObjectResult(
            //    new DataResponse<TEntity>
            //    {
            //        StatusCode = statusCode,
            //        Message = message,   
            //        Success = true,
            //        Data = entities
            //    }
            //);

            var response = new DataResponse<TEntity>
            {
                StatusCode = statusCode,
                Message = message,
                Success = true,
                Data = entities
            };

            return actionName != null ? new CreatedAtActionResult(actionName, null, routeValues, response) : new OkObjectResult(response);
        }

        // Eliminación satisfactoria

        public IActionResult CreateSuccessDeleteResponse(string message)
        {
            return new OkObjectResult(
                new ErrorResponse
                {
                    StatusCode = 200,
                    Message = message,
                    Success = true
                }
            );
        }


    }
}

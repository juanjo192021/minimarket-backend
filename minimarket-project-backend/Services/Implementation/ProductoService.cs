using AutoMapper;
using Microsoft.EntityFrameworkCore;
using minimarket_project_backend.Models.Responses;
using minimarket_project_backend.Utilities;
using System.Collections.Generic;
using tienda_project_backend.Dtos.Producto;
using tienda_project_backend.Models;

namespace tienda_project_backend.Services.Implementation
{
    public class ProductoService : IProducto
    {
        private readonly DbMinimarketContext _dbcontext;
        private readonly IMapper _mapper;
        private readonly DataResponse<List<ProductoDTO>> _dataResponseList;
        private readonly DataResponse<ProductoDTO> _dataResponse;
        private readonly ApiResponse _apiResponse;
        private readonly QueryHelper _queryHelper;
        private readonly ResponseHelper _responseHelper;


        public ProductoService(DbMinimarketContext dbcontext, IMapper mapper)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
            _dataResponseList = new DataResponse<List<ProductoDTO>>();
            _dataResponse = new DataResponse<ProductoDTO>();
            _apiResponse = new ApiResponse();
            _queryHelper = new QueryHelper();
            _responseHelper = new ResponseHelper(mapper);

        }

        public async Task<DataResponse<List<ProductoDTO>>> getAll(int page, int limit)
        {
            try
            {
                var query = _queryHelper.BuildQuery(_dbcontext.Producto, page, limit);
                var productos = await (from p in query
                                                  join c in _dbcontext.Categoria on p.IdCategoria equals c.Id
                                                  join m in _dbcontext.Marca on p.IdMarca equals m.Id
                                                  select new ProductoDTO
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
                                                      Categoria = c.Nombre,
                                                      Marca = m.Nombre
                                                  }).ToListAsync();

                _responseHelper.SetListDataFilterResponse(_dataResponseList, productos);
            }
            catch (Exception ex)
            {
                _responseHelper.SetListErrorResponse(_dataResponseList, ex);
            }

            return _dataResponseList;
        }
    }
}

using AutoMapper;
using minimarket_project_backend.Common.Responses;
using minimarket_project_backend.Dtos.Product;
using minimarket_project_backend.Helpers;
using minimarket_project_backend.Utilities;

namespace minimarket_project_backend.Services.Implementation
{
    public class ProductService : IProductService
    {
        //private readonly DbMinimarketContext _dbcontext;
        private readonly IMapper _mapper;
        private readonly DataResponse<List<ProductDTO>> _dataResponseList;
        private readonly DataResponse<ProductDTO> _dataResponse;
        private readonly QueryHelper _queryHelper;
        private readonly ResponseHelper _responseHelper;
        private readonly ErrorResponseHelper _errorResponseHelper;


        public ProductService(/*DbMinimarketContext dbcontext,*/ IMapper mapper)
        {
            //_dbcontext = dbcontext;
            _mapper = mapper;
            _dataResponseList = new DataResponse<List<ProductDTO>>();
            _dataResponse = new DataResponse<ProductDTO>();
            _queryHelper = new QueryHelper();
            _responseHelper = new ResponseHelper();
            _errorResponseHelper = new ErrorResponseHelper();

        }

        public async Task<DataResponse<List<ProductDTO>>> getAll(int page, int limit)
        {
            //try
            //{
            //    var query = _queryHelper.BuildQuery(_dbcontext.Producto, page, limit);
            //    var productos = await (from p in query
            //                                      join c in _dbcontext.Categoria on p.IdCategoria equals c.Id
            //                                      join m in _dbcontext.Marca on p.IdMarca equals m.Id
            //                                      select new ProductoDTO
            //                                      {
            //                                          Id = p.Id,
            //                                          Nombre = p.Nombre,
            //                                          Descripcion = p.Descripcion,
            //                                          RutaImagen = p.RutaImagen,
            //                                          Stock = p.Stock,
            //                                          Precio = p.Precio,
            //                                          Estado = p.Estado,
            //                                          FechaCreacion = p.FechaCreacion,
            //                                          FechaModificacion = p.FechaModificacion,
            //                                          Categoria = c.Nombre,
            //                                          Marca = m.Nombre
            //                                      }).ToListAsync();

            //    _responseHelper.SetListDataFilterResponse(_dataResponseList, productos);
            //}
            //catch (Exception ex)
            //{
            //    _responseHelper.SetListErrorResponse(_dataResponseList, ex);
            //}

            return _dataResponseList;
        }
    }
}

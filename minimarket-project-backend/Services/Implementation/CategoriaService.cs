using AutoMapper;
using Microsoft.EntityFrameworkCore;
using minimarket_project_backend.Dtos.Categoria;
using minimarket_project_backend.Helpers;
using minimarket_project_backend.Models.Responses;
using minimarket_project_backend.Utilities;
using tienda_project_backend.Dtos.Categoria;
using tienda_project_backend.Dtos.Marca;
using tienda_project_backend.Models;

namespace tienda_project_backend.Services.Implementation
{
    public class CategoriaService : ICategoria
    {
        private readonly DbMinimarketContext _dbcontext;
        private readonly IMapper _mapper;
        private readonly DataResponse<List<CategoriaDTO>> _dataResponseList;
        private readonly DataResponse<CategoriaDTO> _dataResponse;
        private readonly ApiResponse _apiResponse;
        private readonly QueryHelper _queryHelper;
        private readonly ResponseHelper _responseHelper;
        private readonly ErrorResponseHelper _errorResponseHelper;

        public CategoriaService(DbMinimarketContext dbcontext, IMapper mapper)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
            _dataResponseList = new DataResponse<List<CategoriaDTO>>();
            _dataResponse = new DataResponse<CategoriaDTO>();
            _apiResponse = new ApiResponse();
            _queryHelper = new QueryHelper();
            _responseHelper = new ResponseHelper(mapper);
            _errorResponseHelper = new ErrorResponseHelper();
        }

        public async Task<DataResponse<List<CategoriaDTO>>> getAll(int page, int limit)
        {
            //try
            //{
            //    var query = _queryHelper.BuildQuery(_dbcontext.Categoria, page, limit);
            //    var categorias = await query.ToListAsync();
            //    return _responseHelper.SetListResponse<Categoria, CategoriaDTO>(categorias);
            //}
            //catch (Exception ex)
            //{
            //    return _errorResponseHelper.SetErrorForListResponse<List<CategoriaDTO>>(ex);
            //}
            throw new NotImplementedException();
        }

        public Task<DataResponse<List<CategoriaDTO>>> search(string name, int page, int limit)
        {
            throw new NotImplementedException();
        }

        public Task<DataResponse<CategoriaDTO>> searchById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> create(CreateCategoriaDTO createCategoriaDTO)
        {
            throw new NotImplementedException();
        }
        public Task<ApiResponse> update(UpdateCategoriaDTO updateCategoriaDTO)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}

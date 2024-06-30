using AutoMapper;
using Microsoft.EntityFrameworkCore;
using minimarket_project_backend.Models.Responses;
using minimarket_project_backend.Utilities;
using tienda_project_backend.Dtos.Marca;
using tienda_project_backend.Models;

namespace tienda_project_backend.Services.Implementation
{
    public class MarcaService : IMarca
    {
        private readonly DbMinimarketContext _dbcontext;
        private readonly IMapper _mapper;
        private readonly DataResponse<List<MarcaDTO>> _dataResponseList;
        private readonly DataResponse<MarcaDTO> _dataResponse;
        private  readonly ApiResponse _apiResponse;
        private readonly QueryHelper _queryHelper;
        private readonly ResponseHelper _responseHelper;


        public MarcaService(DbMinimarketContext dbcontext, IMapper mapper)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
            _dataResponseList = new DataResponse<List<MarcaDTO>>();
            _dataResponse = new DataResponse<MarcaDTO>();
            _apiResponse = new ApiResponse();
            _queryHelper = new QueryHelper();
            _responseHelper = new ResponseHelper(mapper);
        }
        

        public async Task<DataResponse<List<MarcaDTO>>> getAll(int page, int limit)
        {
            try
            {
                var query = _queryHelper.BuildQuery(_dbcontext.Marca, page, limit);
                var marcas = await query.ToListAsync();
                _responseHelper.SetListDataResponse(_dataResponseList, marcas);
            }
            catch (Exception ex)
            {
                _responseHelper.SetListErrorResponse(_dataResponseList, ex);
            }
            return _dataResponseList;
        }

        public async Task<DataResponse<List<MarcaDTO>>> search(string name, int page, int limit)
        {
            try
            {
                name = name.ToUpper();          
                IQueryable<Marca> query = _dbcontext.Marca.Where(m => m.Nombre.Contains(name)) 
                                                                                         .OrderBy(m => m.Id);
                query = _queryHelper.BuildQuery(query, page, limit);
                var marcas = await query.ToListAsync();
                _responseHelper.SetListDataResponse(_dataResponseList, marcas);
            }
            catch (Exception ex)
            {
                _responseHelper.SetListErrorResponse(_dataResponseList, ex);
            }
            return _dataResponseList;
        }
        
        public async Task<DataResponse<MarcaDTO>> searchById(int id)
        {
            try
            {
                var marca = await _dbcontext.Marca.FindAsync(id);
                _responseHelper.SetDataResponse(_dataResponse, marca, id);
            }
            catch (Exception ex)
            {
                _responseHelper.SetStandardErrorResponse(_dataResponse, ex);
            }
            return _dataResponse;
        }

        public async Task<ApiResponse> create(CreateMarcaDTO createMarcaDTO)
        {
            try
            {
                var marca = _mapper.Map<Marca>(createMarcaDTO);

                marca.FechaCreacion = DateTime.Now;

                _dbcontext.Marca.Add(marca);
                await _dbcontext.SaveChangesAsync();
                _responseHelper.SetCreateResponse(_apiResponse, marca);
            }
            catch (Exception ex)
            {
                _responseHelper.SetStandardErrorResponse(_apiResponse, ex);
            }
            return _apiResponse;
        }

        public async Task<ApiResponse> update(UpdateMarcaDTO updateMarcaDTO)
        {
            try
            {
                var marca = await _dbcontext.Marca.FindAsync(updateMarcaDTO.Id);

                if (marca == null)
                {
                    _responseHelper.SetNotFoundApiResponse(_apiResponse, marca,updateMarcaDTO.Id);
                }
                else
                {
                    marca.Nombre = updateMarcaDTO.Nombre ?? marca.Nombre;
                    marca.RutaImagen = updateMarcaDTO.RutaImagen ?? marca.RutaImagen;
                    marca.Estado = updateMarcaDTO.Estado;
                    marca.FechaModificacion = DateTime.Now;

                    _dbcontext.Marca.Update(marca);
                    await _dbcontext.SaveChangesAsync();
                    _responseHelper.SetUpdateResponse(_apiResponse, marca);
                }
            }
            catch (Exception ex)
            {
                _responseHelper.SetStandardErrorResponse(_apiResponse, ex);
            }
            return _apiResponse;
        }

        public async Task<ApiResponse> delete(int id)
        {
            try
            {
                var marca = await _dbcontext.Marca.FindAsync(id);
                if (marca == null)
                {
                    _responseHelper.SetNotFoundApiResponse(_apiResponse, marca, id);
                }
                else
                {
                    _dbcontext.Marca.Remove(marca);
                    await _dbcontext.SaveChangesAsync();
                    _responseHelper.SetDeleteResponse(_apiResponse, marca);
                }
            }
            catch (Exception ex)
            {
                _responseHelper.SetStandardErrorResponse(_apiResponse, ex);
            }
            return _apiResponse;
        }
    }
}

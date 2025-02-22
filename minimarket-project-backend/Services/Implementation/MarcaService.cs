using AutoMapper;
using Microsoft.EntityFrameworkCore;
using minimarket_project_backend.Helpers;
using minimarket_project_backend.Models.Responses;
using minimarket_project_backend.Services;
using minimarket_project_backend.Utilities;
using tienda_project_backend.Dtos.Marca;
using tienda_project_backend.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace tienda_project_backend.Services.Implementation
{
    public class MarcaService : IMarca
    {
        private readonly DbMinimarketContext _dbcontext;
        private readonly IMapper _mapper;
        private readonly DataResponse<List<MarcaDTO>> _dataResponseList;
        private readonly DataResponse<MarcaDTO> _dataResponse;
        private readonly QueryHelper _queryHelper;
        private readonly ResponseHelper _responseHelper;
        private readonly ErrorResponseHelper _errorResponseHelper;
        private readonly IFirebaseStorageService _firebaseStorageService;

        public MarcaService(
            DbMinimarketContext dbcontext, 
            IMapper mapper, 
            IFirebaseStorageService firebaseStorageService)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
            _dataResponseList = new DataResponse<List<MarcaDTO>>();
            _dataResponse = new DataResponse<MarcaDTO>();
            _queryHelper = new QueryHelper();
            _responseHelper = new ResponseHelper(mapper);
            _errorResponseHelper = new ErrorResponseHelper();
            _firebaseStorageService = firebaseStorageService;
        }
        

        public async Task<PaginationResponse<List<MarcaDTO>>> GetAll(string name, int page, int limit)
        {
            IQueryable<Marca> query = _dbcontext.Marca;

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(m => m.Nombre.Contains(name))
                             .OrderBy(m => m.Id);
            }

            int totalRecords = await query.CountAsync();

            List<Marca> data = await _queryHelper.GetPaginatedList(query, page, limit);

            var paginationResponse = new PaginationResponse<List<Marca>> {
                Page = page,
                PageSize = limit,
                TotalRecords = totalRecords,
                TotalPages = (int)Math.Ceiling((double)totalRecords / limit),
                HasPreviousPage = page > 1,
                HasNextPage = page < (int)Math.Ceiling((double)totalRecords / limit),
                Data = data
            };

            //  Retorna la clase de respuesta mapeada
            return _responseHelper.MapToPaginationResponse<Marca, MarcaDTO>(paginationResponse);
        }

        public async Task<MarcaDTO?> SearchById(int id)
        {
            var marca = await _dbcontext.Marca.FindAsync(id);

            if (marca == null)
                return null;

            return _mapper.Map<MarcaDTO>(marca);
        }

        public async Task<MarcaDTO?> Create(CreateMarcaDTO createMarcaDTO)
        {
            var response = new MarcaDTO();
            try
            {
                var marca = _mapper.Map<Marca>(createMarcaDTO);

                marca.FechaCreacion = DateTime.Now;
                marca.FechaModificacion = null;

                // Si hay una imagen, subirla a Firebase
                if (createMarcaDTO.Imagen != null)
                {
                    using var stream = createMarcaDTO.Imagen.OpenReadStream();
                    string fileName = $"{Guid.NewGuid()}_{createMarcaDTO.Imagen.FileName}";

                    string imageUrl = await _firebaseStorageService.UploadFileAsync(stream, fileName);
                    marca.RutaImagen = imageUrl; // Guardar la URL en la BD
                }

                _dbcontext.Marca.Add(marca);
                int filasAfectadas = await _dbcontext.SaveChangesAsync();

                if (marca.Id > 0 && filasAfectadas > 0)
                {
                    return _mapper.Map<MarcaDTO>(marca);
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<MarcaDTO?> Update(UpdateMarcaDTO updateMarcaDTO)
        {
            try
            {
                var marca = await _dbcontext.Marca.FindAsync(updateMarcaDTO.Id);

                if (marca == null) return null;

                marca.Nombre = updateMarcaDTO.Nombre ?? marca.Nombre;
                marca.RutaImagen = updateMarcaDTO.RutaImagen ?? marca.RutaImagen;
                marca.Estado = updateMarcaDTO.Estado ?? marca.Estado;
                marca.FechaModificacion = DateTime.Now;

                _dbcontext.Marca.Update(marca);

                int filasAfectadas = await _dbcontext.SaveChangesAsync();

                if (marca.Id > 0 && filasAfectadas > 0)
                {
                    return _mapper.Map<MarcaDTO>(marca);
                }

                return null;

            }
            catch (Exception)
            {
                return null;
            }

        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var marca = await _dbcontext.Marca.FindAsync(id);
                if (marca == null) return false;

                marca.Estado = false;

                _dbcontext.Marca.Update(marca);

                int filasAfectadas = await _dbcontext.SaveChangesAsync();

                if (marca.Id > 0 && filasAfectadas > 0)
                {
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

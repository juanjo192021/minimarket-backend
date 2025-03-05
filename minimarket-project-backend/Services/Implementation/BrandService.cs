using AutoMapper;
using Microsoft.EntityFrameworkCore;
using minimarket_project_backend.Common.Responses;
using minimarket_project_backend.Dtos.Brand;
using minimarket_project_backend.Helpers;
using minimarket_project_backend.Models;
using minimarket_project_backend.Utilities;

namespace minimarket_project_backend.Services.Implementation
{
    public class BrandService : IBrandService
    {
        private readonly DbMinimarketContext _dbcontext;
        private readonly IMapper _mapper;
        private readonly QueryHelper _queryHelper;
        private readonly ResponseHelper _responseHelper;
        private readonly IFirebaseStorageService _firebaseStorageService;
        private readonly IImageManagerService _imageManagerService;

        public BrandService(
            DbMinimarketContext dbcontext, 
            IMapper mapper, 
            IFirebaseStorageService firebaseStorageService,
            IImageManagerService imageManagerService)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
            _queryHelper = new QueryHelper();
            _responseHelper = new ResponseHelper();
            _firebaseStorageService = firebaseStorageService;
            _imageManagerService = imageManagerService;
        }
        

        public async Task<PaginationResponse<List<Brand>>> GetAll(string name, int page, int limit)
        {
            IQueryable<Brand> query = _dbcontext.Brands;

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(m => m.Name.Contains(name))
                             .OrderBy(m => m.Id);
            }

            int totalRecords = await query.CountAsync();

            List<Brand> data = await _queryHelper.GetPaginatedList(query, page, limit);

            var paginationResponse = new PaginationResponse<List<Brand>> {
                Page = page,
                PageSize = limit,
                TotalRecords = totalRecords,
                TotalPages = (int)Math.Ceiling((double)totalRecords / limit),
                HasPreviousPage = page > 1,
                HasNextPage = page < (int)Math.Ceiling((double)totalRecords / limit),
                Data = data
            };

            return _responseHelper.CreatePaginationResponse<Brand>(paginationResponse);
        }

        public async Task<Brand?> SearchById(int id)
        {
            try
            {
                var marca = await _dbcontext.Brands.FindAsync(id);

                if (marca == null)
                    return null;

                return marca;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Brand?> SearchByName(string name)
        {
            try
            {
                var marca = await _dbcontext.Brands.FirstOrDefaultAsync(x => x.Name == name);

                if (marca == null)
                    return null;

                return marca;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Brand?> Create(BrandRequestDTO brandRequestDTO)
        {
            try
            {
                var brand = _mapper.Map<Brand>(brandRequestDTO);

                brand.CreationDate = DateTime.Now;

                brand.BrandImageUrl = await _imageManagerService.UploadImageAsync(brandRequestDTO.fileImage, "Brands");

                _dbcontext.Brands.Add(brand);
                int filasAfectadas = await _dbcontext.SaveChangesAsync();

                if (brand.Id > 0 && filasAfectadas > 0)
                {
                    return brand;
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Brand?> Update(Brand brand, BrandRequestDTO brandRequestDTO)
        {
            try
            {
                
                brand.Name = brandRequestDTO.name ?? brand.Name;

                brand.BrandImageUrl = await _imageManagerService.UploadImageAsync(brandRequestDTO.fileImage, "Brands", brand.BrandImageUrl);
                brand.Status = brandRequestDTO.status ?? brand.Status;
                brand.LastUpdateDate = DateTime.Now;

                _dbcontext.Brands.Update(brand);

                int filasAfectadas = await _dbcontext.SaveChangesAsync();

                if (brand.Id > 0 && filasAfectadas > 0)
                {
                    return brand;
                }

                return null;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> Deactivated(Brand brand)
        {
            try
            {
                brand.Status = false;

                _dbcontext.Brands.Update(brand);

                int filasAfectadas = await _dbcontext.SaveChangesAsync();

                if (brand.Id > 0 && filasAfectadas > 0)
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


        public async Task<bool> Delete(Brand brand)
        {
            try
            {
                _dbcontext.Brands.Remove(brand);

                int filasAfectadas = await _dbcontext.SaveChangesAsync();

                if (brand.Id > 0 && filasAfectadas > 0)
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

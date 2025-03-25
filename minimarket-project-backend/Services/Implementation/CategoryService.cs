using AutoMapper;
using Microsoft.EntityFrameworkCore;
using minimarket_project_backend.Common.Responses;
using minimarket_project_backend.Dtos.Brand;
using minimarket_project_backend.Dtos.Category;
using minimarket_project_backend.Helpers;
using minimarket_project_backend.Models;
using minimarket_project_backend.Utilities;

namespace minimarket_project_backend.Services.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly DbMinimarketContext _dbcontext;
        private readonly IMapper _mapper;
        private readonly QueryHelper _queryHelper;
        private readonly ResponseHelper _responseHelper;
        private readonly IImageManagerService _imageManagerService;

        public CategoryService(
            DbMinimarketContext dbcontext,
            IMapper mapper,
            IImageManagerService imageManagerService) 
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
            _queryHelper = new QueryHelper();
            _responseHelper = new ResponseHelper();
            _imageManagerService = imageManagerService;
        }

        public Task<PaginationResponse<List<Category>>> GetAll(string name, int page, int limit)
        {
            throw new NotImplementedException();
        }

        public async Task<Category?> SearchById(int id)
        {
            try
            {
                var category = await _dbcontext.Categories.FindAsync(id);

                if (category == null)
                    return null;

                return category;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Category?> SearchByName(string name)
        {
            try
            {
                var category = await _dbcontext.Categories.FirstOrDefaultAsync(x => x.Name == name);

                if (category == null)
                    return null;

                return category;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Category?> Create(CategoryRequestDTO categoryRequestDTO)
        {
            try
            {
                var category = _mapper.Map<Category>(categoryRequestDTO);
                category.CreationDate = DateTime.Now;
                category.ImageUrl = await _imageManagerService.UploadImageAsync(categoryRequestDTO.FileImage, "Categories");

                _dbcontext.Categories.Add(category);
                int filasAfectadas = await _dbcontext.SaveChangesAsync();

                if (category.Id > 0 && filasAfectadas > 0)
                {
                    return category;
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}

using AutoMapper;
using minimarket_project_backend.Dtos.Category;
using minimarket_project_backend.Models;

namespace minimarket_project_backend.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryRequestDTO, Category>();
        }
    }
}

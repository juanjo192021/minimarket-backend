using AutoMapper;
using minimarket_project_backend.Dtos.Brand;
using minimarket_project_backend.Models;

namespace minimarket_project_backend.Profiles
{
    public class BrandProfile: Profile
    {
        public BrandProfile() 
        {
            //CreateMap<Brand, BrandDTO>();
            CreateMap<BrandRequestDTO, Brand>();
        }
    }
}

using AutoMapper;
using minimarket_project_backend.Dtos.UserType;
using minimarket_project_backend.Models;

namespace minimarket_project_backend.Profiles
{
    public class UserTypeProfile: Profile
    {
        public UserTypeProfile()
        {
            //Retorna un BrandDTO a partir de un Brand
            //CreateMap<Brand, BrandDTO>();
            //Retorna un Brand a partir de un BrandRequestDTO
            CreateMap<UserTypeRequestDTO, UserType>();
        }
    }
}

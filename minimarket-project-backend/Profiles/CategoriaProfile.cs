using AutoMapper;
using tienda_project_backend.Dtos.Categoria;
using tienda_project_backend.Models;

namespace tienda_project_backend.Profiles
{
    public class CategoriaProfile : Profile
    {
        public CategoriaProfile()
        {
            CreateMap<Categoria, CategoriaDTO>();
        }
    }
}

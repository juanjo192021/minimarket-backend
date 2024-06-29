using AutoMapper;
using tienda_project_backend.Dtos.Producto;
using tienda_project_backend.Models;
namespace tienda_project_backend.Profiles
{
    public class ProductoProfile : Profile
    {
        public ProductoProfile()
        {

            CreateMap<Producto, ProductoDTO>()
                .ForMember(dest => dest.Categoria, opt => opt.MapFrom(src => src.Categoria.Nombre))
                .ForMember(dest => dest.Marca, opt => opt.MapFrom(src => src.Marca.Nombre));

        }
    }
}

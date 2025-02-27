using AutoMapper;
namespace minimarket_project_backend.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            /*
            CreateMap<Producto, ProductoDTO>()
                .ForMember(dest => dest.Categoria, opt => opt.MapFrom(src => src.Categoria.Nombre))
                .ForMember(dest => dest.Marca, opt => opt.MapFrom(src => src.Marca.Nombre));*/

        }
    }
}

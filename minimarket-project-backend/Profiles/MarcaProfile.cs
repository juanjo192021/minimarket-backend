using AutoMapper;
using tienda_project_backend.Dtos.Marca;
using tienda_project_backend.Models;

namespace tienda_project_backend.Profiles
{
    public class MarcaProfile: Profile
    {
        public MarcaProfile() 
        {
            CreateMap<Marca, MarcaDTO>();
        }
    }
}

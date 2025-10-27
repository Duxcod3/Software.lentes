using AutoMapper;
using Lente.Domain.Entities;
using Software.lentes.DTOs;

namespace Software.lentes.Mapping
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<Usuario, UsuarioDTO>();
            CreateMap<UsuarioDTO, Usuario>();
            CreateMap<UsuarioAtualizarDTO, Usuario>();



        }
    }
}
    


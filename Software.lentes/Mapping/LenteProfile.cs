using AutoMapper;
using Lente.Domain.Entities;
using Lente.DTOs;
using Software.lentes.DTOs;

namespace Software.lentes.Mapping
{
    public class LenteProfile : Profile
    {
        public LenteProfile()
        {
            // Mapeamento de LenteModelo para LenteDTO
            CreateMap<LenteModelo, LenteDTO>()
                .ForMember(dest => dest.DiagonalMaior, opt => opt.MapFrom(src => src.Diagonal))
                .ForMember(dest => dest.Horizontal, opt => opt.MapFrom(src => src.Horizontal))
                .ForMember(dest => dest.Vertical, opt => opt.MapFrom(src => src.Vertical))
                .ForMember(dest => dest.Ponte, opt => opt.MapFrom(src => src.Ponte))   // Novo
                .ForMember(dest => dest.Lado, opt => opt.MapFrom(src => src.Lado));    // Novo

            // Mapeamento de LenteDTO para LenteModelo
            CreateMap<LenteDTO, LenteModelo>()
                .ForMember(dest => dest.Diagonal, opt => opt.MapFrom(src => src.DiagonalMaior))
                .ForMember(dest => dest.Horizontal, opt => opt.MapFrom(src => src.Horizontal))
                .ForMember(dest => dest.Vertical, opt => opt.MapFrom(src => src.Vertical))
                .ForMember(dest => dest.Ponte, opt => opt.MapFrom(src => src.Ponte))   // Novo
                .ForMember(dest => dest.Lado, opt => opt.MapFrom(src => src.Lado));    // Novo

            // Mapeamento de LenteAtualizarDTO para LenteModelo
            CreateMap<LenteAtualizarDTO, LenteModelo>()
                .ForMember(dest => dest.Diagonal, opt => opt.MapFrom(src => src.DiagonalMaior))
                .ForMember(dest => dest.Horizontal, opt => opt.MapFrom(src => src.Horizontal))
                .ForMember(dest => dest.Vertical, opt => opt.MapFrom(src => src.Vertical))
                .ForMember(dest => dest.Ponte, opt => opt.MapFrom(src => src.Ponte))   // Novo
                .ForMember(dest => dest.Lado, opt => opt.MapFrom(src => src.Lado));    // Novo
        }
    }
}

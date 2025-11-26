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
            // Mapeamento de LenteModelo → LenteDTO
            CreateMap<LenteModelo, LenteDTO>()
                .ForMember(dest => dest.DiagonalMaior, opt => opt.MapFrom(src => src.DiagonalMaior))  // Continua compatível
                .ForMember(dest => dest.Horizontal, opt => opt.MapFrom(src => src.Horizontal))
                .ForMember(dest => dest.Vertical, opt => opt.MapFrom(src => src.Vertical))
                .ForMember(dest => dest.Ponte, opt => opt.MapFrom(src => src.Ponte))
                .ForMember(dest => dest.Lado, opt => opt.MapFrom(src => src.Lado))
                .ForMember(dest => dest.Job, opt => opt.MapFrom(src => src.Job)); // ✅ Novo campo

            // Mapeamento de LenteDTO → LenteModelo
            CreateMap<LenteDTO, LenteModelo>()
                .ForMember(dest => dest.DiagonalMaior, opt => opt.MapFrom(src => src.DiagonalMaior))
                .ForMember(dest => dest.Horizontal, opt => opt.MapFrom(src => src.Horizontal))
                .ForMember(dest => dest.Vertical, opt => opt.MapFrom(src => src.Vertical))
                .ForMember(dest => dest.Ponte, opt => opt.MapFrom(src => src.Ponte))
                .ForMember(dest => dest.Lado, opt => opt.MapFrom(src => src.Lado))
                .ForMember(dest => dest.Job, opt => opt.MapFrom(src => src.Job)); // ✅ Novo campo

            // Mapeamento de LenteAtualizarDTO → LenteModelo
            CreateMap<LenteAtualizarDTO, LenteModelo>()
                .ForMember(dest => dest.DiagonalMaior, opt => opt.MapFrom(src => src.DiagonalMaior))
                .ForMember(dest => dest.Horizontal, opt => opt.MapFrom(src => src.Horizontal))
                .ForMember(dest => dest.Vertical, opt => opt.MapFrom(src => src.Vertical))
                .ForMember(dest => dest.Ponte, opt => opt.MapFrom(src => src.Ponte))
                .ForMember(dest => dest.Lado, opt => opt.MapFrom(src => src.Lado))
                .ForMember(dest => dest.Job, opt => opt.MapFrom(src => src.Job)); // ✅ Novo campo
        }
    }
}

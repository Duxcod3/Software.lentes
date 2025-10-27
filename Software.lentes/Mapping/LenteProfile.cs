using AutoMapper;
using Lente.Domain.Entities;
using Software.lentes.DTOs;

namespace Software.lentes.Mapping
{
    public class LenteProfile : Profile
    {
        public LenteProfile()
        {
            // Mapeamento de LenteModelo para LenteDTO
            CreateMap<LenteModelo, LenteDTO>()
                .ForMember(dest => dest.DiagonalMaior, opt => opt.MapFrom(src => src.Diagonal))  // Mapeando Diagonal para DiagonalMaior
                .ForMember(dest => dest.Horizontal, opt => opt.MapFrom(src => src.Horizontal))
                .ForMember(dest => dest.Vertical, opt => opt.MapFrom(src => src.Vertical));

            // Mapeamento de LenteDTO para LenteModelo
            CreateMap<LenteDTO, LenteModelo>()
                .ForMember(dest => dest.Diagonal, opt => opt.MapFrom(src => src.DiagonalMaior))  // Mapeando DiagonalMaior para Diagonal
                .ForMember(dest => dest.Horizontal, opt => opt.MapFrom(src => src.Horizontal))
                .ForMember(dest => dest.Vertical, opt => opt.MapFrom(src => src.Vertical));

            // Mapeamento de LenteAtualizarDTO para LenteModelo
            CreateMap<LenteAtualizarDTO, LenteModelo>()
                .ForMember(dest => dest.Diagonal, opt => opt.MapFrom(src => src.DiagonalMaior))  // Mapeando DiagonalMaior para Diagonal
                .ForMember(dest => dest.Horizontal, opt => opt.MapFrom(src => src.Horizontal))
                .ForMember(dest => dest.Vertical, opt => opt.MapFrom(src => src.Vertical));
        }
    }
}

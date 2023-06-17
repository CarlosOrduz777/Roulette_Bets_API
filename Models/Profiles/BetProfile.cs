using AutoMapper;
using RouletteBetsApi.Models.Dtos;

namespace RouletteBetsApi.Models.Profiles
{
    public class BetProfile:Profile
    {
        public BetProfile() {
            CreateMap<BetDto, Bet>()
                .ForMember(
                    dest => dest.rouletteId,
                    opt => opt.MapFrom(src => $"{src.rouletteId}")
                )
                .ForMember(
                    dest => dest.number,
                    opt => opt.MapFrom(src => $"{src.number}")
                )
                .ForMember(
                    dest => dest.quantity,
                    opt => opt.MapFrom(src => $"{src.quantity}")
                )
                .ForMember(
                    dest => dest.color,
                    opt => opt.MapFrom(src => $"{src.color}")
                );
        }
    }
}

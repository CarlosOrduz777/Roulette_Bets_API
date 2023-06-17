using AutoMapper;
using RouletteBetsApi.Models.Dtos;

namespace RouletteBetsApi.Models.Profiles
{
    public class RouletteProfile:Profile
    {
        public RouletteProfile() {
            CreateMap<RouletteDto, Roulette>()
                .ForMember(
                    dest => dest.name,
                    opt => opt.MapFrom(src => $"{src.name}")
                )
                .ForMember(
                    dest => dest.state,
                    opt => opt.MapFrom(src => $"{src.state}")
                );
        }
    }
}

using AutoMapper;
using RouletteBetsApi.Models.Dtos;

namespace RouletteBetsApi.Models.Profiles
{
    public class UserProfile:Profile
    {
        public UserProfile() {
            CreateMap<UserDto, User>()
                .ForMember(
                    dest => dest.UserName,
                    opt => opt.MapFrom(src => $"{src.UserName}")
                )
                .ForMember(
                    dest => dest.DisplayName,
                    opt => opt.MapFrom(src => $"{src.DisplayName}")
                )
                .ForMember(
                    dest => dest.Email,
                    opt => opt.MapFrom(src => $"{src.Email}")
                )
                .ForMember(
                    dest => dest.Password,
                    opt => opt.MapFrom(src => $"{src.Password}")
                );
        }
    }
}

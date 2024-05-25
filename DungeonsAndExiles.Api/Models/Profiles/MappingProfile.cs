using AutoMapper;
using DungeonsAndExiles.Api.DTOs.Player;
using DungeonsAndExiles.Api.DTOs.User;
using DungeonsAndExiles.Api.Models.Domain;

namespace DungeonsAndExiles.Api.Models.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            //user
            CreateMap<UserLoginDto, User>();
            CreateMap<UserRegisterDto, User>();
            CreateMap<UserUpdateDto, User>()
                .ForMember(dest => dest.RoleId, opt => opt.Ignore());

            //player
            CreateMap<PlayerDto, Player>();
            CreateMap<PlayerUpdateDto, Player>();

            //
        }
    }
}

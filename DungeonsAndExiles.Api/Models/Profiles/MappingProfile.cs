using AutoMapper;
using DungeonsAndExiles.Api.DTOs.Player;
using DungeonsAndExiles.Api.DTOs.User;
using DungeonsAndExiles.Api.Models.Domain;
using DungeonsAndExiles.Api.ViewModels;

namespace DungeonsAndExiles.Api.Models.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {

            //user
            CreateMap<UserLoginDto, User>().ReverseMap();
            CreateMap<UserRegisterDto, User>().ReverseMap();
            CreateMap<UserUpdateDto, User>()
                .ForMember(dest => dest.RoleId, opt => opt.Ignore());
            CreateMap<UserVM, User>().ReverseMap();

            //player
            CreateMap<PlayerDto, Player>().ReverseMap(); 
            CreateMap<PlayerUpdateDto, Player>().ReverseMap();
            CreateMap<PlayerVM, Player>().ReverseMap();

            //item
            CreateMap<ItemVM, Item>().ReverseMap();


        }
    }
}

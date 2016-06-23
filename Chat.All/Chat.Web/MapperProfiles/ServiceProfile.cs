using AutoMapper;
using Chat.EntityModel;
using Chat.Infrastructure.Dto;
using Chat.Web.Models;

namespace Chat.Web.MapperProfiles
{
    public class ServiceProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, ApplicationUser>();
        }
    }
}
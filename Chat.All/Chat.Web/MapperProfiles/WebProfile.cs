using AutoMapper;
using Chat.Infrastructure.ViewModels;
using Chat.Web.Models;

namespace Chat.Web.MapperProfiles
{
    public class WebProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<RegisterUserModel, ApplicationUser>();
        }
    }
}
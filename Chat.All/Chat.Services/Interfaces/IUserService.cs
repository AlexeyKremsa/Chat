using Chat.Infrastructure.Dto;
using Chat.Infrastructure.ViewModels;

namespace Chat.Services.Interfaces
{
    public interface IUserService
    {
        int RegisterUser(RegisterUserModel userModel);
        bool ValidateUser(string email, string password);
        UserDto GetUserByEmail(string email);
    }
}

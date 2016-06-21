using Chat.Infrastructure.ViewModels;

namespace Chat.Services.Interfaces
{
    public interface IUserService
    {
        int RegisterUser(RegisterUserModel userModel);
    }
}

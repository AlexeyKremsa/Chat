using Chat.Web.Models;

namespace Chat.Services.Interfaces
{
    public interface IUserService
    {
        int RegisterUser(RegisterUserModel userModel);
    }
}

using Chat.EntityModel;

namespace Chat.Services.Security
{
    public interface IPasswordHelper
    {
        PasswordModel HashPassword(string password);
        bool IsPasswordValid(User user, string password);
    }
}

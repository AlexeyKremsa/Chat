using Chat.EntityModel;
using Chat.Infrastructure;

namespace Chat.Services.Security
{
    public interface IPasswordHelper
    {
        PasswordModel HashPassword(string password);
        bool IsPasswordValid(User user, string password);
    }
}

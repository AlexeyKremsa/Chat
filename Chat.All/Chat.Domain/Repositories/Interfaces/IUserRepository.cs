using Chat.EntityModel;

namespace Chat.Domain.Repositories.Interfaces
{
    public interface IUserRepository
    {
        int AddUser(User userModel);
        User GetUserByEmail(string email);
    }
}

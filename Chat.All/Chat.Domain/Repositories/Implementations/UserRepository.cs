using System.Data.Entity;
using Chat.Domain.Repositories.Interfaces;
using Chat.EntityModel;

namespace Chat.Domain.Repositories.Implementations
{
    public class UserRepository: BaseRepository<User>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {
        }

        public int AddUser(User userModel)
        {
            ThrowIfNull(userModel);

            Insert(userModel);
            _context.SaveChanges();

            return userModel.Id;
        }
    }
}

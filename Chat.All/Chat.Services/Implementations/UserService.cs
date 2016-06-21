using Chat.Domain.Repositories.Interfaces;
using Chat.EntityModel;
using Chat.Infrastructure.ViewModels;
using Chat.Services.Interfaces;
using Chat.Services.Security;

namespace Chat.Services.Implementations
{
    public class UserService : BaseService, IUserService
    {
        private readonly IPasswordHelper _passwordHelper;
        private readonly IUserRepository _userRepository;

        public UserService(IPasswordHelper passwordHelper,
            IUserRepository userRepository)
        {
            ThrowIfNull(passwordHelper);
            ThrowIfNull(userRepository);

            _passwordHelper = passwordHelper;
            _userRepository = userRepository;
        }

        public int RegisterUser(RegisterUserModel userModel)
        {
            var password = _passwordHelper.HashPassword(userModel.Password);

            var newUser = new User()
            {
                Email = userModel.Email,
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                PasswordHash = password.PasswordsHash,
                PasswordSalt = password.PasswordSalt
            };

            return _userRepository.AddUser(newUser);
        }
    }
}

using AutoMapper;
using Chat.Domain.Repositories.Interfaces;
using Chat.EntityModel;
using Chat.Infrastructure.Dto;
using Chat.Infrastructure.ViewModels;
using Chat.Services.Interfaces;
using Chat.Services.Security;

namespace Chat.Services.Implementations
{
    public class UserService : BaseService, IUserService
    {
        private readonly IPasswordHelper _passwordHelper;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IMapper mapper,
            IPasswordHelper passwordHelper,
            IUserRepository userRepository)
        {
            ThrowIfNull(passwordHelper);
            ThrowIfNull(userRepository);

            _mapper = mapper;
            _passwordHelper = passwordHelper;
            _userRepository = userRepository;
        }

        public int RegisterUser(RegisterUserModel userModel)
        {
            ThrowIfNull(userModel);

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

        public bool ValidateUser(string email, string password)
        {
            ThrowIfNull(email);
            ThrowIfNull(password);

            var user = _userRepository.GetUserByEmail(email);

            if (user != null)
            {
                return _passwordHelper.IsPasswordValid(user, password);
            }

            return false;
        }

        public UserDto GetUserByEmail(string email)
        {
            ThrowIfNull(email);

            var userEntity = _userRepository.GetUserByEmail(email);

            return _mapper.Map<UserDto>(userEntity);
        }

        public bool CanCrateUser(string email)
        {
            ThrowIfNull(email);

            var userEntity = _userRepository.GetUserByEmail(email);

            return userEntity == null;
        }
    }
}

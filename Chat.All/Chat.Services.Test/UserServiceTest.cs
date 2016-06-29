using System;
using AutoMapper;
using Chat.Domain.Repositories.Interfaces;
using Chat.EntityModel;
using Chat.Infrastructure;
using Chat.Infrastructure.Dto;
using Chat.Infrastructure.ViewModels;
using Chat.Services.Implementations;
using Chat.Services.Interfaces;
using Chat.Services.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Chat.Services.Test
{
    [TestClass]
    public class UserServiceTest
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private Mock<IPasswordHelper> _passwordHelperMock;
        private IUserService _userService;
            
        [TestInitialize]
        public void Initialize()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _mapperMock = new Mock<IMapper>();
            _passwordHelperMock = new Mock<IPasswordHelper>();

            _userService = new UserService(_mapperMock.Object,
                _passwordHelperMock.Object,
                _userRepositoryMock.Object);
        }

        [TestMethod]
        public void CtorThrowsExceptionIfNullIsPassed()
        {
            ExceptionAssert.Throws<ArgumentNullException>(() => new UserService(
                null,
                _passwordHelperMock.Object,
                _userRepositoryMock.Object));

            ExceptionAssert.Throws<ArgumentNullException>(() => new UserService(
                _mapperMock.Object,
                null,
                _userRepositoryMock.Object));

            ExceptionAssert.Throws<ArgumentNullException>(() => new UserService(
                _mapperMock.Object,
                _passwordHelperMock.Object,
                null));
        }

        [TestMethod]
        public void RegisterUser_NullPassed_ThrowsArgumentNullException()
        {
            ExceptionAssert.Throws<ArgumentNullException>(() => _userService.RegisterUser((RegisterUserModel)null));
        }

        [TestMethod]
        public void RegisterUser_AllRequiredMethodsAreCalled()
        {
            //arrange
            var model = new RegisterUserModel()
            {
                Email = "email",
                ConfirmPassword = "confirm_password",
                Password = "password",
                FirstName = "first_name",
                LastName = "last_name"
            };

            var passwordModel = new PasswordModel()
            {
                PasswordSalt = "salt",
                PasswordHash = "hash"
            };

            _passwordHelperMock.Setup(x => x.HashPassword(It.IsAny<string>())).Returns(passwordModel);

            //act
            _userService.RegisterUser(model);

            //assert
            _passwordHelperMock.Verify(x => x.HashPassword(model.Password), Times.Once);
            _userRepositoryMock.Verify(x => x.AddUser(It.Is<User>(
                q => q.Email == model.Email &&
                q.FirstName == model.FirstName &&
                q.LastName == model.LastName &&
                q.PasswordHash == passwordModel.PasswordHash &&
                q.PasswordSalt == passwordModel.PasswordSalt)), Times.Once());
        }

        [TestMethod]
        public void ValidateUser_NullArguments_ThrowsArgumentNullException()
        {
            ExceptionAssert.Throws<ArgumentNullException>(() => _userService.ValidateUser(null, "password"));
            ExceptionAssert.Throws<ArgumentNullException>(() => _userService.ValidateUser("email", null));
            ExceptionAssert.Throws<ArgumentNullException>(() => _userService.ValidateUser(null, null));
        }

        [TestMethod]
        public void ValidateUser_RetrievedUserIsNull_ReturnsFalse()
        {
            //arrange
            _userRepositoryMock.Setup(x => x.GetUserByEmail(It.IsAny<string>())).Returns((User) null);
            string email = "email";
            string password = "password";

            //act
            var result = _userService.ValidateUser(email, password);

            //assert
            _userRepositoryMock.Verify(x => x.GetUserByEmail(email), Times.Once);
            Assert.IsFalse(result);
            _passwordHelperMock.Verify(x => x.IsPasswordValid(It.IsAny<User>(), It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public void ValidateUser_ReturnsTrue()
        {
            //arrange
            var user = new User()
            {
                Email = "email",
                FirstName = "first_name",
                LastName = "last_name",
                Id = 1,
                PasswordSalt = "salt",
                PasswordHash = "hash"
            };

            _userRepositoryMock.Setup(x => x.GetUserByEmail(It.IsAny<string>())).Returns(user);

            string email = "email";
            string password = "password";

            _passwordHelperMock.Setup(x => x.IsPasswordValid(user, It.IsAny<string>())).Returns(true);

            //act
            var result = _userService.ValidateUser(email, password);

            //assert
            _userRepositoryMock.Verify(x => x.GetUserByEmail(email), Times.Once);
            _passwordHelperMock.Verify(x => x.IsPasswordValid(user, It.IsAny<string>()), Times.Once);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetUserByEmail_EmailIsNull_ThrowsArgumentNullException()
        {
            ExceptionAssert.Throws<ArgumentNullException>(() => _userService.GetUserByEmail(null));
        }

        [TestMethod]
        public void GetUserByEmail_AllRequiredMethodsAreCalled()
        {
            //arrange
            string email = "email";

            var user = new User()
            {
                Email = "email",
                FirstName = "first_name",
                LastName = "last_name",
                Id = 1,
                PasswordSalt = "salt",
                PasswordHash = "hash"
            };

            _userRepositoryMock.Setup(x => x.GetUserByEmail(It.IsAny<string>())).Returns(user);

            //act
            _userService.GetUserByEmail(email);

            //assert
            _userRepositoryMock.Verify(x => x.GetUserByEmail(email), Times.Once);
            _mapperMock.Verify(x => x.Map<UserDto>(user), Times.Once);
        }

        [TestMethod]
        public void CanCreateUser_EmailIsNull_ThrowsArgumentNullException()
        {
            ExceptionAssert.Throws<ArgumentNullException>(() => _userService.CanCrateUser(null));
        }

        [TestMethod]
        public void CanCreateUser_ReturnsFalse()
        {
            //arrange
            string email = "email";

            var user = new User()
            {
                Email = "email",
                FirstName = "first_name",
                LastName = "last_name",
                Id = 1,
                PasswordSalt = "salt",
                PasswordHash = "hash"
            };

            _userRepositoryMock.Setup(x => x.GetUserByEmail(email)).Returns(user);

            //act
            var result = _userService.CanCrateUser(email);

            //assert
            _userRepositoryMock.Verify(x => x.GetUserByEmail(email), Times.Once);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CanCreateUser_ReturnsTrue()
        {
            //arrange
            string email = "email";
            _userRepositoryMock.Setup(x => x.GetUserByEmail(email)).Returns((User)null);

            //act
            var result = _userService.CanCrateUser(email);

            //assert
            _userRepositoryMock.Verify(x => x.GetUserByEmail(email), Times.Once);
            Assert.IsTrue(result);
        }
    }
}

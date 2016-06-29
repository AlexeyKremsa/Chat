using System.Security.Claims;
using System.Threading.Tasks;
using Chat.Services.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Chat.Web.Models;

namespace Chat.Web
{
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
            UserValidator = new UserValidator<ApplicationUser>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };
        }

        public override Task<IdentityResult> CreateAsync(ApplicationUser user)
        {
            Store.CreateAsync(user).Wait();

            return base.CreateAsync(user);
        }
    }

    // Configure the application sign-in manager which is used in this application.
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        private readonly IUserService _userService;

        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager, IUserService userService)
            : base(userManager, authenticationManager)
        {
            _userService = userService;
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public override async Task<SignInStatus> PasswordSignInAsync(string email, string password, bool isPersistent, bool shouldLockout)
        {
            if (_userService.ValidateUser(email, password))
            {
                var user = new ApplicationUser()
                {
                    UserName = email
                };

                await SignInAsync(user, isPersistent, false);

                return SignInStatus.Success;
            }

            return SignInStatus.Failure;
        }
    }

    public class ApplicationUserStore<TUser> : IUserStore<TUser> where TUser : ApplicationUser
    {
        public void Dispose()
        {
            
        }

        public Task CreateAsync(TUser user)
        {
            return new Task(() => { });
        }

        public Task UpdateAsync(TUser user)
        {
            return new Task(() => { });
        }

        public Task DeleteAsync(TUser user)
        {
            return new Task(() => { });
        }

        public Task<TUser> FindByIdAsync(string userId)
        {
            return new Task<TUser>(() => null);
        }

        public Task<TUser> FindByNameAsync(string userName)
        {
            return new Task<TUser>(() => null);
        }
    }
}

using System;
using System.Security.Authentication;
using System.Web.Mvc;
using AutoMapper;
using Chat.Services.Interfaces;
using Chat.Web.Models;
using Microsoft.Practices.Unity;

namespace Chat.Web.Infrastructure
{
    [Authorize]
    public class BaseController : Controller
    {
        [Dependency]
        public IMapper Mapper { get; set; }

        [Dependency]
        public IUserService UserService { get; set; }

        protected void ThrowIfNull<TArgument>(TArgument argument)
        {
            if (argument == null)
            {
                throw new ArgumentNullException(typeof(TArgument).Name);
            }
        }

        private ApplicationUser _currentUser;
        protected ApplicationUser CurrentUser
        {
            get
            {
                if (_currentUser == null && User != null && User.Identity != null)
                {
                    _currentUser = Mapper.Map<ApplicationUser>(UserService.GetUserByEmail(User.Identity.Name));
                }

                if (_currentUser == null)
                {
                    throw new AuthenticationException();
                }

                return _currentUser;
            }
        }
    }
}
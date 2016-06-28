using System.Web.Mvc;
using Chat.Infrastructure.ViewModels;
using Chat.Web.Infrastructure;
using Chat.Web.Infrastructure.SignalR;

namespace Chat.Web.Controllers
{
    [Authorize]
    public class ChatController : BaseController
    {
        private readonly IChatPermission _charPermission;

        public ChatController(IChatPermission chatPermission)
        {
            ThrowIfNull(chatPermission);

            _charPermission = chatPermission;
        }

        [HttpGet]
        public ActionResult Index()
        {
            string userNameEmail = string.Format("{0} ({1})", CurrentUser.FirstName, CurrentUser.Email);

            if (_charPermission.CanUserJoinChat(userNameEmail))
            {
                return View(new ChatViewModel()
                {
                    UserNameEmail = userNameEmail
                }); 
            }

            ModelState.AddModelError("", "User with such an email had already joined the chat");
            return View("Error");
        }

        [HttpGet]
        public ActionResult PrivateDialogTemplate(string windowId, string userName)
        {
            return PartialView("PrivateDialogTemplate", new PrivateDialogTemplateModel()
            {
                WindowId = windowId,
                UserName = userName
            });
        }
    }
}
using System.Web.Mvc;
using Chat.Infrastructure.ViewModels;
using Chat.Web.Infrastructure;

namespace Chat.Web.Controllers
{
    [Authorize]
    public class ChatController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            string userNameEmail = string.Format("{0} ({1})", CurrentUser.FirstName, CurrentUser.Email);

            return View(new ChatViewModel()
            {
                UserNameEmail = userNameEmail
            });
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
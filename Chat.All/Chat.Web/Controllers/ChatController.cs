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
            return View(new ChatViewModel()
            {
                Email = CurrentUser.Email,
                UserName = CurrentUser.FirstName
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
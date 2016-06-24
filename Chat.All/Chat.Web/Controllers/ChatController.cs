using System.Web.Mvc;
using Chat.Infrastructure.ViewModels;
using Chat.Web.Infrastructure;

namespace Chat.Web.Controllers
{
    [Authorize]
    public class ChatController : BaseController
    {
        // GET: Chat
        public ActionResult Index()
        {
            return View(new ChatViewModel()
            {
                Email = CurrentUser.Email,
                UserName = CurrentUser.FirstName
            });
        }
    }
}
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
            var a = HttpContext.User.Identity.Name;
            return View(new ChatViewModel()
            {
                Email = User.Identity.Name
            });
        }
    }
}
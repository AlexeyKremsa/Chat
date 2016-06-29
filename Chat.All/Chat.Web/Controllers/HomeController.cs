using System.Web.Mvc;
using Chat.Web.Infrastructure;

namespace Chat.Web.Controllers
{
    public class HomeController : BaseController
    {
        [AllowAnonymous]
        public ActionResult About()
        {
            return View();
        }
    }
}
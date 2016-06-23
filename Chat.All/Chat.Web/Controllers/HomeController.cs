﻿using System.Web.Mvc;
using Chat.Web.Infrastructure;

namespace Chat.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
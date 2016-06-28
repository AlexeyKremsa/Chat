using System;
using System.Web.Mvc;
using Chat.Web.Models;
using log4net;

namespace Chat.Web.CustomExceptions
{
    public class CustomErrorHandler : HandleErrorAttribute
    {
        private readonly ILog _log;

        public CustomErrorHandler(ILog log)
        {
            _log = log;
        }

        public override void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled)
            {
                string controller = filterContext.RouteData.Values["controller"].ToString();
                string action = filterContext.RouteData.Values["action"].ToString();
                Exception ex = filterContext.Exception;
                filterContext.Result = new ViewResult()
                {
                    ViewName = "~/Views/Shared/Error.cshtml",
                    ViewData = new ViewDataDictionary(new CustomExceptionModel()
                    {
                        ExceptionMessage = ex.Message,
                        ControllerName = controller,
                        ActionName = action
                    })
                };
                filterContext.ExceptionHandled = true;

                _log.Error(ex.Message);
                _log.Error(ex.StackTrace);
            }
        }
    }
}
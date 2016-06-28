using System;
using System.Web.Mvc;
using Chat.Web.Models;

namespace Chat.Web.CustomExceptions
{
    public class CustomErrorHandler : HandleErrorAttribute
    {
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
                //Log error  
                //Logger.LogErrorMessage(ex.Message);
                //do something with these details here  
                
            }
        }
    }
}
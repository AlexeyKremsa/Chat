using System.Web.Mvc;
using Chat.Web.CustomExceptions;
using log4net;

namespace Chat.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(ILog log, GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new CustomErrorHandler(log));
        }
    }
}

using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Chat.Web.App_Start;
using log4net;
using Microsoft.Practices.Unity;

namespace Chat.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            var log = UnityConfig.GetConfiguredContainer().Resolve<ILog>();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(log, GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            UnityWebActivator.Start();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var log = UnityConfig.GetConfiguredContainer().Resolve<ILog>();

            Exception ex = Server.GetLastError();
            
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
        }
    }
}

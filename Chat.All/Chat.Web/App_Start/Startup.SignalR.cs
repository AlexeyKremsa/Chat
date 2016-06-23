using Microsoft.Owin;
using Owin;

//[assembly: OwinStartup(typeof(SignalRChat.Startup))]
namespace Chat.Web
{
    public partial class Startup
    {
        public void ConfigureSignalR(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
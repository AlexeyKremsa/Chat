
namespace Chat.Web.Models
{
    public class CustomExceptionModel
    {
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string ExceptionMessage { get; set; }
    }
}
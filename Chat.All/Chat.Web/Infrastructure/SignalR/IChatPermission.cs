namespace Chat.Web.Infrastructure.SignalR
{
    public interface IChatPermission
    {
        bool CanUserJoinChat(string userName);
    }
}

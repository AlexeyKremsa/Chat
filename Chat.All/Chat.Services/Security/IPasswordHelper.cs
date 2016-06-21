namespace Chat.Services.Security
{
    public interface IPasswordHelper
    {
        PasswordModel HashPassword(string password);
    }
}

namespace Chat.Services.Security
{
    public class PasswordModel
    {
        public string PasswordsHash { get; set; }
        public string PasswordSalt { get; set; }
    }
}

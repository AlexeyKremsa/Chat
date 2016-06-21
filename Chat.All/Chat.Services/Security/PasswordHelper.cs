using System;

namespace Chat.Services.Security
{
    public class PasswordHelper: IPasswordHelper
    {
        public PasswordModel HashPassword(string password)
        {
            if (String.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException("password", "Provided password is null or empty");
            }

            var crypto = new SimpleCrypto.PBKDF2();

            return new PasswordModel()
            {
                PasswordsHash = crypto.Compute(password),
                PasswordSalt = crypto.Salt
            };
        }
    }
}

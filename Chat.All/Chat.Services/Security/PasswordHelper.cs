using System;
using Chat.EntityModel;

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

            var cryptoService = new SimpleCrypto.PBKDF2();

            return new PasswordModel()
            {
                PasswordsHash = cryptoService.Compute(password),
                PasswordSalt = cryptoService.Salt
            };
        }

        public bool IsPasswordValid(User user, string password)
        {
            if (user != null && !String.IsNullOrEmpty(password))
            {
                var cryptoService = new SimpleCrypto.PBKDF2();

                var passwordHashToCheck = cryptoService.Compute(password, user.PasswordSalt);

                return cryptoService.Compare(user.PasswordHash, passwordHashToCheck);
            }

            return false;
        }
    }
}

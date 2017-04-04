using Bookmaker.Core.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bookmaker.Core.Domain
{
    public class User
    {
        public Guid Id { get; protected set; }
        public string Email { get; protected set; }
        public string Password { get; protected set; }
        public string Salt { get; protected set; }
        public string Username { get; protected set; }
        public string FullName { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public Wallet Wallet { get; protected set; }
        public IEnumerable<Bet> Bets { get; protected set; }

        protected User()
        {
        }

        public User(string email, string username, string password, string salt)
        {
            var emailValidator = new EmailValidator();

            if (!emailValidator.IsValid(email))
                throw new Exception("Provided email has invalid format.");

            if (!emailValidator.IsUnique(email))
                throw new Exception("This email address is already in use.");

            if (password.Length < 6)
                throw new Exception("Password must have at least 6 letters.");

            //TODO: sprawdzenie soli

            Id = Guid.NewGuid();
            Email = email;
            Username = username;
            Password = password;
            Salt = salt;
            CreatedAt = DateTime.UtcNow;
        }
    }
}

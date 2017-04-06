using Bookmaker.Core.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Bookmaker.Core.Domain
{
    public class User
    {
        private readonly Regex nameRegex = new Regex("^(?![_.-])(?!.*[_.-]{2})[a-zA-Z0-9._.-]+(?<![_.-])$");

        public Guid Id { get; protected set; }
        public string Email { get; protected set; }
        public string Password { get; protected set; }
        public string Salt { get; protected set; }
        public string Username { get; protected set; }
        public string FullName { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public Wallet Wallet { get; protected set; }
        public IEnumerable<Bet> Bets { get; protected set; }
        public DateTime LastUpdate { get; protected set; }

        protected User()
        {
        }

        public User(string email, string username, string password, string salt)
        {
            //TODO: walidacja wszystkiego
            Id = Guid.NewGuid();

            SetEmail(email);
            SetUsername(username);
            SetPassword(password);

            Salt = salt;
            CreatedAt = DateTime.UtcNow;
        }

        public void SetUsername(string username)
        {
            if (!nameRegex.IsMatch(username))
            {
                throw new Exception("Username is invalid.");
            }
            if (Username == username)
            {
                return;
            }

            Username = username;
            Update();
        }

        public void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new Exception("Email cannot be empty.");
            }
            if (Email == email)
            {
                return;
            }

            Email = email;
            Update();
        }

        public void SetPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new Exception("Password cannot be empty.");
            }
            if (password.Length < 4)
            {
                throw new Exception("Password must contain at least 4 characters.");
            }
            if (password.Length > 100)
            {
                throw new Exception("Password cannot contain more than 100 characters.");
            }
            if (Password == password)
            {
                return;
            }

            Password = password;
            Update();
        }

        private void Update()
        {
            LastUpdate = DateTime.UtcNow;
        }
    }
}

using Bookmaker.Core.Interfaces;
using Bookmaker.Core.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;

namespace Bookmaker.Core.Domain
{
    public class User
    {
        private readonly Regex nameRegex = new Regex("^(?![_.-])(?!.*[_.-]{2})[a-zA-Z0-9._.-]+(?<![_.-])$");
        
        public int Id { get; protected set; }        
        public string Email { get; protected set; }        
        public string Password { get; protected set; }        
        public string Salt { get; protected set; }        
        public string Username { get; protected set; }
        public string FullName { get; protected set; }        
        public DateTime CreatedAt { get; protected set; }        
        public DateTime LastUpdate { get; protected set; }    
        public Wallet Wallet { get; protected set; }
        public IEnumerable<Bet> Bets { get; protected set; }

        protected User()
        {
        }

        public User(string email, string username, string password, string salt)
        {
            SetEmail(email);
            SetUsername(username);
            SetPassword(password);

            Salt = salt;
            SetCreationDate();
        }

        public void SetId(int id)
        {
            if (id < 0)
            {
                throw new Exception($"User: Id cannot be set to { id } (less than zero).");
            }

            Id = id;
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

            // TODO: to powinno być w infrastructure?
            IEmailValidator emailValidator = new EmailValidator();

            if (!emailValidator.IsUnique(email))
            {
                throw new Exception("Provided email is already in use.");
            }
            if (!emailValidator.IsValid(email))
            {
                throw new Exception("Provided email is not valid.");
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

        public void SetSalt(string salt)
        {
            if (string.IsNullOrWhiteSpace(salt))
            {
                throw new Exception("User: salt cannot be empty.");
            }
            if (Salt == salt.ToLowerInvariant())
            {
                return;
            }

            Salt = salt;
            Update();
        }

        private void Update()
        {
            LastUpdate = DateTime.UtcNow;
        }

        private void SetCreationDate()
        {
            CreatedAt = DateTime.UtcNow;
            Update();
        }
    }
}

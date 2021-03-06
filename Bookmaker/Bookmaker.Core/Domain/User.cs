﻿using Bookmaker.Core.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;

namespace Bookmaker.Core.Domain
{
    public class User
    {
        private readonly Regex _nameRegex = new Regex("^(?![_.-])(?!.*[_.-]{2})[a-zA-Z0-9._.-]+(?<![_.-])$");
        private const int _defaultWalletPoints = 1000;

        public int Id { get; protected set; }
        public string Email { get; protected set; }
        public string Salt { get; protected set; }
        public string Hash { get; protected set; }
        public string Username { get; protected set; }
        public string FullName { get; protected set; }
        public string Role { get; set; }
        public int WalletPoints { get; protected set; }
        public string AvatarFileName { get; set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime LastUpdate { get; protected set; }
        public IEnumerable<Bet> Bets { get; protected set; }

        protected User()
        {
        }

        public User(string email, string username, string salt, string hash, string role, int walletPoints = _defaultWalletPoints)
        {
            SetEmail(email);
            SetUsername(username);
            SetSalt(salt);
            SetHash(hash);
            SetRole(role);
            SetWalletPoints(walletPoints);
            SetAvatarFileName("default-user-avatar.png");

            SetCreationDate();
        }

        public void SetId(int id)
        {
            if (id < 0)
            {
                throw new InvalidDataException($"User: Id cannot be set to '{ id }' (less than zero).");
            }

            Id = id;
        }

        public void SetUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new InvalidDataException("Username cannot be empty.");
            }

            if (username.Length < 2)
            {
                throw new InvalidDataException("Username cannot be shorter than 2 characters.");
            }

            if (!_nameRegex.IsMatch(username))
            {
                throw new InvalidDataException("Username contains invalid characters.");
            }

            if (Username == username)
            {
                return;
            }

            Username = username;
            Update();
        }

        public void SetAvatarFileName(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new InvalidDataException("Avatar file name cannot be empty.");
            }

            if (fileName == AvatarFileName)
            {
                return;
            }

            AvatarFileName = fileName;
            Update();
        } 

        public void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new InvalidDataException("Email cannot be empty.");
            }

            if (Email == email)
            {
                return;
            }

            Email = email;
            Update();
        }

        //public void SetPassword(string password)
        //{
        //    if (string.IsNullOrWhiteSpace(password))
        //    {
        //        throw new InvalidDataException("Password cannot be empty.");
        //    }

        //    if (password.Length < 4)
        //    {
        //        throw new InvalidDataException("Password must contain at least 4 characters.");
        //    }

        //    if (password.Length > 100)
        //    {
        //        throw new InvalidDataException("Password cannot contain more than 100 characters.");
        //    }

        //    if (Password == password)
        //    {
        //        return;
        //    }

        //    Password = password;
        //    Update();
        //}

        public void SetSalt(string salt)
        {
            if (string.IsNullOrWhiteSpace(salt))
            {
                throw new InvalidDataException("User: salt cannot be empty.");
            }

            Salt = salt;
            Update();
        }

        public void SetHash(string hash)
        {
            if (string.IsNullOrWhiteSpace(hash))
            {
                throw new InvalidDataException("User: hash cannot be empty.");
            }

            Hash = hash;
            Update();
        }

        public void SetFullName(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
            {
                throw new InvalidDataException("Full name cannot be empty.");
            }

            FullName = fullName;
            Update();
        }

        public void SetRole(string role)
        {
            if (string.IsNullOrWhiteSpace(role))
            {
                throw new InvalidDataException("Role cannot be empty.");
            }

            Role = role;
            Update();
        }

        public void SetWalletPoints(int points)
        {
            if (points < 0)
                throw new InvalidDataException("User: cannot set less than zero wallet points.");

            if (points > 1000000)
                throw new InvalidDataException("User: cannot set more than 1000000 wallet points.");

            if (WalletPoints == points)
                return;

            WalletPoints = points;
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

﻿using Bookmaker.Core.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Bookmaker.Core.Domain;

namespace Bookmaker.Infrastructure.Repositories
{
    public class InMemoryUserRepository : IUserRepository
    {
        private static ISet<User> _users = new HashSet<User>
        {
            new User("em@1.com", "em", "pas13123s", "salt"),
            new User("eq@2.com", "eq", "pas12312312sx", "saaaalt")
        };

        public async Task AddAsync(User user)
        {
            _users.Add(user);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
            => await Task.FromResult(_users);

        public async Task<User> GetAsync(Guid id)
            => await Task.FromResult(_users.SingleOrDefault(x => x.Id == id));

        public async Task<User> GetAsync(string email)
            => await Task.FromResult(_users.SingleOrDefault(x => x.Email == email.ToLowerInvariant()));

        public async Task RemoveAsync(string email)
        {
            var user = await GetAsync(email);
            _users.Remove(user);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(User user)
        {
            var userToUpdate = await GetAsync(user.Email);

            if (userToUpdate == null)
            {
                throw new Exception($"User with email'{ user.Email }' does not exist.");
            }
            
            userToUpdate.SetUsername(user.Username);
            userToUpdate.SetPassword(user.Password);            
            await Task.CompletedTask;
        }
    }
}

using Bookmaker.Core.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Bookmaker.Core.Domain;
using Bookmaker.Core.Utils;

namespace Bookmaker.Infrastructure.Repositories
{
    public class InMemoryUserRepository : IUserRepository
    {
        private static ISet<User> _users = new HashSet<User>
        {
            new User("test1@email.com", "em", "pas13123s", "salt"),
            new User("test2@email.com", "eq", "pas12312312sx", "saaaalt")
        };

        public InMemoryUserRepository()
        {
            int i = 0;
            foreach(var user in _users)
            {
                user.SetId(i++);                
            }
        }

        public async Task AddAsync(User user)
        {
            var maxId = _users.Max(v => v.Id);
            user.SetId(maxId + 1);
            _users.Add(user);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
            => await Task.FromResult(_users);

        //public async Task<User> GetAsync(string id)
        //    => await Task.FromResult(_users.SingleOrDefault(x => x.Id == id));

        public async Task<User> GetAsync(string email)
            => await Task.FromResult(_users.SingleOrDefault(x => x.Email == email.ToLowerInvariant()));

        public Task<User> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

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
                throw new InvalidDataException($"User with email'{ user.Email }' does not exist.");
            }
            
            userToUpdate.SetUsername(user.Username);
            userToUpdate.SetPassword(user.Password);            
            await Task.CompletedTask;
        }
    }
}

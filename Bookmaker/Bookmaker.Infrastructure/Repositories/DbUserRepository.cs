using Bookmaker.Core.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using Bookmaker.Core.Domain;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Bookmaker.Infrastructure.Helpers;
using System.Data;
using Dapper;
using System.Linq;
using Bookmaker.Core.Utils;

namespace Bookmaker.Infrastructure.Repositories
{
    public class DbUserRepository : IUserRepository
    {
        public async Task AddAsync(User user)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var listToAdd = new List<User> { user };
                var executeString = "dbo.Users_Insert @Email, @Password, @Salt, @Username, @FullName, @WalletPoints, @CreatedAt, @LastUpdate";
                
                // todo: Co z IEnumerable?
                await Task.Factory.StartNew(()
                    => connection.Execute(executeString, listToAdd));
            }            
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                // todel: #ask1
                return await Task.Factory.StartNew(()
                    => connection.Query<User>("dbo.Users_GetAll"));
            }
        }

        public Task<User> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetAsync(string email)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                List<User> output = new List<User>();

                await Task.Factory.StartNew(()
                    => output = connection.Query<User>("dbo.Users_GetByEmail @Email", new { Email = email }).ToList());
                
                if (output == null)
                {
                    return null;
                }

                if (output.Count == 0)
                {
                    return null;
                }

                if (output.Count > 1)
                {
                    throw new InvalidDataException($"DbUserRepository: More than one user with email '{ email }' found.");
                }

                return output[0];
            }
        }

        public async Task<User> GetByIdAsync(int id)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var user = await Task.Factory.StartNew(()
                    => connection.Query<User>("dbo.Users_GetById @Id", new { Id = id }).ToList());

                if (user == null)
                {
                    return null;
                }

                if (user.Count == 0)
                {
                    return null;
                }

                if (user.Count > 1)
                {
                    throw new InvalidDataException($"More than one user with id '{ id }' found.");
                }

                return user[0];
            }
        }

        public async Task RemoveAsync(string email)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var executeString = "dbo.Users_RemoveByEmail @Email";

                await Task.Factory.StartNew(()
                    => connection.Execute(executeString, new { Email = email }));
            }
        }

        public async Task UpdateAsync(User user)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var executeString = "dbo.Users_UpdateUser @Email, @Username, @Password, @FullName";

                await Task.Factory.StartNew(()
                    => connection.Execute(executeString, new { Email = user.Email, Username = user.Username, Password = user.Password, FullName = user.FullName }));
            }
        }
    }
}

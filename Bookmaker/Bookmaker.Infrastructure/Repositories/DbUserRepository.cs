﻿using Bookmaker.Core.Repository;
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

namespace Bookmaker.Infrastructure.Repositories
{
    public class DbUserRepository : IUserRepository
    {
        public async Task AddAsync(User user)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var listToAdd = new List<User> { user };
                var executeString = "dbo.Users_Insert @Id, @Email, @Password, @Salt, @Username, @FullName, @CreatedAt, @LastUpdate, @Wallet";
                
                // todo: czy DateTime będzie działać, Wallet, co z IEnumerable?
                await Task.Factory.StartNew(()
                    => connection.Execute(executeString, listToAdd));
            }            
        }

        public Task<IEnumerable<User>> GetAllAsync()
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {

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
                var output = connection.Query<User>("dbo.Users_GetByEmail @Email", new { Email = email }).ToList(); // TODO: add stored procedure  select * from Users where Email = '{ email }'
                if (output == null)
                {
                    return null;
                }

                if (output.Count > 1)
                {
                    throw new Exception($"DbUserRepository: More than one user with email '{ email }' found.");
                }

                return await Task.FromResult(output[0]);
            }
        }

        public Task RemoveAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}

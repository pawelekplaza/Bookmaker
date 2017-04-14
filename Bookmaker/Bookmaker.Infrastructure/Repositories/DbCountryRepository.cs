﻿using System;
using System.Collections.Generic;
using System.Text;
using Bookmaker.Core.Domain;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Bookmaker.Infrastructure.Helpers;
using Dapper;
using System.Linq;
using Bookmaker.Core.Repository;
using Bookmaker.Core.Utils;

namespace Bookmaker.Infrastructure.Repositories
{
    public class DbCountryRepository : ICountryRepository
    {
        public async Task CreateAsync(Country country)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var listToAdd = new List<Country> { country };
                var executeString = "dbo.Countries_Insert @Name";

                await Task.Factory.StartNew(()
                    => connection.Execute(executeString, listToAdd));
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var executeString = "dbo.Countries_DeleteById @Id";

                await Task.Factory.StartNew(()
                    => connection.Execute(executeString, new { Id = id }));
            }
        }

        public async Task<IEnumerable<Country>> GetAllAsync()
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                return await Task.Factory.StartNew(()
                    => connection.Query<Country>("dbo.Countries_GetAll"));
            }
        }

        public async Task<Country> GetAsync(int id)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                List<Country> output = new List<Country>();

                await Task.Factory.StartNew(()
                    => output = connection.Query<Country>("dbo.Countries_GetById @Id", new { Id = id }).ToList());

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
                    throw new InvalidDataException($"More than one country with id '{ id }' found.");
                }

                return output[0];
            }
        }

        public async Task<Country> GetAsync(string name)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                List<Country> output = new List<Country>();

                await Task.Factory.StartNew(()
                    => output = connection.Query<Country>("dbo.Countries_GetByName @Name", new { Name = name }).ToList());

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
                    throw new InvalidDataException($"More than one country with name '{ name }' found.");
                }

                return output[0];                
            }
        }

        /// <summary>
        /// Updates country by id.
        /// </summary>     
        /// <param name="country">Must have the same id and new value of name.</param>
        public async Task UpdateAsync(Country country)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var executeString = "dbo.Countries_UpdateById @Id, @Name";

                await Task.Factory.StartNew(()
                    => connection.Execute(executeString, new { Id = country.Id, Name = country.Name }));
            }
        }
    }
}

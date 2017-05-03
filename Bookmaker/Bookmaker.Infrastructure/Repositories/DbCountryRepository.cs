using System;
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
using Bookmaker.Infrastructure.DTO;

namespace Bookmaker.Infrastructure.Repositories
{
    public class DbCountryRepository : ICountryRepository
    {
        private readonly ICommonDataProvider _commonDataProvider;

        public DbCountryRepository()
        {
            _commonDataProvider = new CommonDataProvider();
        }

        public async Task CreateAsync(Country country)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var listToAdd = new List<Country> { country };
                var executeString = "dbo.Countries_Insert @Name";

                await connection.ExecuteAsync(executeString, listToAdd);
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var executeString = "dbo.Countries_DeleteById @Id";

                await connection.ExecuteAsync(executeString, new { Id = id });
            }
        }

        public async Task<IEnumerable<Country>> GetAllAsync()
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                return await connection.QueryAsync<Country>("dbo.Countries_GetAll");
            }
        }

        public async Task<Country> GetByIdAsync(int id)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var queryResult = await connection.QueryAsync<Country>("dbo.Countries_GetById @Id", new { Id = id });
                var countryDtos = queryResult.ToList();                

                if (countryDtos == null)
                {
                    return null;
                }

                if (countryDtos.Count == 0)
                {
                    return null;
                }

                if (countryDtos.Count > 1)
                {
                    throw new InvalidDataException($"More than one country with id '{ id }' found.");
                }

                return countryDtos[0];
            }
        }

        public async Task<Country> GetByNameAsync(string name)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var queryResult = await connection.QueryAsync<Country>("dbo.Countries_GetByName @Name", new { Name = name });
                var countryDtos = queryResult.ToList();

                if (countryDtos == null)
                {
                    return null;
                }

                if (countryDtos.Count == 0)
                {
                    return null;
                }

                if (countryDtos.Count > 1)
                {
                    throw new InvalidDataException($"More than one country with name '{ name }' found.");
                }

                return countryDtos[0];                
            }
        }

        public async Task<IEnumerable<City>> GetCitiesAsync(int countryId)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var queryResult = await connection.QueryAsync<CityDto>("dbo.Countries_GetCities @Id", new { Id = countryId });
                var cityDtos = queryResult.ToList();

                var resultList = new List<City>();

                foreach (var city in cityDtos)
                {
                    var country = await GetByIdAsync(city.CountryId);
                    var newCity = new City(city.Name, country);
                    newCity.SetId(city.Id);

                    resultList.Add(newCity);
                }

                return resultList;
            }
        }

        public async Task<IEnumerable<Stadium>> GetStadiumsAsync(int countryId)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var queryResult = await connection.QueryAsync<StadiumDto>("dbo.Countries_GetStadiums @Id", new { Id = countryId });
                var stadiumDtos = queryResult.ToList();                

                var resultList = new List<Stadium>();

                foreach (var stadium in stadiumDtos)
                {                    
                    var city = await _commonDataProvider.GetCityAsync(stadium.CityId);

                    var newStadium = new Stadium(city.Country, city, stadium.Name);
                    newStadium.SetId(stadium.Id);

                    resultList.Add(newStadium);
                }

                return resultList;
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

                await connection.ExecuteAsync(executeString, new { Id = country.Id, Name = country.Name });
            }
        }        
    }
}

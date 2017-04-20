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

        public async Task<Country> GetByIdAsync(int id)
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

        public async Task<Country> GetByNameAsync(string name)
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

        public async Task<IEnumerable<City>> GetCitiesAsync(int countryId)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                List<CityDto> output = new List<CityDto>();

                await Task.Factory.StartNew(()
                    => output = connection.Query<CityDto>("dbo.Countries_GetCities @Id", new { Id = countryId }).ToList());

                var resultList = new List<City>();

                foreach (var city in output)
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
                List<StadiumDto> output = new List<StadiumDto>();

                await Task.Factory.StartNew(()
                    => output = connection.Query<StadiumDto>("dbo.Countries_GetStadiums @Id", new { Id = countryId }).ToList());

                var resultList = new List<Stadium>();

                foreach (var stadium in output)
                {
                    var city = await GetCityAsync(stadium.CityId);

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

                await Task.Factory.StartNew(()
                    => connection.Execute(executeString, new { Id = country.Id, Name = country.Name }));
            }
        }

        private async Task<City> GetCityAsync(int id)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var cityDto = await Task.Factory.StartNew(()
                    => connection.Query<CityDto>("dbo.Cities_GetById @Id", new { Id = id }).ToList());

                if (cityDto == null)
                {
                    return null;
                }

                if (cityDto.Count == 0)
                {
                    return null;
                }

                if (cityDto.Count > 1)
                {
                    return null;
                }

                var country = await GetByIdAsync(cityDto[0].CountryId);

                var city = new City(cityDto[0].Name, country);
                city.SetId(cityDto[0].Id);

                return city;
            }
        }
    }
}

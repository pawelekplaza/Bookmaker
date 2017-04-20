using Bookmaker.Core.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using Bookmaker.Core.Domain;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Bookmaker.Infrastructure.Helpers;
using Bookmaker.Infrastructure.DTO;
using Dapper;
using System.Linq;
using Bookmaker.Core.Utils;

namespace Bookmaker.Infrastructure.Repositories
{
    public class DbStadiumRepository : IStadiumRepository
    {
        private readonly ICityRepository _cityRepository;
        private readonly ICountryRepository _countryRepository;

        public DbStadiumRepository()
        {
            _cityRepository = new DbCityRepository();
            _countryRepository = new DbCountryRepository();
        }

        public async Task CreateAsync(Stadium stadium)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var listToAdd = new List<StadiumDto>
                {
                    new StadiumDto { Name = stadium.Name, CityId = stadium.City.Id, CountryId = stadium.Country.Id }
                };

                var executeString = "dbo.Stadiums_Insert @Name, @CityId, @CountryId";

                await Task.Factory.StartNew(()
                    => connection.Execute(executeString, listToAdd));                    
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var executeString = "dbo.Stadiums_DeleteById @Id";

                await Task.Factory.StartNew(()
                    => connection.Execute(executeString, new { Id = id }));
            }
        }

        public async Task<IEnumerable<Stadium>> GetAllAsync()
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var listOfDtos = await Task.Factory.StartNew(()
                    => connection.Query<StadiumDto>("dbo.Stadiums_GetAll"));

                var resultList = new List<Stadium>();

                foreach (var stadium in listOfDtos)
                {
                    var country = await _countryRepository.GetByIdAsync(stadium.CountryId);
                    var city = await _cityRepository.GetAsync(stadium.CityId);

                    var stadiumToAdd = new Stadium(country, city, stadium.Name);
                    stadiumToAdd.SetId(stadium.Id);

                    resultList.Add(stadiumToAdd);
                }

                return resultList;
            }
        }

        public async Task<Stadium> GetAsync(int id)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                List<StadiumDto> output = new List<StadiumDto>();

                await Task.Factory.StartNew(()
                    => output = connection.Query<StadiumDto>("dbo.Stadiums_GetById @Id", new { Id = id }).ToList());

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
                    throw new InvalidDataException($"More than one stadium with id'{ id }' found.");
                }

                var country = await _countryRepository.GetByIdAsync(output[0].CountryId);
                var city = await _cityRepository.GetAsync(output[0].CityId);

                var result = new Stadium(country, city, output[0].Name);
                result.SetId(output[0].Id);

                return result;
            }
        }

        public async Task UpdateAsync(Stadium stadium)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var executeString = "dbo.Stadiums_UpdateById @Id, @Name, @CityId, @CountryId";

                await Task.Factory.StartNew(()
                    => connection.Execute(executeString, new { Id = stadium.Id, Name = stadium.Name, CityId = stadium.City.Id, CountryId = stadium.Country.Id }));
            }
        }
    }
}

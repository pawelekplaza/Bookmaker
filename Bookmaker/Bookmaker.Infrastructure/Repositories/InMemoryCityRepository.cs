using Bookmaker.Core.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using Bookmaker.Core.Domain;
using System.Threading.Tasks;

namespace Bookmaker.Infrastructure.Repositories
{
    public class InMemoryCityRepository : ICityRepository
    {
        private static ISet<City> _cities = new HashSet<City>
        {
            new City("Warsaw", new Country("Poland"))
        };

        public async Task CreateAsync(City city)
        {
            
        }

        public async Task<IEnumerable<City>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(City city)
        {
            throw new NotImplementedException();
        }

        public Task<City> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<City>> GetAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Stadium>> GetStadiumsAsync(int cityId)
        {
            throw new NotImplementedException();
        }
    }
}

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
            new City("Warsaw", Guid.NewGuid())
        };

        public async Task AddAsync(City city)
        {
            
        }

        public async Task<IEnumerable<City>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<City> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<City>> GetAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(City city)
        {
            throw new NotImplementedException();
        }
    }
}

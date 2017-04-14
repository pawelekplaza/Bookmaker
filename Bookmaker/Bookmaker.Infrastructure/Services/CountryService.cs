using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Bookmaker.Infrastructure.DTO;
using Bookmaker.Core.Repository;
using AutoMapper;
using Bookmaker.Core.Utils;
using Bookmaker.Core.Domain;

namespace Bookmaker.Infrastructure.Services
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public CountryService(ICountryRepository countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(CountryCreateDto country)
        {
            var newCountry = await _countryRepository.GetAsync(country.Name);
            if (newCountry != null)
            {
                throw new InvalidDataException($"Country with name '{ country.Name }' already exists.");
            }

            newCountry = new Country(country.Name);

            await _countryRepository.CreateAsync(newCountry);
        }

        public async Task DeleteAsync(int id)
        {
            var countryToDelete = await _countryRepository.GetAsync(id);

            if (countryToDelete == null)
            {
                throw new InvalidDataException($"Country with id '{ id } does not exist.");
            }

            await _countryRepository.DeleteAsync(countryToDelete.Id);
        }

        public async Task<IEnumerable<CountryDto>> GetAllAsync()
        {
            var countries = await _countryRepository.GetAllAsync();
            var countriesDto = new HashSet<CountryDto>();

            foreach(var country in countries)
            {
                countriesDto.Add(_mapper.Map<Country, CountryDto>(country));
            }

            return countriesDto;
        }

        public async Task<CountryDto> GetAsync(int id)
        {
            var country = await _countryRepository.GetAsync(id);
            return _mapper.Map<Country, CountryDto>(country);
        }

        public async Task<CountryDto> GetAsync(string name)
        {
            var country = await _countryRepository.GetAsync(name);
            return _mapper.Map<Country, CountryDto>(country);
        }

        public async Task UpdateAsync(CountryUpdateDto country)
        {            
            var countryToUpdate = await _countryRepository.GetAsync(country.Id);

            if (countryToUpdate == null)
            {
                throw new InvalidDataException($"Country with id '{ country.Id }' does not exist.");
            }

            if (!string.IsNullOrWhiteSpace(country.Name))
            {
                countryToUpdate.SetName(country.Name);
            }

            await _countryRepository.UpdateAsync(countryToUpdate);
        }
    }
}

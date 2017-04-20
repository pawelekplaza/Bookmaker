using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Bookmaker.Infrastructure.DTO;
using Bookmaker.Core.Repository;
using AutoMapper;
using Bookmaker.Core.Utils;
using Bookmaker.Core.Domain;
using Bookmaker.Infrastructure.ServicesInterfaces;

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
            var newCountry = await _countryRepository.GetByNameAsync(country.Name);
            if (newCountry != null)
            {
                throw new InvalidDataException($"Country with name '{ country.Name }' already exists.");
            }

            newCountry = new Country(country.Name);

            await _countryRepository.CreateAsync(newCountry);
        }

        public async Task DeleteAsync(int id)
        {
            var countryToDelete = await _countryRepository.GetByIdAsync(id);

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

        public async Task<CountryDto> GetByIdAsync(int id)
        {
            var country = await _countryRepository.GetByIdAsync(id);
            return _mapper.Map<Country, CountryDto>(country);
        }

        public async Task<CountryDto> GetByNameAsync(string name)
        {
            var country = await _countryRepository.GetByNameAsync(name);
            return _mapper.Map<Country, CountryDto>(country);
        }

        public async Task<IEnumerable<CityDto>> GetCitiesAsync(int countryId)
        {
            var cities = await _countryRepository.GetCitiesAsync(countryId);
            var citiesDto = new HashSet<CityDto>();

            foreach (var city in cities)
            {
                citiesDto.Add(_mapper.Map<City, CityDto>(city));
            }

            return citiesDto;
        }

        public async Task<IEnumerable<StadiumDto>> GetStadiumsAsync(int countryId)
        {
            var stadiums = await _countryRepository.GetStadiumsAsync(countryId);
            var stadiumsDto = new HashSet<StadiumDto>();

            foreach (var stadium in stadiums)
            {
                stadiumsDto.Add(_mapper.Map<Stadium, StadiumDto>(stadium));
            }

            return stadiumsDto;
        }

        public async Task UpdateAsync(CountryUpdateDto country)
        {            
            var countryToUpdate = await _countryRepository.GetByIdAsync(country.Id);

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

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Bookmaker.Infrastructure.DTO;
using Bookmaker.Core.Repository;
using AutoMapper;
using Bookmaker.Core.Domain;
using Bookmaker.Core.Utils;

namespace Bookmaker.Infrastructure.Services
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public CityService(ICityRepository cityRepository, ICountryRepository countryRepository, IMapper mapper)
        {
            _cityRepository = cityRepository;
            _countryRepository = countryRepository;
            _mapper = mapper;                
        }

        public async Task CreateAsync(CityCreateDto city)
        {
            var country = await _countryRepository.GetAsync(city.CountryId);
            
            if (country == null)
            {
                throw new InvalidDataException($"Country with id '{ city.CountryId }' does not exist.");
            }                        

            var citiesList = await _cityRepository.GetAsync(city.Name);

            foreach (var value in citiesList)
            {
                if (value.Country.Name.ToLowerInvariant() == country.Name.ToLowerInvariant() && value.Name.ToLowerInvariant() == city.Name.ToLowerInvariant())
                {
                    throw new InvalidDataException($"City with name '{ city.Name }' and country '{ country.Name }' already exists.");
                }
            }

            var newCity = new City(city.Name, country);
            await _cityRepository.CreateAsync(newCity);
        }

        public async Task DeleteAsync(int id)
        {
            var cityToDelete = await _cityRepository.GetAsync(id);

            if (cityToDelete == null)
            {
                throw new InvalidDataException($"City with id '{ id }' does not exist.");
            }

            await _cityRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<CityDto>> GetAllAsync()
        {
            var cities = await _cityRepository.GetAllAsync();
            var citiesDto = new HashSet<CityDto>();

            foreach (var city in cities)
            {
                citiesDto.Add(_mapper.Map<City, CityDto>(city));
            }

            return citiesDto;
        }

        public async Task<CityDto> GetAsync(int id)
        {
            var city = await _cityRepository.GetAsync(id);

            return _mapper.Map<City, CityDto>(city);
        }

        public async Task<IEnumerable<CityDto>> GetAsync(string name)
        {
            var cities = await _cityRepository.GetAsync(name);
            var citiesDto = new HashSet<CityDto>();

            foreach (var city in cities)
            {
                citiesDto.Add(_mapper.Map<City, CityDto>(city));
            }

            return citiesDto;
        }

        public async Task UpdateAsync(CityUpdateDto city)
        {
            var cityToUpdate = await _cityRepository.GetAsync(city.Id.Value);

            if (cityToUpdate == null)
            {
                throw new InvalidDataException($"City with id '{ city.Id.Value }' does not exist.");
            }

            if (city.CountryId != null)
            {
                var country = await _countryRepository.GetAsync(city.CountryId.Value);

                if (country == null)
                {
                    throw new InvalidDataException($"Country with id '{ city.CountryId.Value }' does not exist.");
                }

                cityToUpdate.SetCountry(country);
            }

            if (!string.IsNullOrWhiteSpace(city.Name))
            {
                cityToUpdate.SetName(city.Name);
            }

            await _cityRepository.UpdateAsync(cityToUpdate);
        }
    }
}

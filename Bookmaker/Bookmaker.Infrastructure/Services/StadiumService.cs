using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Bookmaker.Infrastructure.DTO;
using Bookmaker.Core.Repository;
using AutoMapper;
using Bookmaker.Core.Utils;
using Bookmaker.Core.Domain;
using System.Linq;

namespace Bookmaker.Infrastructure.Services
{
    public class StadiumService : IStadiumService
    {
        private readonly IStadiumRepository _stadiumRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;

        public StadiumService(IStadiumRepository stadiumRepository, ICountryRepository countryRepository, ICityRepository cityRepository, IMapper mapper)
        {
            _stadiumRepository = stadiumRepository;
            _countryRepository = countryRepository;
            _cityRepository = cityRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(StadiumDto stadium)
        {
            var country = await _countryRepository.GetAsync(stadium.CountryId);

            if (country == null)
            {
                throw new InvalidDataException($"Country with id '{ stadium.CountryId }' does not exist.");
            }

            var city = await _cityRepository.GetAsync(stadium.CityId);

            if (city == null)
            {
                throw new InvalidDataException($"City with id '{ stadium.CityId }' does not exist.");
            }

            var stadiumsList = await _stadiumRepository.GetAllAsync();

            foreach (var value in stadiumsList)
            {
                if (value.City.Id == stadium.CityId 
                    && value.Country.Id == stadium.CountryId
                    && value.Name.ToLowerInvariant() == stadium.Name.ToLowerInvariant())
                {
                    throw new InvalidDataException($"Stadium with name '{ stadium.Name }', city id '{ stadium.CityId }' and country id '{ stadium.CountryId }' already exists.");
                }
            }

            var newStadium = new Stadium(country, city, stadium.Name);
            await _stadiumRepository.CreateAsync(newStadium);
        }

        public async Task DeleteAsync(int id)
        {
            var stadiumToDelete = await _stadiumRepository.GetAsync(id);

            if (stadiumToDelete == null)
            {
                throw new InvalidDataException($"Stadium with id '{ id }' does not exist.");
            }

            await _stadiumRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<StadiumDto>> GetAllAsync()
        {
            var stadiums = await _stadiumRepository.GetAllAsync();
            var stadiumsDto = new HashSet<StadiumDto>();

            foreach (var stadium in stadiums)
            {
                stadiumsDto.Add(_mapper.Map<Stadium, StadiumDto>(stadium));
            }

            return stadiumsDto;
        }

        public async Task<StadiumDto> GetAsync(int id)
        {
            var stadium = await _stadiumRepository.GetAsync(id);

            return _mapper.Map<Stadium, StadiumDto>(stadium);
        }

        public async Task UpdateAsync(StadiumUpdateDto stadium)
        {
            var stadiumToUpdate = await _stadiumRepository.GetAsync(stadium.Id);

            if (stadiumToUpdate == null)
            {
                throw new InvalidDataException($"Stadium with id '{ stadium.Id }' does not exist.");
            }

            if (stadium.CityId != null)
            {
                var city = await _cityRepository.GetAsync(stadium.CityId.Value);

                if (city == null)
                {
                    throw new InvalidDataException($"City with id '{ stadium.CityId.Value }' does not exist.");
                }

                stadiumToUpdate.SetCity(city);
            }

            if (stadium.CountryId != null)
            {
                var country = await _countryRepository.GetAsync(stadium.CountryId.Value);

                if (country == null)
                {
                    throw new InvalidDataException($"Country with id '{ stadium.CountryId.Value }' does not exist.");
                }

                stadiumToUpdate.SetCountry(country);
            }

            if (!string.IsNullOrWhiteSpace(stadium.Name))
            {
                stadiumToUpdate.SetName(stadium.Name);
            }

            await _stadiumRepository.UpdateAsync(stadiumToUpdate);
        }
    }
}

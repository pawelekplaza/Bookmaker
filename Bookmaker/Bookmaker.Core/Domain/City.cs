using Bookmaker.Core.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bookmaker.Core.Domain
{
    public class City
    {        
        public int Id { get; protected set; }
        public Country Country { get; protected set; }        
        public string Name { get; protected set; }
        public IEnumerable<Stadium> Stadiums { get; protected set; }

        protected City ()
        {

        }        

        public City(string name, Country country)
        {            
            SetName(name);
            SetCountry(country);
        }

        public void SetId(int id)
        {
            if (id < 0)
            {
                throw new InvalidDataException($"City: Id cannot be set to '{ id }' (less than zero).");
            }

            Id = id;
        }

        public void SetCountry(Country country)
        {
            if (country == null)
                throw new InvalidDataException("City: provided country is not valid.");

            if (Country == country)
                return;

            Country = country;
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new InvalidDataException("City: name cannot be empty.");

            if (Name == name.ToLowerInvariant())
                return;

            Name = name;
        }
    }
}

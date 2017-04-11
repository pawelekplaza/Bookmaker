using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bookmaker.Core.Domain
{
    public class City
    {        
        public Guid Id { get; protected set; }
        public Country Country { get; protected set; }        
        public string Name { get; protected set; }

        protected City ()
        {

        }

        public City(string name, Country country)
        {
            Id = Guid.NewGuid();
            SetName(name);
            SetCountry(country);
        }

        public void SetCountry(Country country)
        {
            if (country == null)
                throw new Exception("City: provided country is not valid.");

            if (Country == country)
                return;

            Country = country;
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new Exception("City: name cannot be empty.");

            if (Name == name.ToLowerInvariant())
                return;

            Name = name;
        }
    }
}

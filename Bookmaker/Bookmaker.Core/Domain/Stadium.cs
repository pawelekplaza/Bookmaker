using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bookmaker.Core.Domain
{
    public class Stadium
    {        
        public int Id { get; protected set; }    
        public Country Country { get; protected set; }
        public City City { get; protected set; }        
        public string Name { get; protected set; }

        protected Stadium()
        {

        }

        public Stadium(Country country, City city, string name)
        {

        }

        public void SetId(int id)
        {
            if (id < 0)
            {
                throw new Exception($"Stadium: Id cannot be set to '{ id }' (less than zero).");
            }

            Id = id;
        }

        public void SetCountry(Country country)
        {
            if (country == null)
                throw new Exception("Stadium: provided country is not valid.");

            if (Country == country)
                return;

            Country = country;
        }

        public void SetCity(City city)
        {
            if (city == null)
                throw new Exception("Stadium: provided city is not valid.");

            if (City == city)
                return;

            City = city;
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new Exception("Stadium: provided stadium name cannot be empty.");

            if (Name == name.ToLowerInvariant())
                return;

            Name = name;
        }
    }
}

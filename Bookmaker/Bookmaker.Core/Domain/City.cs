using System;
using System.Collections.Generic;
using System.Text;

namespace Bookmaker.Core.Domain
{
    public class City
    {
        public Guid Id { get; protected set; }
        public Guid CountryId { get; protected set; }
        public string Name { get; protected set; }

        protected City ()
        {

        }

        public City(string name, Guid countryId)
        {
            Id = Guid.NewGuid();
            SetName(name);
            SetCountryId(countryId);
        }

        public void SetCountryId(Guid countryId)
        {
            if (countryId == null)
                throw new Exception("City: provided country id is not valid.");

            if (CountryId == countryId)
                return;

            CountryId = countryId;
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

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bookmaker.Core.Domain
{
    public class Country
    {        
        public Guid Id { get; protected set; }                
        public string Name { get; protected set; }        

        protected Country()
        {

        }

        public Country(string name)
        {
            Id = Guid.NewGuid();
            SetName(name);
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new Exception("Country: name cannot be empty.");

            if (Name == name.ToLowerInvariant())
                return;

            Name = name;
        }
    }
}

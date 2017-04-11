using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bookmaker.Core.Domain
{
    public class Country
    {        
        public int Id { get; protected set; }                
        public string Name { get; protected set; }     

        protected Country()
        {

        }        

        public Country(string name)
        {            
            SetName(name);
        }

        public void SetId(int id)
        {
            if (id < 0)
            {
                throw new Exception($"Country: Id cannot be set to '{ id }' (less than zero).");
            }

            Id = id;
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

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
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Bookmaker.Core.Domain
{
    public class Stadium
    {
        public Guid Id { get; protected set; }
        public Guid CountryId { get; protected set; }
        public Guid CityId { get; protected set; }
        public string Name { get; protected set; }        
    }
}

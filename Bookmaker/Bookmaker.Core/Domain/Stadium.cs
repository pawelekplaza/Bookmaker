using System;
using System.Collections.Generic;
using System.Text;

namespace Bookmaker.Core.Domain
{
    public class Stadium
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public Country Country { get; protected set; }
        public City City { get; protected set; }
    }
}

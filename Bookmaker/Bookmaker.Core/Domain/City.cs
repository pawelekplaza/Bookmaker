using System;
using System.Collections.Generic;
using System.Text;

namespace Bookmaker.Core.Domain
{
    public class City
    {
        public Guid Id { get; protected set; }
        public Country Country { get; protected set; }
        public string Name { get; protected set; }
    }
}

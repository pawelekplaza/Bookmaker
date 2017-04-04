using System;
using System.Collections.Generic;
using System.Text;

namespace Bookmaker.Core.Domain
{
    public class Country
    {
        public Guid Id { get; protected set; }        
        public string Name { get; protected set; }
    }
}

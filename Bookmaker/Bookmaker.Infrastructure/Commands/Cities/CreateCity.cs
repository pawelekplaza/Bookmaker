using System;
using System.Collections.Generic;
using System.Text;

namespace Bookmaker.Infrastructure.Commands.Cities
{
    public class CreateCity : ICommand
    {
        public Guid CountryId { get; set; }
        public string Name { get; set; }
    }
}
